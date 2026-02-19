using CommandPalette.AudioDeviceSelector.Commands;
using CommandPalette.AudioDeviceSelector.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace CommandPalette.AudioDeviceSelector;

internal sealed partial class AudioDeviceSelectorPage : ListPage
{
    private IReadOnlyList<DeviceInformation> _audioOutputDevices = [];
    private int _isRefreshing; // 0 = false, 1 = true; accessed atomically via Interlocked

    public AudioDeviceSelectorPage()
    {
        Icon = new IconInfo("🔊");
        Title = "Audio Device Selector";
        Name = "Choose Audio Device";
        IsLoading = true;
    }

    public override IListItem[] GetItems()
    {
        if (Interlocked.CompareExchange(ref _isRefreshing, 1, 0) == 0)
        {
            _ = RefreshDevicesAsync();
        }

        return _audioOutputDevices.Count == 0
            ? [new ListItem(new NoOpCommand()) { Title = "No audio output devices found." }]
            : _audioOutputDevices.Select(CreateListItemForDevice).ToArray();
    }

    private async Task RefreshDevicesAsync()
    {
        try
        {
            var fresh = await AudioDeviceService.GetAudioOutputDevicesAsync().ConfigureAwait(false);

            if (!DevicesEqual(fresh, _audioOutputDevices))
            {
                _audioOutputDevices = fresh;
                RaiseItemsChanged(_audioOutputDevices.Count);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to refresh audio devices: {ex}");
        }
        finally
        {
            Interlocked.Exchange(ref _isRefreshing, 0);
            IsLoading = false;
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
