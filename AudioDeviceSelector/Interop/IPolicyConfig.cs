using System;
using System.Runtime.InteropServices;

namespace CommandPalette.AudioDeviceSelector.Interop;

// W10_TH1: CA286FC3-91FD-42C3-8E9B-CAAFA66242E3
// W10_TH2: 6BE54BE8-A068-4875-A49D-0C2966473B11
// Win7-Win8, W10_RS1-Present:
[Guid("f8679f50-850a-41cf-9c72-430f290290c8")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IPolicyConfigWin7
{
    // COM vtable slots 3-10 (IUnknown occupies 0-2); these must exist to maintain correct vtable ordering.
    void Unused1();
    void Unused2();
    void Unused3();
    void Unused4();
    void Unused5();
    void Unused6();
    void Unused7();
    void Unused8();
    void GetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string wszDeviceId, ref PROPERTYKEY pkey, ref PropVariant pv);
    void SetPropertyValue([MarshalAs(UnmanagedType.LPWStr)] string wszDeviceId, ref PROPERTYKEY pkey, ref PropVariant pv);
    void SetDefaultEndpoint([MarshalAs(UnmanagedType.LPWStr)] string wszDeviceId, ERole eRole);
    void SetEndpointVisibility([MarshalAs(UnmanagedType.LPWStr)] string wszDeviceId, [MarshalAs(UnmanagedType.I2)] short isVisible);
}
