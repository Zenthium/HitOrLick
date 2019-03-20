using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000B3 RID: 179
	public class UnknownUnityInputDevice : UnityInputDevice
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0001FDC2 File Offset: 0x0001E1C2
		internal UnknownUnityInputDevice(InputDeviceProfile profile, int joystickId) : base(profile, joystickId)
		{
			this.AnalogSnapshot = new float[20];
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0001FDD9 File Offset: 0x0001E1D9
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0001FDE1 File Offset: 0x0001E1E1
		internal float[] AnalogSnapshot { get; private set; }

		// Token: 0x060003C6 RID: 966 RVA: 0x0001FDEC File Offset: 0x0001E1EC
		internal void TakeSnapshot()
		{
			for (int i = 0; i < 20; i++)
			{
				InputControlType inputControlType = InputControlType.Analog0 + i;
				float num = Utility.ApplySnapping(base.GetControl(inputControlType).RawValue, 0.5f);
				this.AnalogSnapshot[i] = num;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001FE34 File Offset: 0x0001E234
		internal UnknownDeviceControl GetFirstPressedAnalog()
		{
			for (int i = 0; i < 20; i++)
			{
				InputControlType inputControlType = InputControlType.Analog0 + i;
				float num = Utility.ApplySnapping(base.GetControl(inputControlType).RawValue, 0.5f);
				float num2 = num - this.AnalogSnapshot[i];
				Debug.Log(num);
				Debug.Log(this.AnalogSnapshot[i]);
				Debug.Log(num2);
				if (num2 > 1.9f)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.MinusOneToOne);
				}
				if (num2 < -0.9f)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.ZeroToMinusOne);
				}
				if (num2 > 0.9f)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.ZeroToOne);
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001FEE4 File Offset: 0x0001E2E4
		internal UnknownDeviceControl GetFirstPressedButton()
		{
			for (int i = 0; i < 20; i++)
			{
				InputControlType inputControlType = InputControlType.Button0 + i;
				if (base.GetControl(inputControlType).IsPressed)
				{
					return new UnknownDeviceControl(inputControlType, InputRangeType.ZeroToOne);
				}
			}
			return UnknownDeviceControl.None;
		}
	}
}
