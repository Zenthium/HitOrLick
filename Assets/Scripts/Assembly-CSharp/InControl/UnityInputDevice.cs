using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000AE RID: 174
	public class UnityInputDevice : InputDevice
	{
		// Token: 0x0600038F RID: 911 RVA: 0x0001F1B8 File Offset: 0x0001D5B8
		public UnityInputDevice(InputDeviceProfile profile, int joystickId) : base(profile.Name)
		{
			this.Initialize(profile, joystickId);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001F1CE File Offset: 0x0001D5CE
		public UnityInputDevice(InputDeviceProfile profile) : base(profile.Name)
		{
			this.Initialize(profile, 0);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0001F1E4 File Offset: 0x0001D5E4
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0001F1EC File Offset: 0x0001D5EC
		internal int JoystickId { get; private set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001F1F5 File Offset: 0x0001D5F5
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0001F1FD File Offset: 0x0001D5FD
		public InputDeviceProfile Profile { get; protected set; }

		// Token: 0x06000395 RID: 917 RVA: 0x0001F208 File Offset: 0x0001D608
		private void Initialize(InputDeviceProfile profile, int joystickId)
		{
			this.Profile = profile;
			base.Meta = this.Profile.Meta;
			int analogCount = this.Profile.AnalogCount;
			for (int i = 0; i < analogCount; i++)
			{
				InputControlMapping inputControlMapping = this.Profile.AnalogMappings[i];
				InputControl inputControl = base.AddControl(inputControlMapping.Target, inputControlMapping.Handle);
				inputControl.Sensitivity = Mathf.Min(this.Profile.Sensitivity, inputControlMapping.Sensitivity);
				inputControl.LowerDeadZone = Mathf.Max(this.Profile.LowerDeadZone, inputControlMapping.LowerDeadZone);
				inputControl.UpperDeadZone = Mathf.Min(this.Profile.UpperDeadZone, inputControlMapping.UpperDeadZone);
				inputControl.Raw = inputControlMapping.Raw;
			}
			int buttonCount = this.Profile.ButtonCount;
			for (int j = 0; j < buttonCount; j++)
			{
				InputControlMapping inputControlMapping2 = this.Profile.ButtonMappings[j];
				base.AddControl(inputControlMapping2.Target, inputControlMapping2.Handle);
			}
			this.JoystickId = joystickId;
			if (joystickId != 0)
			{
				this.SortOrder = 100 + joystickId;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001F32C File Offset: 0x0001D72C
		public override void Update(ulong updateTick, float deltaTime)
		{
			if (this.Profile == null)
			{
				return;
			}
			int analogCount = this.Profile.AnalogCount;
			for (int i = 0; i < analogCount; i++)
			{
				InputControlMapping inputControlMapping = this.Profile.AnalogMappings[i];
				float value = inputControlMapping.Source.GetValue(this);
				InputControl control = base.GetControl(inputControlMapping.Target);
				if (!inputControlMapping.IgnoreInitialZeroValue || !control.IsOnZeroTick || !Utility.IsZero(value))
				{
					float value2 = inputControlMapping.MapValue(value);
					control.UpdateWithValue(value2, updateTick, deltaTime);
				}
			}
			int buttonCount = this.Profile.ButtonCount;
			for (int j = 0; j < buttonCount; j++)
			{
				InputControlMapping inputControlMapping2 = this.Profile.ButtonMappings[j];
				bool state = inputControlMapping2.Source.GetState(this);
				base.UpdateWithState(inputControlMapping2.Target, state, updateTick, deltaTime);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001F417 File Offset: 0x0001D817
		public override bool IsSupportedOnThisPlatform
		{
			get
			{
				return this.Profile != null && this.Profile.IsSupportedOnThisPlatform;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0001F432 File Offset: 0x0001D832
		public override bool IsKnown
		{
			get
			{
				return this.Profile != null && this.Profile.IsKnown;
			}
		}

		// Token: 0x040002AA RID: 682
		public const int MaxDevices = 10;

		// Token: 0x040002AB RID: 683
		public const int MaxButtons = 20;

		// Token: 0x040002AC RID: 684
		public const int MaxAnalogs = 20;
	}
}
