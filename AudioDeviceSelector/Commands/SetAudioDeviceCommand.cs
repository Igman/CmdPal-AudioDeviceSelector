// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommandPallet.AudioDeviceSelector.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Foundation;

namespace CommandPallet.AudioDeviceSelector.Commands;

internal sealed class SetAudioDeviceCommand : IInvokableCommand
{
    private readonly DeviceInformation _device;
    private readonly IIconInfo _icon;

    public event TypedEventHandler<object, IPropChangedEventArgs> PropChanged;

    public SetAudioDeviceCommand(DeviceInformation device)
    {
        _device = device;
        _icon = AudioDeviceService.ExtractDeviceIcon(device);
    }

    public IIconInfo Icon => _icon;

    public string Id => $"set-audio-device-{_device.Id}";

    public string Name => _device.Name;

    public ICommandResult Invoke(object sender)
    {
        Debug.WriteLine($"Setting default audio device to: {_device.Name}");
        Debug.WriteLine($"Device ID: {_device.Id}");

        // Extract the Core Audio device ID from the device properties
        // The COM interface expects format: {0.0.0.00000000}.{guid}
        // But DeviceInformation.Id from winrt is in the form: \\?\SWD#MMDEVAPI#{0.0.0.00000000}.{guid}#{interface-guid}
        string coreAudioDeviceId = AudioDeviceService.ExtractCoreAudioDeviceId(_device);

        if (string.IsNullOrEmpty(coreAudioDeviceId))
        {
            Debug.WriteLine($"Failed to extract Core Audio device ID for: {_device.Name}");
            return CommandResult.KeepOpen();
        }

        Debug.WriteLine($"Core Audio Device ID: {coreAudioDeviceId}");

        bool success = AudioDeviceService.SetDefaultAudioDevice(coreAudioDeviceId);

        if (!success)
        {
            Debug.WriteLine($"Failed to set audio device: {_device.Name}");
            return CommandResult.KeepOpen();
        }

        return CommandResult.Hide();
    }
}