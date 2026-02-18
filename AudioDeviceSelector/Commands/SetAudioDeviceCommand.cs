using CommandPalette.AudioDeviceSelector.Services;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Foundation;

namespace CommandPalette.AudioDeviceSelector.Commands;

internal sealed class SetAudioDeviceCommand(DeviceInformation device) : IInvokableCommand
{
    private readonly DeviceInformation _device = device;
    private readonly IIconInfo _icon = AudioDeviceService.ExtractDeviceIcon(device);

    public event TypedEventHandler<object, IPropChangedEventArgs> PropChanged;

    public IIconInfo Icon => _icon;

    public string Id => $"set-audio-device-{_device.Id}";

    public string Name => _device.Name;

    public ICommandResult Invoke(object sender)
    {
        Debug.WriteLine($"Setting default audio device to: {_device.Name}");
        Debug.WriteLine($"Device ID: {_device.Id}");

        string? coreAudioDeviceId = AudioDeviceService.ExtractCoreAudioDeviceId(_device);

        if (string.IsNullOrEmpty(coreAudioDeviceId))
        {
            Debug.WriteLine($"Failed to extract Core Audio device ID for: {_device.Name}");
            return CommandResult.ShowToast(new ToastArgs
            {
                Message = $"Could not identify audio device: {_device.Name}",
                Result = CommandResult.KeepOpen(),
            });
        }

        Debug.WriteLine($"Core Audio Device ID: {coreAudioDeviceId}");

        bool success = AudioDeviceService.SetDefaultAudioDevice(coreAudioDeviceId);

        if (!success)
        {
            Debug.WriteLine($"Failed to switch to {_device.Name}");
            return CommandResult.ShowToast(new ToastArgs
            {
                Message = $"Failed to switch to {_device.Name}",
                Result = CommandResult.KeepOpen(),
            });
        }

        return CommandResult.Hide();
    }
}