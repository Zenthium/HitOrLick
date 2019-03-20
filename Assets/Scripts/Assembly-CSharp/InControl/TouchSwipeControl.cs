using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000033 RID: 51
	public class TouchSwipeControl : TouchControl
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0000A07F File Offset: 0x0000847F
		public override void CreateControl()
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000A081 File Offset: 0x00008481
		public override void DestroyControl()
		{
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A0A1 File Offset: 0x000084A1
		public override void ConfigureControl()
		{
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A0BA File Offset: 0x000084BA
		public override void DrawGizmos()
		{
			Utility.DrawRectGizmo(this.worldActiveArea, Color.yellow);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A0CC File Offset: 0x000084CC
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A0E8 File Offset: 0x000084E8
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			Vector2 value = TouchControl.SnapTo(this.currentVector, this.snapAngles);
			base.SubmitAnalogValue(this.target, value, 0f, 1f, updateTick, deltaTime);
			base.SubmitButtonState(this.upTarget, this.fireButtonTarget && this.nextButtonTarget == this.upTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.downTarget, this.fireButtonTarget && this.nextButtonTarget == this.downTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.leftTarget, this.fireButtonTarget && this.nextButtonTarget == this.leftTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.rightTarget, this.fireButtonTarget && this.nextButtonTarget == this.rightTarget, updateTick, deltaTime);
			base.SubmitButtonState(this.tapTarget, this.fireButtonTarget && this.nextButtonTarget == this.tapTarget, updateTick, deltaTime);
			if (this.fireButtonTarget && this.nextButtonTarget != TouchControl.ButtonTarget.None)
			{
				this.fireButtonTarget = !this.oneSwipePerTouch;
				this.lastButtonTarget = this.nextButtonTarget;
				this.nextButtonTarget = TouchControl.ButtonTarget.None;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000A230 File Offset: 0x00008630
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
			base.CommitButton(this.upTarget);
			base.CommitButton(this.downTarget);
			base.CommitButton(this.leftTarget);
			base.CommitButton(this.rightTarget);
			base.CommitButton(this.tapTarget);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000A288 File Offset: 0x00008688
		public override void TouchBegan(Touch touch)
		{
			if (this.currentTouch != null)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			if (this.worldActiveArea.Contains(this.beganPosition))
			{
				this.lastPosition = this.beganPosition;
				this.currentTouch = touch;
				this.currentVector = Vector2.zero;
				this.fireButtonTarget = true;
				this.nextButtonTarget = TouchControl.ButtonTarget.None;
				this.lastButtonTarget = TouchControl.ButtonTarget.None;
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000A300 File Offset: 0x00008700
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			Vector3 a = TouchManager.ScreenToWorldPoint(touch.position);
			Vector3 vector = a - this.lastPosition;
			if (vector.magnitude >= this.sensitivity)
			{
				this.lastPosition = a;
				this.currentVector = vector.normalized;
				if (this.fireButtonTarget)
				{
					TouchControl.ButtonTarget buttonTargetForVector = this.GetButtonTargetForVector(this.currentVector);
					if (buttonTargetForVector != this.lastButtonTarget)
					{
						this.nextButtonTarget = buttonTargetForVector;
					}
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000A38C File Offset: 0x0000878C
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.currentTouch = null;
			this.currentVector = Vector2.zero;
			Vector3 b = TouchManager.ScreenToWorldPoint(touch.position);
			if ((this.beganPosition - b).magnitude < this.sensitivity)
			{
				this.fireButtonTarget = true;
				this.nextButtonTarget = this.tapTarget;
				this.lastButtonTarget = TouchControl.ButtonTarget.None;
				return;
			}
			this.fireButtonTarget = false;
			this.nextButtonTarget = TouchControl.ButtonTarget.None;
			this.lastButtonTarget = TouchControl.ButtonTarget.None;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000A418 File Offset: 0x00008818
		private TouchControl.ButtonTarget GetButtonTargetForVector(Vector2 vector)
		{
			Vector2 lhs = TouchControl.SnapTo(vector, TouchControl.SnapAngles.Four);
			if (lhs == Vector2.up)
			{
				return this.upTarget;
			}
			if (lhs == Vector2.right)
			{
				return this.rightTarget;
			}
			if (lhs == -Vector2.up)
			{
				return this.downTarget;
			}
			if (lhs == -Vector2.right)
			{
				return this.leftTarget;
			}
			return TouchControl.ButtonTarget.None;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A494 File Offset: 0x00008894
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A49C File Offset: 0x0000889C
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

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A4BD File Offset: 0x000088BD
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A4C5 File Offset: 0x000088C5
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

		// Token: 0x040001FD RID: 509
		[Header("Position")]
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x040001FE RID: 510
		[SerializeField]
		private Rect activeArea = new Rect(25f, 25f, 50f, 50f);

		// Token: 0x040001FF RID: 511
		[Range(0f, 1f)]
		public float sensitivity = 0.1f;

		// Token: 0x04000200 RID: 512
		[Header("Analog Target")]
		public TouchControl.AnalogTarget target;

		// Token: 0x04000201 RID: 513
		public TouchControl.SnapAngles snapAngles;

		// Token: 0x04000202 RID: 514
		[Header("Button Targets")]
		public TouchControl.ButtonTarget upTarget;

		// Token: 0x04000203 RID: 515
		public TouchControl.ButtonTarget downTarget;

		// Token: 0x04000204 RID: 516
		public TouchControl.ButtonTarget leftTarget;

		// Token: 0x04000205 RID: 517
		public TouchControl.ButtonTarget rightTarget;

		// Token: 0x04000206 RID: 518
		public TouchControl.ButtonTarget tapTarget;

		// Token: 0x04000207 RID: 519
		public bool oneSwipePerTouch;

		// Token: 0x04000208 RID: 520
		private Rect worldActiveArea;

		// Token: 0x04000209 RID: 521
		private Vector3 currentVector;

		// Token: 0x0400020A RID: 522
		private Vector3 beganPosition;

		// Token: 0x0400020B RID: 523
		private Vector3 lastPosition;

		// Token: 0x0400020C RID: 524
		private Touch currentTouch;

		// Token: 0x0400020D RID: 525
		private bool fireButtonTarget;

		// Token: 0x0400020E RID: 526
		private TouchControl.ButtonTarget nextButtonTarget;

		// Token: 0x0400020F RID: 527
		private TouchControl.ButtonTarget lastButtonTarget;

		// Token: 0x04000210 RID: 528
		private bool dirty;
	}
}
