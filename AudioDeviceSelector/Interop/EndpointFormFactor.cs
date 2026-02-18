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
