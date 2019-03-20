using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000032 RID: 50
	public class TouchStickControl : TouchControl
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000098EA File Offset: 0x00007CEA
		public override void CreateControl()
		{
			this.ring.Create("Ring", base.transform, 1000);
			this.knob.Create("Knob", base.transform, 1001);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009922 File Offset: 0x00007D22
		public override void DestroyControl()
		{
			this.ring.Delete();
			this.knob.Delete();
			if (this.currentTouch != null)
			{
				this.TouchEnded(this.currentTouch);
				this.currentTouch = null;
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009958 File Offset: 0x00007D58
		public override void ConfigureControl()
		{
			this.resetPosition = base.OffsetToWorldPosition(this.anchor, this.offset, this.offsetUnitType, true);
			base.transform.position = this.resetPosition;
			this.ring.Update(true);
			this.knob.Update(true);
			this.worldActiveArea = TouchManager.ConvertToWorld(this.activeArea, this.areaUnitType);
			this.worldKnobRange = TouchManager.ConvertToWorld(this.knobRange, this.knob.SizeUnitType);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000099E0 File Offset: 0x00007DE0
		public override void DrawGizmos()
		{
			this.ring.DrawGizmos(this.RingPosition, Color.yellow);
			this.knob.DrawGizmos(this.KnobPosition, Color.yellow);
			Utility.DrawCircleGizmo(this.RingPosition, this.worldKnobRange, Color.red);
			Utility.DrawRectGizmo(this.worldActiveArea, Color.green);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00009A44 File Offset: 0x00007E44
		private void Update()
		{
			if (this.dirty)
			{
				this.ConfigureControl();
				this.dirty = false;
			}
			else
			{
				this.ring.Update();
				this.knob.Update();
			}
			if (this.IsNotActive)
			{
				if (this.resetWhenDone && this.KnobPosition != this.resetPosition)
				{
					Vector3 b = this.KnobPosition - this.RingPosition;
					this.RingPosition = Vector3.MoveTowards(this.RingPosition, this.resetPosition, this.ringResetSpeed * Time.deltaTime);
					this.KnobPosition = this.RingPosition + b;
				}
				if (this.KnobPosition != this.RingPosition)
				{
					this.KnobPosition = Vector3.MoveTowards(this.KnobPosition, this.RingPosition, this.knobResetSpeed * Time.deltaTime);
				}
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00009B30 File Offset: 0x00007F30
		public override void SubmitControlState(ulong updateTick, float deltaTime)
		{
			base.SubmitAnalogValue(this.target, this.snappedValue, this.lowerDeadZone, this.upperDeadZone, updateTick, deltaTime);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009B57 File Offset: 0x00007F57
		public override void CommitControlState(ulong updateTick, float deltaTime)
		{
			base.CommitAnalog(this.target);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009B68 File Offset: 0x00007F68
		public override void TouchBegan(Touch touch)
		{
			if (this.IsActive)
			{
				return;
			}
			this.beganPosition = TouchManager.ScreenToWorldPoint(touch.position);
			bool flag = this.worldActiveArea.Contains(this.beganPosition);
			bool flag2 = this.ring.Contains(this.beganPosition);
			if (this.snapToInitialTouch && (flag || flag2))
			{
				this.RingPosition = this.beganPosition;
				this.KnobPosition = this.beganPosition;
				this.currentTouch = touch;
			}
			else if (flag2)
			{
				this.KnobPosition = this.beganPosition;
				this.beganPosition = this.RingPosition;
				this.currentTouch = touch;
			}
			if (this.IsActive)
			{
				this.TouchMoved(touch);
				this.ring.State = true;
				this.knob.State = true;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009C48 File Offset: 0x00008048
		public override void TouchMoved(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.movedPosition = TouchManager.ScreenToWorldPoint(touch.position);
			Vector3 vector = this.movedPosition - this.beganPosition;
			Vector3 normalized = vector.normalized;
			float magnitude = vector.magnitude;
			if (this.allowDragging)
			{
				float num = magnitude - this.worldKnobRange;
				if (num < 0f)
				{
					num = 0f;
				}
				this.beganPosition += num * normalized;
				this.RingPosition = this.beganPosition;
			}
			this.movedPosition = this.beganPosition + Mathf.Clamp(magnitude, 0f, this.worldKnobRange) * normalized;
			this.value = (this.movedPosition - this.beganPosition) / this.worldKnobRange;
			this.value.x = this.inputCurve.Evaluate(Utility.Abs(this.value.x)) * Mathf.Sign(this.value.x);
			this.value.y = this.inputCurve.Evaluate(Utility.Abs(this.value.y)) * Mathf.Sign(this.value.y);
			if (this.snapAngles == TouchControl.SnapAngles.None)
			{
				this.snappedValue = this.value;
				this.KnobPosition = this.movedPosition;
			}
			else
			{
				this.snappedValue = TouchControl.SnapTo(this.value, this.snapAngles);
				this.KnobPosition = this.beganPosition + this.snappedValue * this.worldKnobRange;
			}
			this.RingPosition = this.beganPosition;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009E10 File Offset: 0x00008210
		public override void TouchEnded(Touch touch)
		{
			if (this.currentTouch != touch)
			{
				return;
			}
			this.value = Vector3.zero;
			this.snappedValue = Vector3.zero;
			float magnitude = (this.resetPosition - this.RingPosition).magnitude;
			this.ringResetSpeed = ((!Utility.IsZero(this.resetDuration)) ? (magnitude / this.resetDuration) : magnitude);
			float magnitude2 = (this.RingPosition - this.KnobPosition).magnitude;
			this.knobResetSpeed = ((!Utility.IsZero(this.resetDuration)) ? (magnitude2 / this.resetDuration) : this.knobRange);
			this.currentTouch = null;
			this.ring.State = false;
			this.knob.State = false;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00009EE0 File Offset: 0x000082E0
		public bool IsActive
		{
			get
			{
				return this.currentTouch != null;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009EEE File Offset: 0x000082EE
		public bool IsNotActive
		{
			get
			{
				return this.currentTouch == null;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00009EF9 File Offset: 0x000082F9
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00009F26 File Offset: 0x00008326
		public Vector3 RingPosition
		{
			get
			{
				return (!this.ring.Ready) ? base.transform.position : this.ring.Position;
			}
			set
			{
				if (this.ring.Ready)
				{
					this.ring.Position = value;
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009F44 File Offset: 0x00008344
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00009F71 File Offset: 0x00008371
		public Vector3 KnobPosition
		{
			get
			{
				return (!this.knob.Ready) ? base.transform.position : this.knob.Position;
			}
			set
			{
				if (this.knob.Ready)
				{
					this.knob.Position = value;
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009F8F File Offset: 0x0000838F
		// (set) Token: 0x0600023C RID: 572 RVA: 0x00009F97 File Offset: 0x00008397
		public TouchControlAnchor Anchor
		{
			get
			{
				return this.anchor;
			}
			set
			{
				if (this.anchor != value)
				{
					this.anchor = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00009FB3 File Offset: 0x000083B3
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00009FBB File Offset: 0x000083BB
		public Vector2 Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				if (this.offset != value)
				{
					this.offset = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00009FDC File Offset: 0x000083DC
		// (set) Token: 0x06000240 RID: 576 RVA: 0x00009FE4 File Offset: 0x000083E4
		public TouchUnitType OffsetUnitType
		{
			get
			{
				return this.offsetUnitType;
			}
			set
			{
				if (this.offsetUnitType != value)
				{
					this.offsetUnitType = value;
					this.dirty = true;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A000 File Offset: 0x00008400
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000A008 File Offset: 0x00008408
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A029 File Offset: 0x00008429
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A031 File Offset: 0x00008431
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

		// Token: 0x040001E1 RID: 481
		[Header("Position")]
		[SerializeField]
		private TouchControlAnchor anchor = TouchControlAnchor.BottomLeft;

		// Token: 0x040001E2 RID: 482
		[SerializeField]
		private TouchUnitType offsetUnitType;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		private Vector2 offset = new Vector2(20f, 20f);

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		private TouchUnitType areaUnitType;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		private Rect activeArea = new Rect(0f, 0f, 50f, 100f);

		// Token: 0x040001E6 RID: 486
		[Header("Options")]
		public TouchControl.AnalogTarget target = TouchControl.AnalogTarget.LeftStick;

		// Token: 0x040001E7 RID: 487
		public TouchControl.SnapAngles snapAngles;

		// Token: 0x040001E8 RID: 488
		[Range(0f, 1f)]
		public float lowerDeadZone = 0.1f;

		// Token: 0x040001E9 RID: 489
		[Range(0f, 1f)]
		public float upperDeadZone = 0.9f;

		// Token: 0x040001EA RID: 490
		public AnimationCurve inputCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

		// Token: 0x040001EB RID: 491
		public bool allowDragging;

		// Token: 0x040001EC RID: 492
		public bool snapToInitialTouch = true;

		// Token: 0x040001ED RID: 493
		public bool resetWhenDone = true;

		// Token: 0x040001EE RID: 494
		public float resetDuration = 0.1f;

		// Token: 0x040001EF RID: 495
		[Header("Sprites")]
		public TouchSprite ring = new TouchSprite(20f);

		// Token: 0x040001F0 RID: 496
		public TouchSprite knob = new TouchSprite(10f);

		// Token: 0x040001F1 RID: 497
		public float knobRange = 7.5f;

		// Token: 0x040001F2 RID: 498
		private Vector3 resetPosition;

		// Token: 0x040001F3 RID: 499
		private Vector3 beganPosition;

		// Token: 0x040001F4 RID: 500
		private Vector3 movedPosition;

		// Token: 0x040001F5 RID: 501
		private float ringResetSpeed;

		// Token: 0x040001F6 RID: 502
		private float knobResetSpeed;

		// Token: 0x040001F7 RID: 503
		private Rect worldActiveArea;

		// Token: 0x040001F8 RID: 504
		private float worldKnobRange;

		// Token: 0x040001F9 RID: 505
		private Vector3 value;

		// Token: 0x040001FA RID: 506
		private Vector3 snappedValue;

		// Token: 0x040001FB RID: 507
		private Touch currentTouch;

		// Token: 0x040001FC RID: 508
		private bool dirty;
	}
}
