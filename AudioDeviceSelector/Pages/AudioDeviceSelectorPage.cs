using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using CommandPalette.AudioDeviceSelector.Commands;
using CommandPalette.AudioDeviceSelector.Services;

namespace CommandPalette.AudioDeviceSelector;

internal sealed partial class AudioDeviceSelectorPage : ListPage
{
    private IReadOnlyList<DeviceInformation> _audioOutputDevices = [];
    private bool _isRefreshing;

    public AudioDeviceSelectorPage()
    {
        Icon = new IconInfo("🔊");
        Title = "Audio Device Selector";
        Name = "Choose Audio Device";
        IsLoading = true;
    }

    public override IListItem[] GetItems()
    {
        if (!_isRefreshing)
        {
            _isRefreshing = true;
            _ = RefreshDevicesAsync();
        }

        return _audioOutputDevices.Count == 0
            ? [new ListItem(new NoOpCommand()) { Title = "No audio output devices found." }]
            : _audioOutputDevices.Select(CreateListItemForDevice).ToArray();
    }

    private async Task RefreshDevicesAsync()
    {
        var fresh = await AudioDeviceService.GetAudioOutputDevicesAsync().ConfigureAwait(false);
        _isRefreshing = false;
        IsLoading = false;

        if (!DevicesEqual(fresh, _audioOutputDevices))
        {
            _audioOutputDevices = fresh;
            RaiseItemsChanged(_audioOutputDevices.Count);
        }
    }

    private static bool DevicesEqual(IReadOnlyList<DeviceInformation> a, IReadOnlyList<DeviceInformation> b)
    {
        return a.Count == b.Count && a.Select(d => d.Id).SequenceEqual(b.Select(d => d.Id));
    }

    private static IListItem CreateListItemForDevice(DeviceInformation device)
    {
        var status = device.IsEnabled ? "Available" : "Disabled";
        var subtitle = device.IsDefault ? $"{status} (Default)" : status;

        return new ListItem(new SetAudioDeviceCommand(device))
        {
            Title = device.Name,
            Subtitle = subtitle
        };
    }
}
