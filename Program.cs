using System;
using System.Diagnostics;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.UserInput;

namespace BrickMemory
{
	public static class Program
	{
		static int[] tst;

		public static void Main (string[] args)
		{

			// http://msdn.microsoft.com/en-us/library/s80a75e5%28VS.80%29.aspx
			// The number of bytes that the associated process has allocated
			// that cannot be shared with other processes.
			Process proc = Process.GetCurrentProcess();
			LcdConsole.WriteLine("PrivateMemorySize {0:N0}", proc.PrivateMemorySize64);

			// http://msdn.microsoft.com/en-us/library/system.gc.gettotalmemory.aspx
			// A number that is the best available approximation of the number of 
			// bytes currently allocated in managed memory.
			LcdConsole.WriteLine("GC.GetTotalMemory {0:N0}", GC.GetTotalMemory(false));

			// http://mono-project.com/Mono_Performance_Counters
			//OutPerformanceCounterValue("Process", "Virtual Bytes");
			//OutPerformanceCounterValue("Process", "Private Bytes");
			OutPerformanceCounterValue("Mono Memory", "Total Physical Memory");
			//OutPerformanceCounterValue(".NET CLR Memory", "# Bytes in all Heaps");

			EV3KeyPad.ReadKey ();

			LcdConsole.Clear ();

			var MBytes = 42;
			var bytes = MBytes * 1024 * 1024;
			var intsize = sizeof(int);

			LcdConsole.WriteLine ("Allocate {0}Mb of memory.", MBytes);
			LcdConsole.WriteLine ("Please wait...");
			tst = new int[ bytes/intsize ];

			MBytes = 0;
			for (int i=0; i < bytes/intsize; i++)
			{
				tst[i] = i;
				if ((i * intsize % (1024 * 1024)) == 0)
				{
					LcdConsole.WriteLine("{0} MBytes used", ++MBytes);
				}
			}
			LcdConsole.WriteLine("Finished!");
			LcdConsole.WriteLine("Bytes allocated: {0:N0}", GC.GetTotalMemory(false));

			//EV3KeyPad.ReadKey ();
			//PerformanceContersInfo.Display ();

			EV3KeyPad.ReadKey ();
			LcdConsole.WriteLine ("Memory deallocation.");
			LcdConsole.WriteLine ("Please wait...");
		}

		private static void OutPerformanceCounterValue( string categoryName, string counterName)
		{
			var pc = new PerformanceCounter(categoryName, counterName);
			LcdConsole.WriteLine("{0} ({1})", counterName, categoryName);
			LcdConsole.WriteLine("{0:N0}", pc.RawValue);
		}
	}
}