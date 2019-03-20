﻿using System;

namespace InControl
{
	// Token: 0x020000A8 RID: 168
	[AutoDiscover]
	public class Xbox360WinProfile : UnityInputDeviceProfile
	{
		// Token: 0x06000366 RID: 870 RVA: 0x0001E06C File Offset: 0x0001C46C
		public Xbox360WinProfile()
		{
			base.Name = "XBox 360 Controller";
			base.Meta = "XBox 360 Controller on Windows";
			base.SupportedPlatforms = new string[]
			{
				"Windows"
			};
			this.JoystickNames = new string[]
			{
				"AIRFLO             ",
				"AxisPad",
				"Controller (Afterglow Gamepad for Xbox 360)",
				"Controller (Batarang wired controller (XBOX))",
				"Controller (Gamepad for Xbox 360)",
				"Controller (GPX Gamepad)",
				"Controller (Infinity Controller 360)",
				"Controller (Mad Catz FPS Pro GamePad)",
				"Controller (MadCatz Call of Duty GamePad)",
				"Controller (MadCatz GamePad)",
				"Controller (MLG GamePad for Xbox 360)",
				"Controller (Razer Sabertooth Elite)",
				"Controller (Rock Candy Gamepad for Xbox 360)",
				"Controller (SL-6566)",
				"Controller (Xbox 360 For Windows)",
				"Controller (Xbox 360 Wireless Receiver for Windows)",
				"Controller (Xbox Airflo wired controller)",
				"Controller (XEOX Gamepad)",
				"Cyborg V.3 Rumble Pad",
				"Generic USB Joystick ",
				"MadCatz GamePad (Controller)",
				"Saitek P990 Dual Analog Pad",
				"SL-6566 (Controller)",
				"USB Gamepad ",
				"WingMan RumblePad",
				"XBOX 360 For Windows (Controller)",
				"XEOX Gamepad (Controller)",
				"XEQX Gamepad SL-6556-BK"
			};
			this.LastResortRegex = "360|xbox|catz";
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Handle = "A",
					Target = InputControlType.Action1,
					Source = UnityInputDeviceProfile.Button0
				},
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action2,
					Source = UnityInputDeviceProfile.Button1
				},
				new InputControlMapping
				{
					Handle = "X",
					Target = InputControlType.Action3,
					Source = UnityInputDeviceProfile.Button2
				},
				new InputControlMapping
				{
					Handle = "Y",
					Target = InputControlType.Action4,
					Source = UnityInputDeviceProfile.Button3
				},
				new InputControlMapping
				{
					Handle = "Left Bumper",
					Target = InputControlType.LeftBumper,
					Source = UnityInputDeviceProfile.Button4
				},
				new InputControlMapping
				{
					Handle = "Right Bumper",
					Target = InputControlType.RightBumper,
					Source = UnityInputDeviceProfile.Button5
				},
				new InputControlMapping
				{
					Handle = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = UnityInputDeviceProfile.Button8
				},
				new InputControlMapping
				{
					Handle = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = UnityInputDeviceProfile.Button9
				},
				new InputControlMapping
				{
					Handle = "Back",
					Target = InputControlType.Back,
					Source = UnityInputDeviceProfile.Button6
				},
				new InputControlMapping
				{
					Handle = "Start",
					Target = InputControlType.Start,
					Source = UnityInputDeviceProfile.Button7
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				UnityInputDeviceProfile.LeftStickLeftMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.LeftStickRightMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.LeftStickUpMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.LeftStickDownMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.RightStickLeftMapping(UnityInputDeviceProfile.Analog3),
				UnityInputDeviceProfile.RightStickRightMapping(UnityInputDeviceProfile.Analog3),
				UnityInputDeviceProfile.RightStickUpMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.RightStickDownMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadUpMapping2(UnityInputDeviceProfile.Analog6),
				UnityInputDeviceProfile.DPadDownMapping2(UnityInputDeviceProfile.Analog6),
				new InputControlMapping
				{
					Handle = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = UnityInputDeviceProfile.Analog2,
					SourceRange = InputRange.ZeroToOne,
					TargetRange = InputRange.ZeroToOne
				},
				new InputControlMapping
				{
					Handle = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = UnityInputDeviceProfile.Analog2,
					SourceRange = InputRange.ZeroToMinusOne,
					TargetRange = InputRange.ZeroToOne
				},
				new InputControlMapping
				{
					Handle = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = UnityInputDeviceProfile.Analog8
				},
				new InputControlMapping
				{
					Handle = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = UnityInputDeviceProfile.Analog9
				}
			};
		}
	}
}
