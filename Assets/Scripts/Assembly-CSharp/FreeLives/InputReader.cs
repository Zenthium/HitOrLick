using System;
using InControl;
using UnityEngine;

namespace FreeLives
{
	// Token: 0x020000D7 RID: 215
	public static class InputReader
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x00024618 File Offset: 0x00022A18
		public static void GetInput(InputState inputState)
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			InputReader.CacheLastInput(inputState);
			if (activeDevice.SortOrder == 2147483647)
			{
				InputReader.GetKeyboard1Input(inputState);
			}
			else
			{
				InputReader.GetInControlInput(activeDevice, inputState);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00024654 File Offset: 0x00022A54
		public static void GetInput(InputReader.Device device, InputState inputState)
		{
			InputReader.Initialize();
			InputReader.CacheLastInput(inputState);
			if (device == InputReader.Device.Keyboard1)
			{
				InputReader.GetKeyboard1Input(inputState);
			}
			else if (device == InputReader.Device.Keyboard2)
			{
				InputReader.GetKeyboard2Input(inputState);
			}
			else if (device == InputReader.Device.Gamepad1)
			{
				if (InputReader.GamepadHasBeenAssigned(InputReader.Device.Gamepad1))
				{
					InputReader.GetInControlInput(InputReader.inControlDevices[0], inputState);
				}
				else
				{
					InputReader.ClearInputState(inputState);
				}
			}
			else if (device == InputReader.Device.Gamepad2)
			{
				if (InputReader.GamepadHasBeenAssigned(InputReader.Device.Gamepad2))
				{
					InputReader.GetInControlInput(InputReader.inControlDevices[1], inputState);
				}
				else
				{
					InputReader.ClearInputState(inputState);
				}
			}
			else if (device == InputReader.Device.Gamepad3)
			{
				if (InputReader.GamepadHasBeenAssigned(InputReader.Device.Gamepad3))
				{
					InputReader.GetInControlInput(InputReader.inControlDevices[2], inputState);
				}
				else
				{
					InputReader.ClearInputState(inputState);
				}
			}
			else if (device == InputReader.Device.Gamepad4)
			{
				if (InputReader.GamepadHasBeenAssigned(InputReader.Device.Gamepad4))
				{
					InputReader.GetInControlInput(InputReader.inControlDevices[3], inputState);
				}
				else
				{
					InputReader.ClearInputState(inputState);
				}
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00024748 File Offset: 0x00022B48
		private static void CacheLastInput(InputState inputState)
		{
			inputState.wasAButton = inputState.aButton;
			inputState.wasBButton = inputState.bButton;
			inputState.wasXButton = inputState.xButton;
			inputState.wasYButton = inputState.yButton;
			inputState.wasLeft = inputState.left;
			inputState.wasRight = inputState.right;
			inputState.wasUp = inputState.up;
			inputState.wasDown = inputState.down;
			inputState.wasStart = inputState.start;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000247C4 File Offset: 0x00022BC4
		private static void Initialize()
		{
			if (InputReader.haveInitialized)
			{
				return;
			}
			InputReader.haveInitialized = true;
			int num = 0;
			foreach (InputDevice inputDevice in InputManager.Devices)
			{
				if (inputDevice != null)
				{
					InputReader.inControlDevices[num] = inputDevice;
					num++;
				}
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0002483C File Offset: 0x00022C3C
		private static void GetKeyboard1Input(InputState inputState)
		{
			inputState.xAxis = (inputState.yAxis = (inputState.leftTrigger = (inputState.rightTrigger = 0f)));
			if (Input.GetKey(InputReader.kb1Left))
			{
				inputState.xAxis -= 1f;
			}
			if (Input.GetKey(InputReader.kb1Right))
			{
				inputState.xAxis += 1f;
			}
			if (Input.GetKey(InputReader.kb1Up))
			{
				inputState.yAxis += 1f;
			}
			if (Input.GetKey(InputReader.kb1Down))
			{
				inputState.yAxis -= 1f;
			}
			inputState.up = Input.GetKey(InputReader.kb1Up);
			inputState.down = Input.GetKey(InputReader.kb1Down);
			inputState.left = Input.GetKey(InputReader.kb1Left);
			inputState.right = Input.GetKey(InputReader.kb1Right);
			inputState.yButton = Input.GetKey(InputReader.kb1Y);
			inputState.xButton = Input.GetKey(InputReader.kb1X);
			inputState.aButton = Input.GetKey(InputReader.kb1A);
			inputState.bButton = Input.GetKey(InputReader.kb1B);
			inputState.start = Input.GetKey(InputReader.kb1Start);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00024984 File Offset: 0x00022D84
		private static void GetKeyboard2Input(InputState inputState)
		{
			inputState.xAxis = (inputState.yAxis = (inputState.leftTrigger = (inputState.rightTrigger = 0f)));
			if (Input.GetKey(InputReader.kb2Left))
			{
				inputState.xAxis -= 1f;
			}
			if (Input.GetKey(InputReader.kb2Right))
			{
				inputState.xAxis += 1f;
			}
			if (Input.GetKey(InputReader.kb2Up))
			{
				inputState.yAxis += 1f;
			}
			if (Input.GetKey(InputReader.kb2Down))
			{
				inputState.yAxis -= 1f;
			}
			inputState.up = Input.GetKey(InputReader.kb2Up);
			inputState.down = Input.GetKey(InputReader.kb2Down);
			inputState.left = Input.GetKey(InputReader.kb2Left);
			inputState.right = Input.GetKey(InputReader.kb2Right);
			inputState.yButton = Input.GetKey(InputReader.kb2Y);
			inputState.xButton = Input.GetKey(InputReader.kb2X);
			inputState.aButton = Input.GetKey(InputReader.kb2A);
			inputState.bButton = Input.GetKey(InputReader.kb2B);
			inputState.start = Input.GetKey(InputReader.kb2Start);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00024ACC File Offset: 0x00022ECC
		private static void GetInControlInput(InputDevice device, InputState inputState)
		{
			if (device == null)
			{
				return;
			}
			inputState.xAxis = device.LeftStickX;
			inputState.yAxis = device.LeftStickY;
			inputState.right = (device.LeftStickX > InputReader.deadZone || device.DPadRight);
			inputState.left = (device.LeftStickX < -InputReader.deadZone || device.DPadLeft);
			inputState.up = (device.LeftStickY > InputReader.deadZone || device.DPadUp);
			inputState.down = (device.LeftStickY < -InputReader.deadZone || device.DPadDown);
			inputState.aButton = device.Action1.IsPressed;
			inputState.bButton = device.Action2.IsPressed;
			inputState.xButton = device.Action3.IsPressed;
			inputState.yButton = device.Action4.IsPressed;
			inputState.leftTrigger = device.LeftTrigger;
			inputState.rightTrigger = device.RightTrigger;
			inputState.start = device.Command;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00024C20 File Offset: 0x00023020
		public static void ClearInputState(InputState inputState)
		{
			inputState.rightTrigger = (inputState.leftTrigger = (inputState.xAxis = (inputState.yAxis = 0f)));
			inputState.left = (inputState.right = (inputState.up = (inputState.down = (inputState.aButton = (inputState.bButton = (inputState.xButton = (inputState.yButton = false)))))));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00024C9C File Offset: 0x0002309C
		private static bool GamepadHasBeenAssigned(InputReader.Device device)
		{
			int num = 0;
			switch (device)
			{
			case InputReader.Device.Gamepad1:
				num = 0;
				break;
			case InputReader.Device.Gamepad2:
				num = 1;
				break;
			case InputReader.Device.Gamepad3:
				num = 2;
				break;
			case InputReader.Device.Gamepad4:
				num = 3;
				break;
			}
			return InputReader.inControlDevices[num] != null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00024CF8 File Offset: 0x000230F8
		public static void DeviceAttached(InputDevice device)
		{
			for (int i = 0; i < 4; i++)
			{
				if (InputReader.inControlDevices[i] == null || InputReader.inControlDevices[i].SortOrder == device.SortOrder)
				{
					InputReader.inControlDevices[i] = device;
					return;
				}
			}
		}

		// Token: 0x040003AC RID: 940
		private static KeyCode kb1Left = KeyCode.LeftArrow;

		// Token: 0x040003AD RID: 941
		private static KeyCode kb1Right = KeyCode.RightArrow;

		// Token: 0x040003AE RID: 942
		private static KeyCode kb1Up = KeyCode.UpArrow;

		// Token: 0x040003AF RID: 943
		private static KeyCode kb1Down = KeyCode.DownArrow;

		// Token: 0x040003B0 RID: 944
		private static KeyCode kb1A = KeyCode.M;

		// Token: 0x040003B1 RID: 945
		private static KeyCode kb1B = KeyCode.Comma;

		// Token: 0x040003B2 RID: 946
		private static KeyCode kb1X = KeyCode.Period;

		// Token: 0x040003B3 RID: 947
		private static KeyCode kb1Y = KeyCode.Slash;

		// Token: 0x040003B4 RID: 948
		private static KeyCode kb1Start = KeyCode.Return;

		// Token: 0x040003B5 RID: 949
		private static KeyCode kb2Left = KeyCode.A;

		// Token: 0x040003B6 RID: 950
		private static KeyCode kb2Right = KeyCode.D;

		// Token: 0x040003B7 RID: 951
		private static KeyCode kb2Up = KeyCode.W;

		// Token: 0x040003B8 RID: 952
		private static KeyCode kb2Down = KeyCode.S;

		// Token: 0x040003B9 RID: 953
		private static KeyCode kb2A = KeyCode.T;

		// Token: 0x040003BA RID: 954
		private static KeyCode kb2B = KeyCode.Y;

		// Token: 0x040003BB RID: 955
		private static KeyCode kb2X = KeyCode.U;

		// Token: 0x040003BC RID: 956
		private static KeyCode kb2Y = KeyCode.I;

		// Token: 0x040003BD RID: 957
		private static KeyCode kb2Start = KeyCode.Space;

		// Token: 0x040003BE RID: 958
		private static float deadZone = 0.3f;

		// Token: 0x040003BF RID: 959
		private static InputDevice[] inControlDevices = new InputDevice[4];

		// Token: 0x040003C0 RID: 960
		private static bool haveInitialized;

		// Token: 0x020000D8 RID: 216
		public enum Device
		{
			// Token: 0x040003C2 RID: 962
			Keyboard1,
			// Token: 0x040003C3 RID: 963
			Keyboard2,
			// Token: 0x040003C4 RID: 964
			Gamepad1,
			// Token: 0x040003C5 RID: 965
			Gamepad2,
			// Token: 0x040003C6 RID: 966
			Gamepad3,
			// Token: 0x040003C7 RID: 967
			Gamepad4
		}
	}
}
