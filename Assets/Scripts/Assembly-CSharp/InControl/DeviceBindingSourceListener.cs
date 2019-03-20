using System;

namespace InControl
{
	// Token: 0x0200000A RID: 10
	public class DeviceBindingSourceListener : BindingSourceListener
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002759 File Offset: 0x00000B59
		public void Reset()
		{
			this.detectFound = InputControlType.None;
			this.detectPhase = 0;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000276C File Offset: 0x00000B6C
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeControllers || device.IsUnknown)
			{
				return null;
			}
			if (this.detectFound != InputControlType.None && !this.IsPressed(this.detectFound, device) && this.detectPhase == 2)
			{
				DeviceBindingSource result = new DeviceBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			InputControlType inputControlType = this.ListenForControl(listenOptions, device);
			if (inputControlType != InputControlType.None)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = inputControlType;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000280F File Offset: 0x00000C0F
		private bool IsPressed(InputControl control)
		{
			return Utility.AbsoluteIsOverThreshold(control.Value, 0.5f);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002821 File Offset: 0x00000C21
		private bool IsPressed(InputControlType control, InputDevice device)
		{
			return this.IsPressed(device.GetControl(control));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002830 File Offset: 0x00000C30
		private InputControlType ListenForControl(BindingListenOptions listenOptions, InputDevice device)
		{
			if (device.IsKnown)
			{
				int num = device.Controls.Length;
				for (int i = 0; i < num; i++)
				{
					InputControl inputControl = device.Controls[i];
					if (inputControl != null && this.IsPressed(inputControl) && (listenOptions.IncludeNonStandardControls || inputControl.IsStandard))
					{
						InputControlType target = inputControl.Target;
						if (target != InputControlType.Command || !listenOptions.IncludeNonStandardControls)
						{
							return target;
						}
					}
				}
			}
			return InputControlType.None;
		}

		// Token: 0x04000022 RID: 34
		private InputControlType detectFound;

		// Token: 0x04000023 RID: 35
		private int detectPhase;
	}
}
