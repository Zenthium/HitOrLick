using System;
using System.IO;

namespace InControl
{
	// Token: 0x0200001A RID: 26
	public struct UnknownDeviceControl : IEquatable<UnknownDeviceControl>
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000635E File Offset: 0x0000475E
		public UnknownDeviceControl(InputControlType control, InputRangeType sourceRange)
		{
			this.Control = control;
			this.SourceRange = sourceRange;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006370 File Offset: 0x00004770
		internal float GetValue(InputDevice device)
		{
			if (device == null)
			{
				return 0f;
			}
			float value = device.GetControl(this.Control).Value;
			return InputRange.Remap(value, this.SourceRange, InputRangeType.ZeroToOne);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000063A8 File Offset: 0x000047A8
		public static bool operator ==(UnknownDeviceControl a, UnknownDeviceControl b)
		{
			if (object.ReferenceEquals(null, a))
			{
				return object.ReferenceEquals(null, b);
			}
			return a.Equals(b);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000063D0 File Offset: 0x000047D0
		public static bool operator !=(UnknownDeviceControl a, UnknownDeviceControl b)
		{
			return !(a == b);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000063DC File Offset: 0x000047DC
		public bool Equals(UnknownDeviceControl other)
		{
			return this.Control == other.Control && this.SourceRange == other.SourceRange;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006402 File Offset: 0x00004802
		public override bool Equals(object other)
		{
			return this.Equals((UnknownDeviceControl)other);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006410 File Offset: 0x00004810
		public override int GetHashCode()
		{
			return this.Control.GetHashCode() ^ this.SourceRange.GetHashCode();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006435 File Offset: 0x00004835
		public static implicit operator bool(UnknownDeviceControl control)
		{
			return control.Control != InputControlType.None;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006444 File Offset: 0x00004844
		internal void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
			writer.Write((int)this.SourceRange);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000645E File Offset: 0x0000485E
		internal void Load(BinaryReader reader)
		{
			this.Control = (InputControlType)reader.ReadInt32();
			this.SourceRange = (InputRangeType)reader.ReadInt32();
		}

		// Token: 0x040000D8 RID: 216
		public static readonly UnknownDeviceControl None = new UnknownDeviceControl(InputControlType.None, InputRangeType.None);

		// Token: 0x040000D9 RID: 217
		public InputControlType Control;

		// Token: 0x040000DA RID: 218
		public InputRangeType SourceRange;
	}
}
