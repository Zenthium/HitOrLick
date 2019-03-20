using System;
using System.Runtime.InteropServices;

namespace XInputDotNetPure
{
	// Token: 0x020000BC RID: 188
	internal class Imports
	{
		// Token: 0x06000419 RID: 1049
		[DllImport("XInputInterface32", EntryPoint = "XInputGamePadGetState")]
		public static extern uint XInputGamePadGetState32(uint playerIndex, IntPtr state);

		// Token: 0x0600041A RID: 1050
		[DllImport("XInputInterface32", EntryPoint = "XInputGamePadSetState")]
		public static extern void XInputGamePadSetState32(uint playerIndex, float leftMotor, float rightMotor);

		// Token: 0x0600041B RID: 1051
		[DllImport("XInputInterface64", EntryPoint = "XInputGamePadGetState")]
		public static extern uint XInputGamePadGetState64(uint playerIndex, IntPtr state);

		// Token: 0x0600041C RID: 1052
		[DllImport("XInputInterface64", EntryPoint = "XInputGamePadSetState")]
		public static extern void XInputGamePadSetState64(uint playerIndex, float leftMotor, float rightMotor);

		// Token: 0x0600041D RID: 1053 RVA: 0x000217A0 File Offset: 0x0001FBA0
		public static uint XInputGamePadGetState(uint playerIndex, IntPtr state)
		{
			if (IntPtr.Size == 4)
			{
				return Imports.XInputGamePadGetState32(playerIndex, state);
			}
			return Imports.XInputGamePadGetState64(playerIndex, state);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000217BC File Offset: 0x0001FBBC
		public static void XInputGamePadSetState(uint playerIndex, float leftMotor, float rightMotor)
		{
			if (IntPtr.Size == 4)
			{
				Imports.XInputGamePadSetState32(playerIndex, leftMotor, rightMotor);
			}
			else
			{
				Imports.XInputGamePadSetState64(playerIndex, leftMotor, rightMotor);
			}
		}
	}
}
