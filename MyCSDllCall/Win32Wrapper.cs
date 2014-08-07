using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCSDllCall
{
    class Win32Wrapper
    {
        [DllImport("MyCPPDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ClassICrate();
        [DllImport("MyCPPDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ClassIGetVersion(IntPtr value);
        [DllImport("MyCPPDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ClassIDelete(IntPtr value);

        [DllImport("MyCDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr GetDllVersion();
        [DllImport("MyCDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static Int16 OpenCWSProject(StringBuilder Filename);
        [DllImport("MyCDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static Int16 PredictSample_C(StringBuilder CalibName, int nArraySize, double[] fWavelength, double[] fY, short fTypeY, Int16[] fFlags, double[] fResults);
        [DllImport("MyCDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static void CloseDatabase();
    }
}
