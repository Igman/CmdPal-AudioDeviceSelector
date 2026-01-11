using CommandPallet.AudioDeviceSelector.Interop;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Devices;

namespace CommandPallet.AudioDeviceSelector.Services;

internal class AudioDeviceService
{
    public static bool SetDefaultAudioDevice(string deviceId)
    {
        try
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                return false;
            }

            var policyConfig = new PolicyConfigClientWin7();

            // Set for all roles to ensure it becomes the system default
            policyConfig.SetDefaultEndpoint(deviceId, ERole.eConsole);
            policyConfig.SetDefaultEndpoint(deviceId, ERole.eMultimedia);
            policyConfig.SetDefaultEndpoint(deviceId, ERole.eCommunications);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static async Task<IReadOnlyList<DeviceInformation>> GetAudioOutputDevicesAsync()
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


    public static string? ExtractCoreAudioDeviceId(DeviceInformation device)
    {
        // Try to get the device instance ID which contains the Core Audio endpoint ID
        if (device.Properties.TryGetValue("System.Devices.DeviceInstanceId", out object instanceIdObj)
            && instanceIdObj is string instanceId)
        {
            // Format: SWD\MMDEVAPI\{0.0.0.00000000}.{guid}
            // We need: {0.0.0.00000000}.{guid}
            var parts = instanceId.Split('\\');
            if (parts.Length >= 3)
            {
                return parts[2]; // Return the {0.0.0.00000000}.{guid} part
            }
        }

        return null;
    }

    public static IIconInfo ExtractDeviceIcon(DeviceInformation device)
    {
        // Try to get the form factor from device properties
        string formFactorKey = PropertyKeys.FormatPropertyKey(PropertyKeys.PKEY_AudioEndpoint_FormFactor);
        if (device.Properties.TryGetValue(formFactorKey, out object formFactorObj))
        {
            if (formFactorObj is uint formFactorValue)
            {
                var formFactor = (EndpointFormFactor)formFactorValue;

                return formFactor switch
                {
                    EndpointFormFactor.Headphones => new IconInfo("🎧"),
                    EndpointFormFactor.Headset => new IconInfo("🎧"),
                    EndpointFormFactor.Microphone => new IconInfo("🎤"),
                    EndpointFormFactor.Speakers => new IconInfo("🔈"),
                    EndpointFormFactor.DigitalAudioDisplayDevice => new IconInfo("🖥️"),
                    EndpointFormFactor.SPDIF => new IconInfo("🔌"),
                    EndpointFormFactor.RemoteNetworkDevice => new IconInfo("📶"),
                    EndpointFormFactor.Handset => new IconInfo("📞"),
                    EndpointFormFactor.LineLevel => new IconInfo("🎵"),
                    _ => new IconInfo("🔈")
                };
            }
        }

        // Default fallback when form factor is not available
        return new IconInfo("🔈");
    }
}