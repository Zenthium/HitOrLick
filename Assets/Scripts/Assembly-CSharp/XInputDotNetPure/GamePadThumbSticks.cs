using System;
using UnityEngine;

namespace XInputDotNetPure
{
	// Token: 0x020000C0 RID: 192
	public struct GamePadThumbSticks
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x000218C9 File Offset: 0x0001FCC9
		internal GamePadThumbSticks(GamePadThumbSticks.StickValue left, GamePadThumbSticks.StickValue right)
		{
			this.left = left;
			this.right = right;
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000218D9 File Offset: 0x0001FCD9
		public GamePadThumbSticks.StickValue Left
		{
			get
			{
				return this.left;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000218E1 File Offset: 0x0001FCE1
		public GamePadThumbSticks.StickValue Right
		{
			get
			{
				return this.right;
			}
		}

		// Token: 0x04000312 RID: 786
		private GamePadThumbSticks.StickValue left;

		// Token: 0x04000313 RID: 787
		private GamePadThumbSticks.StickValue right;

		// Token: 0x020000C1 RID: 193
		public struct StickValue
		{
			// Token: 0x06000432 RID: 1074 RVA: 0x000218E9 File Offset: 0x0001FCE9
			internal StickValue(float x, float y)
			{
				this.vector = new Vector2(x, y);
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x000218F8 File Offset: 0x0001FCF8
			public float X
			{
				get
				{
					return this.vector.x;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x06000434 RID: 1076 RVA: 0x00021905 File Offset: 0x0001FD05
			public float Y
			{
				get
				{
					return this.vector.y;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x00021912 File Offset: 0x0001FD12
			public Vector2 Vector
			{
				get
				{
					return this.vector;
				}
			}

			// Token: 0x04000314 RID: 788
			private Vector2 vector;
		}
	}
}
