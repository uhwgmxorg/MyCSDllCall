/******************************************************************************/
/*                                                                            */
/*   Program: MyCSDllCall                                                     */
/*   Calling Win32 Dlls from .Net                                             */
/*                                                                            */
/*   03.08.2014 0.0.0.0 uhwgmxorg Start                                       */
/*                                                                            */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCSDllCall
{
    class Program
    {
        static Random _random;
        const int FWAVELENGTH_SIZE = 20;

        /// <summary>
        /// RandomDouble
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="deci"></param>
        /// <returns></returns>
        public static double RandomDouble(double min, double max, int deci)
        {
            double d;
            d = _random.NextDouble() * (max - min) + min;
            return Math.Round(d, deci);
        }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Programm MyCSDllCall\n");

            _random = new Random();

            // MyCPPDll.dll
            Console.WriteLine("The version of MyCPPDll.dll (C++ classes) is:");
            IntPtr obi = Win32Wrapper.ClassICrate();

            IntPtr pString = Win32Wrapper.ClassIGetVersion(obi);
            string Version = Marshal.PtrToStringAnsi(pString);
            Console.WriteLine(Version);
            Console.WriteLine();

            Win32Wrapper.ClassIDelete(obi);


            // MyCDll.dll
            // GetDllVersion
            Console.WriteLine("The version of MyCDll.dll (C functions) is:");
            pString = Win32Wrapper.GetDllVersion();
            Version = Marshal.PtrToStringAnsi(pString);
            Console.WriteLine(Version);

            // OpenCWSProject
            Console.WriteLine("and other functions from MyCDll.dll");
            StringBuilder FileName = new StringBuilder("Data.dat");
            Int16 rc2 = Win32Wrapper.OpenCWSProject(FileName);

            // PredictSample_C
            StringBuilder CalibName = new StringBuilder("PLS_Wasser");
            int nArraySize = FWAVELENGTH_SIZE;
            double[] fWavelength = new double[FWAVELENGTH_SIZE]; for (int i = 0; i < FWAVELENGTH_SIZE; i++) fWavelength[i] = i + 850;
            double[] fY = new double[FWAVELENGTH_SIZE]; for (int i = 0; i < FWAVELENGTH_SIZE; i++) fY[i] = RandomDouble(0, 1, 8);
            short fTypeY = 0;
            Int16[] fFlags = new Int16[3];
            double[] fResults = new double[3];
            Int16 rc3 = Win32Wrapper.PredictSample_C(CalibName, nArraySize, fWavelength, fY, fTypeY, fFlags, fResults);
            string Flags = fFlags[0].ToString() + " " + fFlags[1].ToString() + " " + fFlags[2].ToString();
            Console.WriteLine(Flags);
            // Using CreateSpecificCulture("en-US") just for getting 1.1 instead of 1,1
            // on non  US OS-Systems to get conform to MyCPPDllCall.exe in prior sample
            string Results = fResults[0].ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " " + fResults[1].ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US")) + " " + fResults[2].ToString(System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
            Console.WriteLine(Results);

            // CloseDatabase
            Win32Wrapper.CloseDatabase();

            Console.WriteLine("\n\nHit any key to exit");
            Console.ReadKey();
        }
    }
}
