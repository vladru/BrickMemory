using System;
using System.Threading;
using MonoBrickFirmware.UserInput;

namespace BrickMemory
{
	public enum EV3Keys {Up, Down, Left, Right, Enter,Escape};

	public static class EV3KeyPad
	{
		private static EventWaitHandle stopped = new ManualResetEvent(false);
		private static ButtonEvents buttonEvents = new ButtonEvents();
		private static EV3Keys pressedKeyCode;

		public static EV3Keys ReadKey()
		{
			buttonEvents.EnterPressed += () => KeyPressed(EV3Keys.Enter);
			buttonEvents.EscapePressed += () => KeyPressed(EV3Keys.Escape);

			stopped.Reset ();
			stopped.WaitOne();

			buttonEvents.EnterPressed -= () => KeyPressed(EV3Keys.Enter);
			buttonEvents.EscapePressed -= () => KeyPressed (EV3Keys.Escape);

			return pressedKeyCode;
		}

		private static void KeyPressed(EV3Keys keyCode)
		{
			pressedKeyCode = keyCode;
			stopped.Set ();
		}
	}
}
	