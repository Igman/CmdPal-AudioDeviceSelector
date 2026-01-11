// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommandPallet.AudioDeviceSelector.Interop;

/// <summary>
/// Represents the form factor of an audio endpoint device.
/// Maps to the PKEY_AudioEndpoint_FormFactor property values.
/// </summary>
internal enum EndpointFormFactor
{
    RemoteNetworkDevice = 0,
    Speakers = 1,
    LineLevel = 2,
    Headphones = 3,
    Microphone = 4,
    Headset = 5,
    Handset = 6,
    UnknownDigitalPassthrough = 7,
    SPDIF = 8,
  DigitalAudioDisplayDevice = 9,
  UnknownFormFactor = 10
}
