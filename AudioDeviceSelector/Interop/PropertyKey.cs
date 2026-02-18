using System;
using System.Runtime.InteropServices;

namespace CommandPallet.AudioDeviceSelector.Interop;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
internal struct PROPERTYKEY
{
    public Guid fmtid;
    public System.UIntPtr pid;
}
