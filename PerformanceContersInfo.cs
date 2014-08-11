using System;
using System.Diagnostics;
using MonoBrickFirmware.Display;

namespace BrickMemory
{
	public static class PerformanceContersInfo
	{
		public static void Display ()
		{
			LcdConsole.Clear();

			// http://stackoverflow.com/questions/105031/how-do-you-get-total-amount-of-ram-the-computer-has
			// http://stackoverflow.com/questions/3896685/simplest-possible-performance-counter-example
			foreach (var pcCategory in PerformanceCounterCategory.GetCategories())
			{
				LcdConsole.Clear ();
				LcdConsole.WriteLine (pcCategory.CategoryName);
				int lineNum = 1;
				int pageNum = 1;
				foreach (var performanceCounter in pcCategory.GetCounters())
				{
					LcdConsole.WriteLine (performanceCounter.CounterName);
					LcdConsole.WriteLine ("{0}",performanceCounter.RawValue);
					if ((++lineNum % 5) == 0)
					{
						LcdConsole.WriteLine ("-----{0} page {1}", pcCategory.CategoryName, pageNum++);
						EV3KeyPad.ReadKey ();
					}
				}
				LcdConsole.WriteLine ("-----End of {0}", pcCategory.CategoryName);
				EV3KeyPad.ReadKey ();
			}
		}
	}
}

