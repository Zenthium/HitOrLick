using System;

namespace XInputDotNetPure
{
	// Token: 0x020000C2 RID: 194
	public struct GamePadTriggers
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0002191A File Offset: 0x0001FD1A
		internal GamePadTriggers(float left, float right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0002192A File Offset: 0x0001FD2A
		public float Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00021932 File Offset: 0x0001FD32
		public float Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000315 RID: 789
		private float left;

		// Token: 0x04000316 RID: 790
		private float right;
	}
}
