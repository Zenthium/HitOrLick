using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200001E RID: 30
	public abstract class InputControlBase : IInputControl
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004301 File Offset: 0x00002701
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004309 File Offset: 0x00002709
		public ulong UpdateTick { get; protected set; }

		// Token: 0x060000EF RID: 239 RVA: 0x00004314 File Offset: 0x00002714
		private void PrepareForUpdate(ulong updateTick)
		{
			if (updateTick < this.pendingTick)
			{
				throw new InvalidOperationException("Cannot be updated with an earlier tick.");
			}
			if (this.pendingCommit && updateTick != this.pendingTick)
			{
				throw new InvalidOperationException("Cannot be updated for a new tick until pending tick is committed.");
			}
			if (updateTick > this.pendingTick)
			{
				this.lastState = this.thisState;
				this.nextState.Reset();
				this.pendingTick = updateTick;
				this.pendingCommit = true;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000438B File Offset: 0x0000278B
		public bool UpdateWithState(bool state, ulong updateTick, float deltaTime)
		{
			this.PrepareForUpdate(updateTick);
			this.nextState.Set(state || this.nextState.State);
			return state;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000043B4 File Offset: 0x000027B4
		public bool UpdateWithValue(float value, ulong updateTick, float deltaTime)
		{
			this.PrepareForUpdate(updateTick);
			if (Utility.Abs(value) > Utility.Abs(this.nextState.RawValue))
			{
				this.nextState.RawValue = value;
				if (!this.Raw)
				{
					value = Utility.ApplyDeadZone(value, this.lowerDeadZone, this.upperDeadZone);
				}
				this.nextState.Set(value, this.stateThreshold);
				return true;
			}
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004424 File Offset: 0x00002824
		internal bool UpdateWithRawValue(float value, ulong updateTick, float deltaTime)
		{
			this.PrepareForUpdate(updateTick);
			if (Utility.Abs(value) > Utility.Abs(this.nextState.RawValue))
			{
				this.nextState.RawValue = value;
				this.nextState.Set(value, this.stateThreshold);
				return true;
			}
			return false;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004474 File Offset: 0x00002874
		internal void SetValue(float value, ulong updateTick)
		{
			if (updateTick > this.pendingTick)
			{
				this.lastState = this.thisState;
				this.nextState.Reset();
				this.pendingTick = updateTick;
				this.pendingCommit = true;
			}
			this.nextState.RawValue = value;
			this.nextState.Set(value, this.StateThreshold);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000044D0 File Offset: 0x000028D0
		public void ClearInputState()
		{
			this.lastState.Reset();
			this.thisState.Reset();
			this.nextState.Reset();
			this.wasRepeated = false;
			this.clearInputState = true;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004504 File Offset: 0x00002904
		public void Commit()
		{
			this.pendingCommit = false;
			this.thisState = this.nextState;
			if (this.clearInputState)
			{
				this.lastState = this.nextState;
				this.UpdateTick = this.pendingTick;
				this.clearInputState = false;
				return;
			}
			bool state = this.lastState.State;
			bool state2 = this.thisState.State;
			this.wasRepeated = false;
			if (state && !state2)
			{
				this.nextRepeatTime = 0f;
			}
			else if (state2)
			{
				if (state != state2)
				{
					this.nextRepeatTime = Time.realtimeSinceStartup + this.FirstRepeatDelay;
				}
				else if (Time.realtimeSinceStartup >= this.nextRepeatTime)
				{
					this.wasRepeated = true;
					this.nextRepeatTime = Time.realtimeSinceStartup + this.RepeatDelay;
				}
			}
			if (this.thisState != this.lastState)
			{
				this.UpdateTick = this.pendingTick;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000045F9 File Offset: 0x000029F9
		public void CommitWithState(bool state, ulong updateTick, float deltaTime)
		{
			this.UpdateWithState(state, updateTick, deltaTime);
			this.Commit();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000460B File Offset: 0x00002A0B
		public void CommitWithValue(float value, ulong updateTick, float deltaTime)
		{
			this.UpdateWithValue(value, updateTick, deltaTime);
			this.Commit();
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000461D File Offset: 0x00002A1D
		public bool State
		{
			get
			{
				return this.Enabled && this.thisState.State;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004638 File Offset: 0x00002A38
		public bool LastState
		{
			get
			{
				return this.Enabled && this.lastState.State;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004653 File Offset: 0x00002A53
		public float Value
		{
			get
			{
				return (!this.Enabled) ? 0f : this.thisState.Value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004675 File Offset: 0x00002A75
		public float LastValue
		{
			get
			{
				return (!this.Enabled) ? 0f : this.lastState.Value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004697 File Offset: 0x00002A97
		public float RawValue
		{
			get
			{
				return (!this.Enabled) ? 0f : this.thisState.RawValue;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000046B9 File Offset: 0x00002AB9
		internal float NextRawValue
		{
			get
			{
				return (!this.Enabled) ? 0f : this.nextState.RawValue;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000046DB File Offset: 0x00002ADB
		public bool HasChanged
		{
			get
			{
				return this.Enabled && this.thisState != this.lastState;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000046FC File Offset: 0x00002AFC
		public bool IsPressed
		{
			get
			{
				return this.Enabled && this.thisState.State;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004717 File Offset: 0x00002B17
		public bool WasPressed
		{
			get
			{
				return this.Enabled && this.thisState && !this.lastState;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004745 File Offset: 0x00002B45
		public bool WasReleased
		{
			get
			{
				return this.Enabled && !this.thisState && this.lastState;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004770 File Offset: 0x00002B70
		public bool WasRepeated
		{
			get
			{
				return this.Enabled && this.wasRepeated;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004786 File Offset: 0x00002B86
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000478E File Offset: 0x00002B8E
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000479C File Offset: 0x00002B9C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000047A4 File Offset: 0x00002BA4
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000047B2 File Offset: 0x00002BB2
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000047BA File Offset: 0x00002BBA
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000047C8 File Offset: 0x00002BC8
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000047D0 File Offset: 0x00002BD0
		public float StateThreshold
		{
			get
			{
				return this.stateThreshold;
			}
			set
			{
				this.stateThreshold = Mathf.Clamp01(value);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000047DE File Offset: 0x00002BDE
		public static implicit operator bool(InputControlBase instance)
		{
			return instance.State;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000047E6 File Offset: 0x00002BE6
		public static implicit operator float(InputControlBase instance)
		{
			return instance.Value;
		}

		// Token: 0x040000EB RID: 235
		private float sensitivity = 1f;

		// Token: 0x040000EC RID: 236
		private float lowerDeadZone;

		// Token: 0x040000ED RID: 237
		private float upperDeadZone = 1f;

		// Token: 0x040000EE RID: 238
		private float stateThreshold;

		// Token: 0x040000EF RID: 239
		public float FirstRepeatDelay = 0.8f;

		// Token: 0x040000F0 RID: 240
		public float RepeatDelay = 0.1f;

		// Token: 0x040000F1 RID: 241
		public bool Raw;

		// Token: 0x040000F2 RID: 242
		internal bool Enabled = true;

		// Token: 0x040000F3 RID: 243
		private ulong pendingTick;

		// Token: 0x040000F4 RID: 244
		private bool pendingCommit;

		// Token: 0x040000F5 RID: 245
		private float nextRepeatTime;

		// Token: 0x040000F6 RID: 246
		private float lastPressedTime;

		// Token: 0x040000F7 RID: 247
		private bool wasRepeated;

		// Token: 0x040000F8 RID: 248
		private bool clearInputState;

		// Token: 0x040000F9 RID: 249
		private InputControlState lastState;

		// Token: 0x040000FA RID: 250
		private InputControlState nextState;

		// Token: 0x040000FB RID: 251
		private InputControlState thisState;
	}
}
