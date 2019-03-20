using System;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000017 RID: 23
	public class UnknownDeviceBindingSource : BindingSource
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005EF3 File Offset: 0x000042F3
		internal UnknownDeviceBindingSource()
		{
			this.Control = UnknownDeviceControl.None;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00005F06 File Offset: 0x00004306
		public UnknownDeviceBindingSource(UnknownDeviceControl control)
		{
			this.Control = control;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005F15 File Offset: 0x00004315
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005F1D File Offset: 0x0000431D
		public UnknownDeviceControl Control { get; protected set; }

		// Token: 0x060000B2 RID: 178 RVA: 0x00005F28 File Offset: 0x00004328
		public override float GetValue(InputDevice device)
		{
			return this.Control.GetValue(device);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00005F44 File Offset: 0x00004344
		public override bool GetState(InputDevice device)
		{
			return device != null && Utility.IsNotZero(this.GetValue(device));
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00005F5C File Offset: 0x0000435C
		public override string Name
		{
			get
			{
				if (base.BoundTo == null)
				{
					return string.Empty;
				}
				string str = string.Empty;
				if (this.Control.SourceRange == InputRangeType.ZeroToMinusOne)
				{
					str = "Negative ";
				}
				else if (this.Control.SourceRange == InputRangeType.ZeroToOne)
				{
					str = "Positive ";
				}
				InputDevice device = base.BoundTo.Device;
				if (device == InputDevice.Null)
				{
					return str + this.Control.Control.ToString();
				}
				InputControl control = device.GetControl(this.Control.Control);
				if (control == InputControl.Null)
				{
					return str + this.Control.Control.ToString();
				}
				return str + control.Handle;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00006044 File Offset: 0x00004444
		public override string DeviceName
		{
			get
			{
				if (base.BoundTo == null)
				{
					return string.Empty;
				}
				InputDevice device = base.BoundTo.Device;
				if (device == InputDevice.Null)
				{
					return "Unknown Controller";
				}
				return device.Name;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006088 File Offset: 0x00004488
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			UnknownDeviceBindingSource unknownDeviceBindingSource = other as UnknownDeviceBindingSource;
			return unknownDeviceBindingSource != null && this.Control == unknownDeviceBindingSource.Control;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000060CC File Offset: 0x000044CC
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			UnknownDeviceBindingSource unknownDeviceBindingSource = other as UnknownDeviceBindingSource;
			return unknownDeviceBindingSource != null && this.Control == unknownDeviceBindingSource.Control;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006108 File Offset: 0x00004508
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00006129 File Offset: 0x00004529
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.UnknownDeviceBindingSource;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000612C File Offset: 0x0000452C
		internal override bool IsValid
		{
			get
			{
				if (base.BoundTo == null)
				{
					Debug.LogError("Cannot query property 'IsValid' for unbound BindingSource.");
					return false;
				}
				InputDevice device = base.BoundTo.Device;
				return device == InputDevice.Null || device.HasControl(this.Control.Control);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006180 File Offset: 0x00004580
		internal override void Load(BinaryReader reader)
		{
			UnknownDeviceControl control = default(UnknownDeviceControl);
			control.Load(reader);
			this.Control = control;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000061A4 File Offset: 0x000045A4
		internal override void Save(BinaryWriter writer)
		{
			this.Control.Save(writer);
		}
	}
}
