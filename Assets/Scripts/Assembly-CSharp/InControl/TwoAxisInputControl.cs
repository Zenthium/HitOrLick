using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000026 RID: 38
	public class TwoAxisInputControl : IInputControl
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00005A56 File Offset: 0x00003E56
		public TwoAxisInputControl()
		{
			this.Left = new OneAxisInputControl();
			this.Right = new OneAxisInputControl();
			this.Up = new OneAxisInputControl();
			this.Down = new OneAxisInputControl();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00005A95 File Offset: 0x00003E95
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005A9D File Offset: 0x00003E9D
		public float X { get; protected set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00005AA6 File Offset: 0x00003EA6
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005AAE File Offset: 0x00003EAE
		public float Y { get; protected set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005AB7 File Offset: 0x00003EB7
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00005ABF File Offset: 0x00003EBF
		public OneAxisInputControl Left { get; protected set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005AC8 File Offset: 0x00003EC8
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005AD0 File Offset: 0x00003ED0
		public OneAxisInputControl Right { get; protected set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00005AD9 File Offset: 0x00003ED9
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005AE1 File Offset: 0x00003EE1
		public OneAxisInputControl Up { get; protected set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00005AEA File Offset: 0x00003EEA
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00005AF2 File Offset: 0x00003EF2
		public OneAxisInputControl Down { get; protected set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00005AFB File Offset: 0x00003EFB
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005B03 File Offset: 0x00003F03
		public ulong UpdateTick { get; protected set; }

		// Token: 0x06000133 RID: 307 RVA: 0x00005B0C File Offset: 0x00003F0C
		public void ClearInputState()
		{
			this.Left.ClearInputState();
			this.Right.ClearInputState();
			this.Up.ClearInputState();
			this.Down.ClearInputState();
			this.lastState = false;
			this.lastValue = Vector2.zero;
			this.thisState = false;
			this.thisValue = Vector2.zero;
			this.clearInputState = true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005B70 File Offset: 0x00003F70
		public void Filter(TwoAxisInputControl twoAxisInputControl, float deltaTime)
		{
			this.UpdateWithAxes(twoAxisInputControl.X, twoAxisInputControl.Y, InputManager.CurrentTick, deltaTime);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005B8C File Offset: 0x00003F8C
		internal void UpdateWithAxes(float x, float y, ulong updateTick, float deltaTime)
		{
			this.lastState = this.thisState;
			this.lastValue = this.thisValue;
			this.thisValue = ((!this.Raw) ? Utility.ApplyCircularDeadZone(x, y, this.LowerDeadZone, this.UpperDeadZone) : new Vector2(x, y));
			this.X = this.thisValue.x;
			this.Y = this.thisValue.y;
			this.Left.CommitWithValue(Mathf.Max(0f, -this.X), updateTick, deltaTime);
			this.Right.CommitWithValue(Mathf.Max(0f, this.X), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.Up.CommitWithValue(Mathf.Max(0f, -this.Y), updateTick, deltaTime);
				this.Down.CommitWithValue(Mathf.Max(0f, this.Y), updateTick, deltaTime);
			}
			else
			{
				this.Up.CommitWithValue(Mathf.Max(0f, this.Y), updateTick, deltaTime);
				this.Down.CommitWithValue(Mathf.Max(0f, -this.Y), updateTick, deltaTime);
			}
			this.thisState = (this.Up.State || this.Down.State || this.Left.State || this.Right.State);
			if (this.clearInputState)
			{
				this.lastState = this.thisState;
				this.lastValue = this.thisValue;
				this.clearInputState = false;
				this.HasChanged = false;
				return;
			}
			if (this.thisValue != this.lastValue)
			{
				this.UpdateTick = updateTick;
				this.HasChanged = true;
			}
			else
			{
				this.HasChanged = false;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005DA8 File Offset: 0x000041A8
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00005D6F File Offset: 0x0000416F
		public float StateThreshold
		{
			get
			{
				return this.stateThreshold;
			}
			set
			{
				this.stateThreshold = value;
				this.Left.StateThreshold = value;
				this.Right.StateThreshold = value;
				this.Up.StateThreshold = value;
				this.Down.StateThreshold = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00005DB0 File Offset: 0x000041B0
		public bool State
		{
			get
			{
				return this.thisState;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005DB8 File Offset: 0x000041B8
		public bool LastState
		{
			get
			{
				return this.lastState;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00005DC0 File Offset: 0x000041C0
		public Vector2 Value
		{
			get
			{
				return this.thisValue;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005DC8 File Offset: 0x000041C8
		public Vector2 LastValue
		{
			get
			{
				return this.lastValue;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005DD0 File Offset: 0x000041D0
		public Vector2 Vector
		{
			get
			{
				return this.thisValue;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005DD8 File Offset: 0x000041D8
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005DE0 File Offset: 0x000041E0
		public bool HasChanged { get; protected set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005DE9 File Offset: 0x000041E9
		public bool IsPressed
		{
			get
			{
				return this.thisState;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005DF1 File Offset: 0x000041F1
		public bool WasPressed
		{
			get
			{
				return this.thisState && !this.lastState;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005E0A File Offset: 0x0000420A
		public bool WasReleased
		{
			get
			{
				return !this.thisState && this.lastState;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005E20 File Offset: 0x00004220
		public float Angle
		{
			get
			{
				return Utility.VectorToAngle(this.thisValue);
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005E2D File Offset: 0x0000422D
		public static implicit operator bool(TwoAxisInputControl instance)
		{
			return instance.thisState;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005E35 File Offset: 0x00004235
		public static implicit operator Vector2(TwoAxisInputControl instance)
		{
			return instance.thisValue;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005E3D File Offset: 0x0000423D
		public static implicit operator Vector3(TwoAxisInputControl instance)
		{
			return instance.thisValue;
		}

		// Token: 0x04000179 RID: 377
		public float LowerDeadZone;

		// Token: 0x0400017A RID: 378
		public float UpperDeadZone = 1f;

		// Token: 0x0400017B RID: 379
		public bool Raw;

		// Token: 0x0400017C RID: 380
		private bool thisState;

		// Token: 0x0400017D RID: 381
		private bool lastState;

		// Token: 0x0400017E RID: 382
		private Vector2 thisValue;

		// Token: 0x0400017F RID: 383
		private Vector2 lastValue;

		// Token: 0x04000180 RID: 384
		private bool clearInputState;

		// Token: 0x04000181 RID: 385
		private float stateThreshold;
	}
}
