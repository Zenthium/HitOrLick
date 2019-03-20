using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000034 RID: 52
	public class TouchTrackControl : TouchControl
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000A51A File Offset: 0x0000891A
		public override void CreateControl()
		{
			this.ConfigureControl();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A522 File Offset: 0x00008922
		public override void DestroyControl()
		{
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A542 File Offset: 0x00008942
		public override void ConfigureControl()
		{
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A55B File Offset: 0x0000895B
		public override void DrawGizmos()
		{
			Utility.DrawRectGizmo(this.worldActiveArea, Color.yellow);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A56D File Offset: 0x0000896D
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A588 File Offset: 0x00008988
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			Vector3 a = this.thisPosition - this.lastPosition;
			base.SubmitRawAnalogValue(this.target, a * this.scale, updateTick, deltaTime);
			this.lastPosition = this.thisPosition;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A5D2 File Offset: 0x000089D2
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A5E0 File Offset: 0x000089E0
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			Vector3 point = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.worldActiveArea.Contains(point))
			{
				this.thisPosition = TouchManager.ScreenToViewPoint(touch.position * 100f);
				this.lastPosition = this.thisPosition;
				this.currentTouch = touch;
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A644 File Offset: 0x00008A44
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.thisPosition = TouchManager.ScreenToViewPoint(touch.position * 100f);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A66E File Offset: 0x00008A6E
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.thisPosition = Vector3.zero;
			this.lastPosition = Vector3.zero;
			this.currentTouch = null;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A69A File Offset: 0x00008A9A
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000A6A2 File Offset: 0x00008AA2
		public Rect ActiveArea
		{
			get
			{
				return this.activeArea;
			}
			set
			{
				if (this.activeArea != value)
				{
					this.activeArea = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A6C3 File Offset: 0x00008AC3
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000A6CB File Offset: 0x00008ACB
		public TouchUnitType AreaUnitType
		{
			get
			{
				return this.areaUnitType;
			}
			set
			{
				if (this.areaUnitType != value)
				{
					this.areaUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x04000211 RID: 529
		[Header("Dimensions")]
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x04000212 RID: 530
		[SerializeField]
		private Rect activeArea = new Rect(25f, 25f, 50f, 50f);

		// Token: 0x04000213 RID: 531
		[Header("Analog Target")]
		public TouchControl.AnalogTarget target = TouchControl.AnalogTarget.LeftStick;

		// Token: 0x04000214 RID: 532
		public float scale = 1f;

		// Token: 0x04000215 RID: 533
		private Rect worldActiveArea;

		// Token: 0x04000216 RID: 534
		private Vector3 lastPosition;

		// Token: 0x04000217 RID: 535
		private Vector3 thisPosition;

		// Token: 0x04000218 RID: 536
		private Touch currentTouch;

		// Token: 0x04000219 RID: 537
		private bool dirty;
	}
}
