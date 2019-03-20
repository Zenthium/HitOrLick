using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000025 RID: 37
	public class OneAxisInputControl : InputControlBase
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00005994 File Offset: 0x00003D94
		internal void CommitWithSides(InputControl negativeSide, InputControl positiveSide, ulong updateTick, float deltaTime)
		{
			base.LowerDeadZone = Mathf.Max(negativeSide.LowerDeadZone, positiveSide.LowerDeadZone);
			base.UpperDeadZone = Mathf.Min(negativeSide.UpperDeadZone, positiveSide.UpperDeadZone);
			this.Raw = (negativeSide.Raw || positiveSide.Raw);
			float value = Utility.ValueFromSides(negativeSide.RawValue, positiveSide.RawValue);
			base.CommitWithValue(value, updateTick, deltaTime);
		}
	}
}
