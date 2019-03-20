using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000035 RID: 53
	public class Touch
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0000A6E7 File Offset: 0x00008AE7
		internal Touch(int fingerId)
		{
			this.fingerId = fingerId;
			this.phase = TouchPhase.Ended;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A700 File Offset: 0x00008B00
		internal void SetWithTouchData(UnityEngine.Touch touch, ulong updateTick, float deltaTime)
		{
			this.phase = touch.phase;
			this.tapCount = touch.tapCount;
			Vector2 a = touch.position;
			if (a.x < 0f)
			{
				a.x = (float)Screen.width + a.x;
			}
			if (this.phase == TouchPhase.Began)
			{
				this.deltaPosition = Vector2.zero;
				this.lastPosition = a;
				this.position = a;
			}
			else
			{
				if (this.phase == TouchPhase.Stationary)
				{
					this.phase = TouchPhase.Moved;
				}
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
			}
			this.deltaTime = deltaTime;
			this.updateTick = updateTick;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A7C4 File Offset: 0x00008BC4
		internal bool SetWithMouseData(ulong updateTick, float deltaTime)
		{
			if (Input.touchCount > 0)
			{
				return false;
			}
			Vector2 a = new Vector2(Mathf.Round(Input.mousePosition.x), Mathf.Round(Input.mousePosition.y));
			if (Input.GetMouseButtonDown(0))
			{
				this.phase = TouchPhase.Began;
				this.tapCount = 1;
				this.deltaPosition = Vector2.zero;
				this.lastPosition = a;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			if (Input.GetMouseButtonUp(0))
			{
				this.phase = TouchPhase.Ended;
				this.tapCount = 1;
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			if (Input.GetMouseButton(0))
			{
				this.phase = TouchPhase.Moved;
				this.tapCount = 1;
				this.deltaPosition = a - this.lastPosition;
				this.lastPosition = this.position;
				this.position = a;
				this.deltaTime = deltaTime;
				this.updateTick = updateTick;
				return true;
			}
			return false;
		}

		// Token: 0x0400021A RID: 538
		public int fingerId;

		// Token: 0x0400021B RID: 539
		public TouchPhase phase;

		// Token: 0x0400021C RID: 540
		public int tapCount;

		// Token: 0x0400021D RID: 541
		public Vector2 position;

		// Token: 0x0400021E RID: 542
		public Vector2 deltaPosition;

		// Token: 0x0400021F RID: 543
		public Vector2 lastPosition;

		// Token: 0x04000220 RID: 544
		public float deltaTime;

		// Token: 0x04000221 RID: 545
		public ulong updateTick;
	}
}
