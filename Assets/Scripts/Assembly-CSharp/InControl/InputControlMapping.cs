using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200001F RID: 31
	public class InputControlMapping
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00006ACC File Offset: 0x00004ECC
		public float MapValue(float value)
		{
			if (this.Raw)
			{
				value *= this.Scale;
				value = ((!this.SourceRange.Excludes(value)) ? value : 0f);
			}
			else
			{
				value = Mathf.Clamp(value * this.Scale, -1f, 1f);
				value = InputRange.Remap(value, this.SourceRange, this.TargetRange);
			}
			if (this.Invert)
			{
				value = -value;
			}
			return value;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006B4E File Offset: 0x00004F4E
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006B7C File Offset: 0x00004F7C
		public string Handle
		{
			get
			{
				return (!string.IsNullOrEmpty(this.handle)) ? this.handle : this.Target.ToString();
			}
			set
			{
				this.handle = value;
			}
		}

		// Token: 0x040000FC RID: 252
		public InputControlSource Source;

		// Token: 0x040000FD RID: 253
		public InputControlType Target;

		// Token: 0x040000FE RID: 254
		public bool Invert;

		// Token: 0x040000FF RID: 255
		public float Scale = 1f;

		// Token: 0x04000100 RID: 256
		public bool Raw;

		// Token: 0x04000101 RID: 257
		public bool IgnoreInitialZeroValue;

		// Token: 0x04000102 RID: 258
		public float Sensitivity = 1f;

		// Token: 0x04000103 RID: 259
		public float LowerDeadZone;

		// Token: 0x04000104 RID: 260
		public float UpperDeadZone = 1f;

		// Token: 0x04000105 RID: 261
		public InputRange SourceRange = InputRange.MinusOneToOne;

		// Token: 0x04000106 RID: 262
		public InputRange TargetRange = InputRange.MinusOneToOne;

		// Token: 0x04000107 RID: 263
		private string handle;
	}
}
