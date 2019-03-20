﻿using System;

namespace InControl
{
	// Token: 0x0200005E RID: 94
	[AutoDiscover]
	public class HamaBlackForceWinProfile : UnityInputDeviceProfile
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00010074 File Offset: 0x0000E474
		public HamaBlackForceWinProfile()
		{
			base.Name = "Hama Black Force Controller";
			base.Meta = "Hama Black Force Controller on Windows";
			base.SupportedPlatforms = new string[]
			{
				"Windows"
			};
			this.JoystickNames = new string[]
			{
				"Generic   USB  Joystick  "
			};
			base.ButtonMappings = new InputControlMapping[]
			{
				new InputControlMapping
				{
					Handle = "3",
					Target = InputControlType.Action1,
					Source = UnityInputDeviceProfile.Button2
				},
				new InputControlMapping
				{
					Handle = "2",
					Target = InputControlType.Action2,
					Source = UnityInputDeviceProfile.Button1
				},
				new InputControlMapping
				{
					Handle = "4",
					Target = InputControlType.Action3,
					Source = UnityInputDeviceProfile.Button3
				},
				new InputControlMapping
				{
					Handle = "1",
					Target = InputControlType.Action4,
					Source = UnityInputDeviceProfile.Button0
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
					Handle = "Left Trigger",
					Target = InputControlType.LeftTrigger,
					Source = UnityInputDeviceProfile.Button6
				},
				new InputControlMapping
				{
					Handle = "Right Trigger",
					Target = InputControlType.RightTrigger,
					Source = UnityInputDeviceProfile.Button7
				},
				new InputControlMapping
				{
					Handle = "B9",
					Target = InputControlType.Select,
					Source = UnityInputDeviceProfile.Button8
				},
				new InputControlMapping
				{
					Handle = "Left Stick Button",
					Target = InputControlType.LeftStickButton,
					Source = UnityInputDeviceProfile.Button10
				},
				new InputControlMapping
				{
					Handle = "Right Stick Button",
					Target = InputControlType.RightStickButton,
					Source = UnityInputDeviceProfile.Button11
				},
				new InputControlMapping
				{
					Handle = "B10",
					Target = InputControlType.Start,
					Source = UnityInputDeviceProfile.Button9
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				UnityInputDeviceProfile.LeftStickLeftMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.LeftStickRightMapping(UnityInputDeviceProfile.Analog0),
				UnityInputDeviceProfile.LeftStickUpMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.LeftStickDownMapping(UnityInputDeviceProfile.Analog1),
				UnityInputDeviceProfile.RightStickLeftMapping(UnityInputDeviceProfile.Analog2),
				UnityInputDeviceProfile.RightStickRightMapping(UnityInputDeviceProfile.Analog2),
				UnityInputDeviceProfile.RightStickUpMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.RightStickDownMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadUpMapping(UnityInputDeviceProfile.Analog6),
				UnityInputDeviceProfile.DPadDownMapping(UnityInputDeviceProfile.Analog6)
			};
		}
	}
}
