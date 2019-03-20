using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000B1 RID: 177
	public class UnityInputDeviceProfile : InputDeviceProfile
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x0000C851 File Offset: 0x0000AC51
		public UnityInputDeviceProfile()
		{
			base.Sensitivity = 1f;
			base.LowerDeadZone = 0.2f;
			base.UpperDeadZone = 0.9f;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000C87A File Offset: 0x0000AC7A
		public override bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C880 File Offset: 0x0000AC80
		public override bool IsJoystick
		{
			get
			{
				return this.LastResortRegex != null || (this.JoystickNames != null && this.JoystickNames.Length > 0) || (this.JoystickRegex != null && this.JoystickRegex.Length > 0);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000C8D0 File Offset: 0x0000ACD0
		public override bool HasJoystickName(string joystickName)
		{
			if (base.IsNotJoystick)
			{
				return false;
			}
			if (this.JoystickNames != null && this.JoystickNames.Contains(joystickName, StringComparer.OrdinalIgnoreCase))
			{
				return true;
			}
			if (this.JoystickRegex != null)
			{
				for (int i = 0; i < this.JoystickRegex.Length; i++)
				{
					if (Regex.IsMatch(joystickName, this.JoystickRegex[i], RegexOptions.IgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000C948 File Offset: 0x0000AD48
		public override bool HasLastResortRegex(string joystickName)
		{
			return !base.IsNotJoystick && this.LastResortRegex != null && Regex.IsMatch(joystickName, this.LastResortRegex, RegexOptions.IgnoreCase);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000C971 File Offset: 0x0000AD71
		public override bool HasJoystickOrRegexName(string joystickName)
		{
			return this.HasJoystickName(joystickName) || this.HasLastResortRegex(joystickName);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000C989 File Offset: 0x0000AD89
		protected static InputControlSource Button(int index)
		{
			return new UnityButtonSource(index);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000C991 File Offset: 0x0000AD91
		protected static InputControlSource Analog(int index)
		{
			return new UnityAnalogSource(index);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000C99C File Offset: 0x0000AD9C
		protected static InputControlMapping LeftStickLeftMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Left",
				Target = InputControlType.LeftStickLeft,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000C9E0 File Offset: 0x0000ADE0
		protected static InputControlMapping LeftStickRightMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Right",
				Target = InputControlType.LeftStickRight,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000CA24 File Offset: 0x0000AE24
		protected static InputControlMapping LeftStickUpMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Up",
				Target = InputControlType.LeftStickUp,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000CA68 File Offset: 0x0000AE68
		protected static InputControlMapping LeftStickDownMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Stick Down",
				Target = InputControlType.LeftStickDown,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000CAAC File Offset: 0x0000AEAC
		protected static InputControlMapping RightStickLeftMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Left",
				Target = InputControlType.RightStickLeft,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000CAF0 File Offset: 0x0000AEF0
		protected static InputControlMapping RightStickRightMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Right",
				Target = InputControlType.RightStickRight,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000CB34 File Offset: 0x0000AF34
		protected static InputControlMapping RightStickUpMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Up",
				Target = InputControlType.RightStickUp,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000CB78 File Offset: 0x0000AF78
		protected static InputControlMapping RightStickDownMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Stick Down",
				Target = InputControlType.RightStickDown,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000CBBC File Offset: 0x0000AFBC
		protected static InputControlMapping LeftTriggerMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Left Trigger",
				Target = InputControlType.LeftTrigger,
				Source = analog,
				SourceRange = InputRange.MinusOneToOne,
				TargetRange = InputRange.ZeroToOne,
				IgnoreInitialZeroValue = true
			};
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000CC08 File Offset: 0x0000B008
		protected static InputControlMapping RightTriggerMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "Right Trigger",
				Target = InputControlType.RightTrigger,
				Source = analog,
				SourceRange = InputRange.MinusOneToOne,
				TargetRange = InputRange.ZeroToOne,
				IgnoreInitialZeroValue = true
			};
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000CC54 File Offset: 0x0000B054
		protected static InputControlMapping DPadLeftMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Left",
				Target = InputControlType.DPadLeft,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000CC98 File Offset: 0x0000B098
		protected static InputControlMapping DPadRightMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Right",
				Target = InputControlType.DPadRight,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000CCDC File Offset: 0x0000B0DC
		protected static InputControlMapping DPadUpMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000CD20 File Offset: 0x0000B120
		protected static InputControlMapping DPadDownMapping(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000CD64 File Offset: 0x0000B164
		protected static InputControlMapping DPadUpMapping2(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Up",
				Target = InputControlType.DPadUp,
				Source = analog,
				SourceRange = InputRange.ZeroToOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000CDA8 File Offset: 0x0000B1A8
		protected static InputControlMapping DPadDownMapping2(InputControlSource analog)
		{
			return new InputControlMapping
			{
				Handle = "DPad Down",
				Target = InputControlType.DPadDown,
				Source = analog,
				SourceRange = InputRange.ZeroToMinusOne,
				TargetRange = InputRange.ZeroToOne
			};
		}

		// Token: 0x040002B8 RID: 696
		[SerializeField]
		protected string[] JoystickNames;

		// Token: 0x040002B9 RID: 697
		[SerializeField]
		protected string[] JoystickRegex;

		// Token: 0x040002BA RID: 698
		[SerializeField]
		protected string LastResortRegex;

		// Token: 0x040002BB RID: 699
		protected static InputControlSource Button0 = UnityInputDeviceProfile.Button(0);

		// Token: 0x040002BC RID: 700
		protected static InputControlSource Button1 = UnityInputDeviceProfile.Button(1);

		// Token: 0x040002BD RID: 701
		protected static InputControlSource Button2 = UnityInputDeviceProfile.Button(2);

		// Token: 0x040002BE RID: 702
		protected static InputControlSource Button3 = UnityInputDeviceProfile.Button(3);

		// Token: 0x040002BF RID: 703
		protected static InputControlSource Button4 = UnityInputDeviceProfile.Button(4);

		// Token: 0x040002C0 RID: 704
		protected static InputControlSource Button5 = UnityInputDeviceProfile.Button(5);

		// Token: 0x040002C1 RID: 705
		protected static InputControlSource Button6 = UnityInputDeviceProfile.Button(6);

		// Token: 0x040002C2 RID: 706
		protected static InputControlSource Button7 = UnityInputDeviceProfile.Button(7);

		// Token: 0x040002C3 RID: 707
		protected static InputControlSource Button8 = UnityInputDeviceProfile.Button(8);

		// Token: 0x040002C4 RID: 708
		protected static InputControlSource Button9 = UnityInputDeviceProfile.Button(9);

		// Token: 0x040002C5 RID: 709
		protected static InputControlSource Button10 = UnityInputDeviceProfile.Button(10);

		// Token: 0x040002C6 RID: 710
		protected static InputControlSource Button11 = UnityInputDeviceProfile.Button(11);

		// Token: 0x040002C7 RID: 711
		protected static InputControlSource Button12 = UnityInputDeviceProfile.Button(12);

		// Token: 0x040002C8 RID: 712
		protected static InputControlSource Button13 = UnityInputDeviceProfile.Button(13);

		// Token: 0x040002C9 RID: 713
		protected static InputControlSource Button14 = UnityInputDeviceProfile.Button(14);

		// Token: 0x040002CA RID: 714
		protected static InputControlSource Button15 = UnityInputDeviceProfile.Button(15);

		// Token: 0x040002CB RID: 715
		protected static InputControlSource Button16 = UnityInputDeviceProfile.Button(16);

		// Token: 0x040002CC RID: 716
		protected static InputControlSource Button17 = UnityInputDeviceProfile.Button(17);

		// Token: 0x040002CD RID: 717
		protected static InputControlSource Button18 = UnityInputDeviceProfile.Button(18);

		// Token: 0x040002CE RID: 718
		protected static InputControlSource Button19 = UnityInputDeviceProfile.Button(19);

		// Token: 0x040002CF RID: 719
		protected static InputControlSource Analog0 = UnityInputDeviceProfile.Analog(0);

		// Token: 0x040002D0 RID: 720
		protected static InputControlSource Analog1 = UnityInputDeviceProfile.Analog(1);

		// Token: 0x040002D1 RID: 721
		protected static InputControlSource Analog2 = UnityInputDeviceProfile.Analog(2);

		// Token: 0x040002D2 RID: 722
		protected static InputControlSource Analog3 = UnityInputDeviceProfile.Analog(3);

		// Token: 0x040002D3 RID: 723
		protected static InputControlSource Analog4 = UnityInputDeviceProfile.Analog(4);

		// Token: 0x040002D4 RID: 724
		protected static InputControlSource Analog5 = UnityInputDeviceProfile.Analog(5);

		// Token: 0x040002D5 RID: 725
		protected static InputControlSource Analog6 = UnityInputDeviceProfile.Analog(6);

		// Token: 0x040002D6 RID: 726
		protected static InputControlSource Analog7 = UnityInputDeviceProfile.Analog(7);

		// Token: 0x040002D7 RID: 727
		protected static InputControlSource Analog8 = UnityInputDeviceProfile.Analog(8);

		// Token: 0x040002D8 RID: 728
		protected static InputControlSource Analog9 = UnityInputDeviceProfile.Analog(9);

		// Token: 0x040002D9 RID: 729
		protected static InputControlSource Analog10 = UnityInputDeviceProfile.Analog(10);

		// Token: 0x040002DA RID: 730
		protected static InputControlSource Analog11 = UnityInputDeviceProfile.Analog(11);

		// Token: 0x040002DB RID: 731
		protected static InputControlSource Analog12 = UnityInputDeviceProfile.Analog(12);

		// Token: 0x040002DC RID: 732
		protected static InputControlSource Analog13 = UnityInputDeviceProfile.Analog(13);

		// Token: 0x040002DD RID: 733
		protected static InputControlSource Analog14 = UnityInputDeviceProfile.Analog(14);

		// Token: 0x040002DE RID: 734
		protected static InputControlSource Analog15 = UnityInputDeviceProfile.Analog(15);

		// Token: 0x040002DF RID: 735
		protected static InputControlSource Analog16 = UnityInputDeviceProfile.Analog(16);

		// Token: 0x040002E0 RID: 736
		protected static InputControlSource Analog17 = UnityInputDeviceProfile.Analog(17);

		// Token: 0x040002E1 RID: 737
		protected static InputControlSource Analog18 = UnityInputDeviceProfile.Analog(18);

		// Token: 0x040002E2 RID: 738
		protected static InputControlSource Analog19 = UnityInputDeviceProfile.Analog(19);

		// Token: 0x040002E3 RID: 739
		protected static InputControlSource MenuKey = new UnityKeyCodeSource(new KeyCode[]
		{
			KeyCode.Menu
		});

		// Token: 0x040002E4 RID: 740
		protected static InputControlSource EscapeKey = new UnityKeyCodeSource(new KeyCode[]
		{
			KeyCode.Escape
		});
	}
}
