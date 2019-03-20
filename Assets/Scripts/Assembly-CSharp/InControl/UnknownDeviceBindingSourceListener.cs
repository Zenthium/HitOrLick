using System;

namespace InControl
{
	// Token: 0x02000018 RID: 24
	public class UnknownDeviceBindingSourceListener : BindingSourceListener
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000061C8 File Offset: 0x000045C8
		public void Reset()
		{
			this.detectFound = UnknownDeviceControl.None;
			this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForInitialRelease;
			this.TakeSnapshotOnUnknownDevices();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000061E4 File Offset: 0x000045E4
		private void TakeSnapshotOnUnknownDevices()
		{
			int count = InputManager.Devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.Devices[i];
				if (inputDevice.IsUnknown)
				{
					UnknownUnityInputDevice unknownUnityInputDevice = inputDevice as UnknownUnityInputDevice;
					if (unknownUnityInputDevice != null)
					{
						unknownUnityInputDevice.TakeSnapshot();
					}
				}
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006238 File Offset: 0x00004638
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeUnknownControllers || device.IsKnown)
			{
				return null;
			}
			if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlRelease && this.detectFound && !this.IsPressed(this.detectFound, device))
			{
				UnknownDeviceBindingSource result = new UnknownDeviceBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			UnknownDeviceControl control = this.ListenForControl(listenOptions, device);
			if (control)
			{
				if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlPress)
				{
					this.detectFound = control;
					this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlRelease;
				}
			}
			else if (this.detectPhase == UnknownDeviceBindingSourceListener.DetectPhase.WaitForInitialRelease)
			{
				this.detectPhase = UnknownDeviceBindingSourceListener.DetectPhase.WaitForControlPress;
			}
			return null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000062E8 File Offset: 0x000046E8
		private bool IsPressed(UnknownDeviceControl control, InputDevice device)
		{
			float value = control.GetValue(device);
			return Utility.AbsoluteIsOverThreshold(value, 0.5f);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000630C File Offset: 0x0000470C
		private UnknownDeviceControl ListenForControl(BindingListenOptions listenOptions, InputDevice device)
		{
			if (device.IsUnknown)
			{
				UnknownUnityInputDevice unknownUnityInputDevice = device as UnknownUnityInputDevice;
				if (unknownUnityInputDevice != null)
				{
					UnknownDeviceControl firstPressedButton = unknownUnityInputDevice.GetFirstPressedButton();
					if (firstPressedButton)
					{
						return firstPressedButton;
					}
					UnknownDeviceControl firstPressedAnalog = unknownUnityInputDevice.GetFirstPressedAnalog();
					if (firstPressedAnalog)
					{
						return firstPressedAnalog;
					}
				}
			}
			return UnknownDeviceControl.None;
		}

		// Token: 0x040000D2 RID: 210
		private UnknownDeviceControl detectFound;

		// Token: 0x040000D3 RID: 211
		private UnknownDeviceBindingSourceListener.DetectPhase detectPhase;

		// Token: 0x02000019 RID: 25
		private enum DetectPhase
		{
			// Token: 0x040000D5 RID: 213
			WaitForInitialRelease,
			// Token: 0x040000D6 RID: 214
			WaitForControlPress,
			// Token: 0x040000D7 RID: 215
			WaitForControlRelease
		}
	}
}
