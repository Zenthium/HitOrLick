using System;
using System.IO;

namespace InControl
{
	// Token: 0x0200000C RID: 12
	public class KeyBindingSource : BindingSource
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000028B6 File Offset: 0x00000CB6
		internal KeyBindingSource()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028BE File Offset: 0x00000CBE
		public KeyBindingSource(KeyCombo keyCombo)
		{
			this.Control = keyCombo;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028CD File Offset: 0x00000CCD
		public KeyBindingSource(params Key[] keys)
		{
			this.Control = new KeyCombo(keys);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028E1 File Offset: 0x00000CE1
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000028E9 File Offset: 0x00000CE9
		public KeyCombo Control { get; protected set; }

		// Token: 0x06000034 RID: 52 RVA: 0x000028F2 File Offset: 0x00000CF2
		public override float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002910 File Offset: 0x00000D10
		public override bool GetState(InputDevice inputDevice)
		{
			return this.Control.IsPressed;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000292C File Offset: 0x00000D2C
		public override string Name
		{
			get
			{
				return this.Control.ToString();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000294D File Offset: 0x00000D4D
		public override string DeviceName
		{
			get
			{
				return "Keyboard";
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002954 File Offset: 0x00000D54
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			KeyBindingSource keyBindingSource = other as KeyBindingSource;
			return keyBindingSource != null && this.Control == keyBindingSource.Control;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002998 File Offset: 0x00000D98
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			KeyBindingSource keyBindingSource = other as KeyBindingSource;
			return keyBindingSource != null && this.Control == keyBindingSource.Control;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029D4 File Offset: 0x00000DD4
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029F5 File Offset: 0x00000DF5
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.KeyBindingSource;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000029F8 File Offset: 0x00000DF8
		internal override void Load(BinaryReader reader)
		{
			KeyCombo control = default(KeyCombo);
			control.Load(reader);
			this.Control = control;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A1C File Offset: 0x00000E1C
		internal override void Save(BinaryWriter writer)
		{
			this.Control.Save(writer);
		}
	}
}
