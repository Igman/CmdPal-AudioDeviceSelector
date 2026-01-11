using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPallet.AudioDeviceSelector.Interop
{
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct PropArray
    {
        internal uint cElems;
        internal IntPtr pElems;
    }
}
