using System;

namespace XInputDotNetPure
{
	// Token: 0x020000BF RID: 191
	public struct GamePadDPad
	{
		// Token: 0x0600042A RID: 1066 RVA: 0x0002188A File Offset: 0x0001FC8A
		internal GamePadDPad(ButtonState up, ButtonState down, ButtonState left, ButtonState right)
		{
			this.up = up;
			this.down = down;
			this.left = left;
			this.right = right;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000218A9 File Offset: 0x0001FCA9
		public ButtonState Up
		{
			get
			{
				return this.up;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000218B1 File Offset: 0x0001FCB1
		public ButtonState Down
		{
			get
			{
				return this.down;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x000218B9 File Offset: 0x0001FCB9
		public ButtonState Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000218C1 File Offset: 0x0001FCC1
		public ButtonState Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x0400030E RID: 782
		private ButtonState up;

		// Token: 0x0400030F RID: 783
		private ButtonState down;

		// Token: 0x04000310 RID: 784
		private ButtonState left;

		// Token: 0x04000311 RID: 785
		private ButtonState right;
	}
}
