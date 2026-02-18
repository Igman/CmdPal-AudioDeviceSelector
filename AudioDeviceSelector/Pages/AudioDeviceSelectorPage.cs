using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Devices.Enumeration;
using CommandPalette.AudioDeviceSelector.Commands;
using CommandPalette.AudioDeviceSelector.Services;

namespace CommandPalette.AudioDeviceSelector;

internal sealed partial class AudioDeviceSelectorPage : ListPage
{
    public AudioDeviceSelectorPage()
    {
        Icon = new IconInfo("🔊");
        Title = "Audio Device Selector";
        Name = "Choose Audio Device";
    }

    public override IListItem[] GetItems()
    {
        IReadOnlyList<DeviceInformation> audioOutputDevices = AudioDeviceService.GetAudioOutputDevicesAsync().GetAwaiter().GetResult();

        return audioOutputDevices.Count == 0
            ? [new ListItem(new Microsoft.CommandPalette.Extensions.Toolkit.NoOpCommand()) { Title = "No audio output devices found." }]
            : audioOutputDevices.Select(CreateListItemForDevice).ToArray();
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
