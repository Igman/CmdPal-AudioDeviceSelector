// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommandPallet.AudioDeviceSelector.Interop;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Diagnostics;
using Windows.Devices.Enumeration;

namespace CommandPallet.AudioDeviceSelector.Services;

internal class AudioDeviceService
{
    public static bool SetDefaultAudioDevice(string deviceId)
    {
        try
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                Debug.WriteLine("Device ID is null or empty");
                return false;
            }

            var policyConfig = new PolicyConfigClientWin7();

            // Set for all roles to ensure it becomes the system default
            try
            {
                policyConfig.SetDefaultEndpoint(deviceId, ERole.eConsole);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set eConsole role: {ex.Message}");
                throw;
            }

            try
            {
                policyConfig.SetDefaultEndpoint(deviceId, ERole.eMultimedia);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set eMultimedia role: {ex.Message}");
                throw;
            }

            try
            {
                policyConfig.SetDefaultEndpoint(deviceId, ERole.eCommunications);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to set eCommunications role: {ex.Message}");
                throw;
            }

            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to set default audio device: {ex.Message}");
            return false;
        }
    }


    public static string ExtractCoreAudioDeviceId(DeviceInformation device)
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