using System;
using System.Runtime.InteropServices;

namespace CommandPalette.AudioDeviceSelector.Interop;

[ComImport]
[Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
internal class PolicyConfigClient { }

internal class PolicyConfigClientWin7
{
    private readonly IPolicyConfigWin7 _policyClient = (IPolicyConfigWin7)new PolicyConfigClient();

    public void SetEndpointVisibility(string deviceId, bool isVisible)
    {
        _policyClient.SetEndpointVisibility(deviceId, isVisible ? (short)1 : (short)0);
    }

    public void SetDefaultEndpoint(string deviceId, ERole role = ERole.eMultimedia)
    {
        _policyClient.SetDefaultEndpoint(deviceId, role);
    }
}
