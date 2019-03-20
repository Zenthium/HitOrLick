using System;
using System.IO;

namespace InControl
{
	// Token: 0x02000005 RID: 5
	public abstract class BindingSource : InputControlSource, IEquatable<BindingSource>
	{
		// Token: 0x06000009 RID: 9
		public abstract float GetValue(InputDevice inputDevice);

		// Token: 0x0600000A RID: 10
		public abstract bool GetState(InputDevice inputDevice);

		// Token: 0x0600000B RID: 11
		public abstract bool Equals(BindingSource other);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12
		public abstract string Name { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13
		public abstract string DeviceName { get; }

		// Token: 0x0600000E RID: 14 RVA: 0x000024C8 File Offset: 0x000008C8
		public static bool operator ==(BindingSource a, BindingSource b)
		{
			return object.ReferenceEquals(a, b) || (a != null && b != null && a.BindingSourceType == b.BindingSourceType && a.Equals(b));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002500 File Offset: 0x00000900
		public static bool operator !=(BindingSource a, BindingSource b)
		{
			return !(a == b);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000250C File Offset: 0x0000090C
		public override bool Equals(object other)
		{
			return this.Equals((BindingSource)other);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000251A File Offset: 0x0000091A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18
		internal abstract BindingSourceType BindingSourceType { get; }

		// Token: 0x06000013 RID: 19
		internal abstract void Save(BinaryWriter writer);

		// Token: 0x06000014 RID: 20
		internal abstract void Load(BinaryReader reader);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002522 File Offset: 0x00000922
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000252A File Offset: 0x0000092A
		internal PlayerAction BoundTo { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002533 File Offset: 0x00000933
		internal virtual bool IsValid
		{
			get
			{
				return true;
			}
		}
	}
}
