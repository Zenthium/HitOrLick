using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000023 RID: 35
	public struct InputRange
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00006C68 File Offset: 0x00005068
		private InputRange(float value0, float value1, InputRangeType type)
		{
			this.Value0 = value0;
			this.Value1 = value1;
			this.Type = type;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00006C7F File Offset: 0x0000507F
		public InputRange(InputRangeType type)
		{
			this.Value0 = InputRange.TypeToRange[(int)type].Value0;
			this.Value1 = InputRange.TypeToRange[(int)type].Value1;
			this.Type = type;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006CB4 File Offset: 0x000050B4
		public bool Includes(float value)
		{
			return !this.Excludes(value);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006CC0 File Offset: 0x000050C0
		public bool Excludes(float value)
		{
			return this.Type == InputRangeType.None || value < Mathf.Min(this.Value0, this.Value1) || value > Mathf.Max(this.Value0, this.Value1);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006D00 File Offset: 0x00005100
		public static float Remap(float value, InputRange sourceRange, InputRange targetRange)
		{
			if (sourceRange.Excludes(value))
			{
				return 0f;
			}
			float t = Mathf.InverseLerp(sourceRange.Value0, sourceRange.Value1, value);
			return Mathf.Lerp(targetRange.Value0, targetRange.Value1, t);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006D4C File Offset: 0x0000514C
		internal static float Remap(float value, InputRangeType sourceRangeType, InputRangeType targetRangeType)
		{
			InputRange sourceRange = InputRange.TypeToRange[(int)sourceRangeType];
			InputRange targetRange = InputRange.TypeToRange[(int)targetRangeType];
			return InputRange.Remap(value, sourceRange, targetRange);
		}

		// Token: 0x0400015F RID: 351
		public static readonly InputRange None = new InputRange(0f, 0f, InputRangeType.None);

		// Token: 0x04000160 RID: 352
		public static readonly InputRange MinusOneToOne = new InputRange(-1f, 1f, InputRangeType.MinusOneToOne);

		// Token: 0x04000161 RID: 353
		public static readonly InputRange ZeroToOne = new InputRange(0f, 1f, InputRangeType.ZeroToOne);

		// Token: 0x04000162 RID: 354
		public static readonly InputRange ZeroToMinusOne = new InputRange(0f, -1f, InputRangeType.ZeroToMinusOne);

		// Token: 0x04000163 RID: 355
		public static readonly InputRange ZeroToNegativeInfinity = new InputRange(0f, float.NegativeInfinity, InputRangeType.ZeroToNegativeInfinity);

		// Token: 0x04000164 RID: 356
		public static readonly InputRange ZeroToPositiveInfinity = new InputRange(0f, float.PositiveInfinity, InputRangeType.ZeroToPositiveInfinity);

		// Token: 0x04000165 RID: 357
		public static readonly InputRange Everything = new InputRange(float.NegativeInfinity, float.PositiveInfinity, InputRangeType.Everything);

		// Token: 0x04000166 RID: 358
		private static readonly InputRange[] TypeToRange = new InputRange[]
		{
			InputRange.None,
			InputRange.MinusOneToOne,
			InputRange.ZeroToOne,
			InputRange.ZeroToMinusOne,
			InputRange.ZeroToNegativeInfinity,
			InputRange.ZeroToPositiveInfinity,
			InputRange.Everything
		};

		// Token: 0x04000167 RID: 359
		public readonly float Value0;

		// Token: 0x04000168 RID: 360
		public readonly float Value1;

		// Token: 0x04000169 RID: 361
		public readonly InputRangeType Type;
	}
}
