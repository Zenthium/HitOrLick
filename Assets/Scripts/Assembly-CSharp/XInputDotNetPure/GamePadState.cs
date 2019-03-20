using System;

namespace XInputDotNetPure
{
	// Token: 0x020000C3 RID: 195
	public struct GamePadState
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x0002193C File Offset: 0x0001FD3C
		internal GamePadState(bool isConnected, GamePadState.RawState rawState)
		{
			this.isConnected = isConnected;
			if (!isConnected)
			{
				rawState.dwPacketNumber = 0u;
				rawState.Gamepad.dwButtons = 0;
				rawState.Gamepad.bLeftTrigger = 0;
				rawState.Gamepad.bRightTrigger = 0;
				rawState.Gamepad.sThumbLX = 0;
				rawState.Gamepad.sThumbLY = 0;
				rawState.Gamepad.sThumbRX = 0;
				rawState.Gamepad.sThumbRY = 0;
			}
			this.packetNumber = rawState.dwPacketNumber;
			this.buttons = new GamePadButtons(((rawState.Gamepad.dwButtons & 16) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 32) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 64) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 128) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 256) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 512) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 4096) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 8192) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 16384) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 32768) == 0) ? ButtonState.Released : ButtonState.Pressed);
			this.dPad = new GamePadDPad(((rawState.Gamepad.dwButtons & 1) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 2) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 4) == 0) ? ButtonState.Released : ButtonState.Pressed, ((rawState.Gamepad.dwButtons & 8) == 0) ? ButtonState.Released : ButtonState.Pressed);
			this.thumbSticks = new GamePadThumbSticks(new GamePadThumbSticks.StickValue((float)rawState.Gamepad.sThumbLX / 32767f, (float)rawState.Gamepad.sThumbLY / 32767f), new GamePadThumbSticks.StickValue((float)rawState.Gamepad.sThumbRX / 32767f, (float)rawState.Gamepad.sThumbRY / 32767f));
			this.triggers = new GamePadTriggers((float)rawState.Gamepad.bLeftTrigger / 255f, (float)rawState.Gamepad.bRightTrigger / 255f);
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00021BF9 File Offset: 0x0001FFF9
		public uint PacketNumber
		{
			get
			{
				return this.packetNumber;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00021C01 File Offset: 0x00020001
		public bool IsConnected
		{
			get
			{
				return this.isConnected;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00021C09 File Offset: 0x00020009
		public GamePadButtons Buttons
		{
			get
			{
				return this.buttons;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00021C11 File Offset: 0x00020011
		public GamePadDPad DPad
		{
			get
			{
				return this.dPad;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00021C19 File Offset: 0x00020019
		public GamePadTriggers Triggers
		{
			get
			{
				return this.triggers;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00021C21 File Offset: 0x00020021
		public GamePadThumbSticks ThumbSticks
		{
			get
			{
				return this.thumbSticks;
			}
		}

		// Token: 0x04000317 RID: 791
		private bool isConnected;

		// Token: 0x04000318 RID: 792
		private uint packetNumber;

		// Token: 0x04000319 RID: 793
		private GamePadButtons buttons;

		// Token: 0x0400031A RID: 794
		private GamePadDPad dPad;

		// Token: 0x0400031B RID: 795
		private GamePadThumbSticks thumbSticks;

		// Token: 0x0400031C RID: 796
		private GamePadTriggers triggers;

		// Token: 0x020000C4 RID: 196
		internal struct RawState
		{
			// Token: 0x0400031D RID: 797
			public uint dwPacketNumber;

			// Token: 0x0400031E RID: 798
			public GamePadState.RawState.GamePad Gamepad;

			// Token: 0x020000C5 RID: 197
			public struct GamePad
			{
				// Token: 0x0400031F RID: 799
				public ushort dwButtons;

				// Token: 0x04000320 RID: 800
				public byte bLeftTrigger;

				// Token: 0x04000321 RID: 801
				public byte bRightTrigger;

				// Token: 0x04000322 RID: 802
				public short sThumbLX;

				// Token: 0x04000323 RID: 803
				public short sThumbLY;

				// Token: 0x04000324 RID: 804
				public short sThumbRX;

				// Token: 0x04000325 RID: 805
				public short sThumbRY;
			}
		}

		// Token: 0x020000C6 RID: 198
		private enum ButtonsConstants
		{
			// Token: 0x04000327 RID: 807
			DPadUp = 1,
			// Token: 0x04000328 RID: 808
			DPadDown,
			// Token: 0x04000329 RID: 809
			DPadLeft = 4,
			// Token: 0x0400032A RID: 810
			DPadRight = 8,
			// Token: 0x0400032B RID: 811
			Start = 16,
			// Token: 0x0400032C RID: 812
			Back = 32,
			// Token: 0x0400032D RID: 813
			LeftThumb = 64,
			// Token: 0x0400032E RID: 814
			RightThumb = 128,
			// Token: 0x0400032F RID: 815
			LeftShoulder = 256,
			// Token: 0x04000330 RID: 816
			RightShoulder = 512,
			// Token: 0x04000331 RID: 817
			A = 4096,
			// Token: 0x04000332 RID: 818
			B = 8192,
			// Token: 0x04000333 RID: 819
			X = 16384,
			// Token: 0x04000334 RID: 820
			Y = 32768
		}
	}
}
