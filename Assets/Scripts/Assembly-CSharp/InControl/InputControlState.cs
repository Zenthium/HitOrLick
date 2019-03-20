using System;

namespace InControl
{
	// Token: 0x02000021 RID: 33
	public struct InputControlState
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00006B85 File Offset: 0x00004F85
		public void Reset()
		{
			this.State = false;
			this.Value = 0f;
			this.RawValue = 0f;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006BA4 File Offset: 0x00004FA4
		public void Set(float value)
		{
			this.Value = value;
			this.State = Utility.IsNotZero(value);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006BB9 File Offset: 0x00004FB9
		public void Set(float value, float threshold)
		{
			this.Value = value;
			this.State = Utility.AbsoluteIsOverThreshold(value, threshold);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006BCF File Offset: 0x00004FCF
		public void Set(bool state)
		{
			this.State = state;
			this.Value = ((!state) ? 0f : 1f);
			this.RawValue = this.Value;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006BFF File Offset: 0x00004FFF
		public static implicit operator bool(InputControlState state)
		{
			return state.State;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006C08 File Offset: 0x00005008
		public static implicit operator float(InputControlState state)
		{
			return state.Value;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006C11 File Offset: 0x00005011
		public static bool operator ==(InputControlState a, InputControlState b)
		{
			return a.State == b.State && Utility.Approximately(a.Value, b.Value);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006C3B File Offset: 0x0000503B
		public static bool operator !=(InputControlState a, InputControlState b)
		{
			return a.State != b.State || !Utility.Approximately(a.Value, b.Value);
		}

		// Token: 0x04000108 RID: 264
		public bool State;

		// Token: 0x04000109 RID: 265
		public float Value;

		// Token: 0x0400010A RID: 266
		public float RawValue;
	}
}
