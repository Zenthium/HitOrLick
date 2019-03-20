using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
	// Token: 0x020000C8 RID: 200
	public class GamePad
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x00021C34 File Offset: 0x00020034
		public static GamePadState GetState(PlayerIndex playerIndex)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(GamePadState.RawState)));
			uint num = Imports.XInputGamePadGetState((uint)playerIndex, intPtr);
			GamePadState.RawState rawState = (GamePadState.RawState)Marshal.PtrToStructure(intPtr, typeof(GamePadState.RawState));
			return new GamePadState(num == 0u, rawState);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00021C7E File Offset: 0x0002007E
		public static void SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
		{
			Imports.XInputGamePadSetState((uint)playerIndex, leftMotor, rightMotor);
		}
	}
}
