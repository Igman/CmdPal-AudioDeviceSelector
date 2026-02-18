using System;
using System.Runtime.InteropServices;

namespace CommandPallet.AudioDeviceSelector.Interop;

[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct PropArray
{
    internal uint cElems;
    internal IntPtr pElems;
}
