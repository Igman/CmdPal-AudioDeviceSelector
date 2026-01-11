// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Devices;
using CommandPallet.AudioDeviceSelector.Commands;
using CommandPallet.AudioDeviceSelector.Interop;

namespace CommandPallet.AudioDeviceSelector;

internal sealed partial class AudioDeviceSelectorPage : ListPage
{
    private IReadOnlyList<DeviceInformation>? _audioOutputDevices;

    public AudioDeviceSelectorPage()
    {
        Icon = new IconInfo("🔊");
        Title = "Audio Device Selector";
        Name = "Choose Audio Device";
    }

    public override IListItem[] GetItems()
    {
        _audioOutputDevices ??= GetAudioOutputDevicesAsync().GetAwaiter().GetResult();
        Debug.WriteLine("Found {0} audio output devices.", _audioOutputDevices.Count);
        return _audioOutputDevices.Count == 0
            ? [new ListItem(new Microsoft.CommandPalette.Extensions.Toolkit.NoOpCommand()) { Title = "No audio output devices found." }]
            : _audioOutputDevices.Select(CreateListItemForDevice).ToArray();
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

    private static async Task<IReadOnlyList<DeviceInformation>> GetAudioOutputDevicesAsync()
    {
        string audioSelector = MediaDevice.GetAudioRenderSelector();

        // Request additional properties including FormFactor
        var additionalProperties = new[]
        {
            PropertyKeys.FormatPropertyKey(PropertyKeys.PKEY_AudioEndpoint_FormFactor),
            PropertyKeys.FormatPropertyKey(PropertyKeys.PKEY_Device_FriendlyName),
            PropertyKeys.FormatPropertyKey(PropertyKeys.PKEY_ItemNameDisplay),
            "System.Devices.DeviceInstanceId"
        };

        var devices = await DeviceInformation.FindAllAsync(audioSelector, additionalProperties);
        return devices;
    }
}
