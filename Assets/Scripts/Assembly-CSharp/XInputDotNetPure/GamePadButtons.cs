using System;

namespace XInputDotNetPure
{
	// Token: 0x020000BE RID: 190
	public struct GamePadButtons
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x000217E0 File Offset: 0x0001FBE0
		internal GamePadButtons(ButtonState start, ButtonState back, ButtonState leftStick, ButtonState rightStick, ButtonState leftShoulder, ButtonState rightShoulder, ButtonState a, ButtonState b, ButtonState x, ButtonState y)
		{
			this.start = start;
			this.back = back;
			this.leftStick = leftStick;
			this.rightStick = rightStick;
			this.leftShoulder = leftShoulder;
			this.rightShoulder = rightShoulder;
			this.a = a;
			this.b = b;
			this.x = x;
			this.y = y;
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0002183A File Offset: 0x0001FC3A
		public ButtonState Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00021842 File Offset: 0x0001FC42
		public ButtonState Back
		{
			get
			{
				return this.back;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0002184A File Offset: 0x0001FC4A
		public ButtonState LeftStick
		{
			get
			{
				return this.leftStick;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00021852 File Offset: 0x0001FC52
		public ButtonState RightStick
		{
			get
			{
				return this.rightStick;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0002185A File Offset: 0x0001FC5A
		public ButtonState LeftShoulder
		{
			get
			{
				return this.leftShoulder;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00021862 File Offset: 0x0001FC62
		public ButtonState RightShoulder
		{
			get
			{
				return this.rightShoulder;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0002186A File Offset: 0x0001FC6A
		public ButtonState A
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00021872 File Offset: 0x0001FC72
		public ButtonState B
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0002187A File Offset: 0x0001FC7A
		public ButtonState X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00021882 File Offset: 0x0001FC82
		public ButtonState Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x04000304 RID: 772
		private ButtonState start;

		// Token: 0x04000305 RID: 773
		private ButtonState back;

		// Token: 0x04000306 RID: 774
		private ButtonState leftStick;

		// Token: 0x04000307 RID: 775
		private ButtonState rightStick;

		// Token: 0x04000308 RID: 776
		private ButtonState leftShoulder;

		// Token: 0x04000309 RID: 777
		private ButtonState rightShoulder;

		// Token: 0x0400030A RID: 778
		private ButtonState a;

		// Token: 0x0400030B RID: 779
		private ButtonState b;

		// Token: 0x0400030C RID: 780
		private ButtonState x;

		// Token: 0x0400030D RID: 781
		private ButtonState y;
	}
}
