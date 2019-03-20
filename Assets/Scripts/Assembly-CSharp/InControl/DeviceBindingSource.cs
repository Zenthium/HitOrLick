using System;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000009 RID: 9
	public class DeviceBindingSource : BindingSource
	{
		// Token: 0x0600001A RID: 26 RVA: 0x00002536 File Offset: 0x00000936
		internal DeviceBindingSource()
		{
			this.Control = InputControlType.None;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002545 File Offset: 0x00000945
		public DeviceBindingSource(InputControlType control)
		{
			this.Control = control;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002554 File Offset: 0x00000954
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000255C File Offset: 0x0000095C
		public InputControlType Control { get; protected set; }

		// Token: 0x0600001E RID: 30 RVA: 0x00002565 File Offset: 0x00000965
		public override float GetValue(InputDevice inputDevice)
		{
			if (inputDevice == null)
			{
				return 0f;
			}
			return inputDevice.GetControl(this.Control).Value;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002584 File Offset: 0x00000984
		public override bool GetState(InputDevice inputDevice)
		{
			return inputDevice != null && inputDevice.GetControl(this.Control).State;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000025A0 File Offset: 0x000009A0
		public override string Name
		{
			get
			{
				if (base.BoundTo == null)
				{
					return string.Empty;
				}
				InputDevice device = base.BoundTo.Device;
				InputControl control = device.GetControl(this.Control);
				if (control == InputControl.Null)
				{
					return this.Control.ToString();
				}
				return device.GetControl(this.Control).Handle;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002608 File Offset: 0x00000A08
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
					return "Controller";
				}
				return device.Name;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000264C File Offset: 0x00000A4C
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			DeviceBindingSource deviceBindingSource = other as DeviceBindingSource;
			return deviceBindingSource != null && this.Control == deviceBindingSource.Control;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000268C File Offset: 0x00000A8C
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			DeviceBindingSource deviceBindingSource = other as DeviceBindingSource;
			return deviceBindingSource != null && this.Control == deviceBindingSource.Control;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026C4 File Offset: 0x00000AC4
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026E5 File Offset: 0x00000AE5
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.DeviceBindingSource;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026E8 File Offset: 0x00000AE8
		internal override void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026F6 File Offset: 0x00000AF6
		internal override void Load(BinaryReader reader)
		{
			this.Control = (InputControlType)reader.ReadInt32();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002704 File Offset: 0x00000B04
		internal override bool IsValid
		{
			get
			{
				if (base.BoundTo == null)
				{
					Debug.LogError("Cannot query property 'IsValid' for unbound BindingSource.");
					return false;
				}
				return base.BoundTo.Device.HasControl(this.Control) || Utility.TargetIsStandard(this.Control);
			}
		}
	}
}
