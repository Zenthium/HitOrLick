using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200004A RID: 74
	[Obsolete("Custom profiles are deprecated. Use the bindings API instead.", false)]
	public class CustomInputDeviceProfile : InputDeviceProfile
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000C7C4 File Offset: 0x0000ABC4
		public CustomInputDeviceProfile()
		{
			base.Name = "Custom Device Profile";
			base.Meta = "Custom Device Profile";
			base.SupportedPlatforms = new string[]
			{
				"Windows",
				"Mac",
				"Linux"
			};
			base.Sensitivity = 1f;
			base.LowerDeadZone = 0f;
			base.UpperDeadZone = 1f;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000C832 File Offset: 0x0000AC32
		public sealed override bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000C835 File Offset: 0x0000AC35
		public sealed override bool IsJoystick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000C838 File Offset: 0x0000AC38
		public sealed override bool HasJoystickName(string joystickName)
		{
			return false;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000C83B File Offset: 0x0000AC3B
		public sealed override bool HasLastResortRegex(string joystickName)
		{
			return false;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000C83E File Offset: 0x0000AC3E
		public sealed override bool HasJoystickOrRegexName(string joystickName)
		{
			return false;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C841 File Offset: 0x0000AC41
		protected static InputControlSource KeyCodeButton(params KeyCode[] keyCodeList)
		{
			return new UnityKeyCodeSource(keyCodeList);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C849 File Offset: 0x0000AC49
		protected static InputControlSource KeyCodeComboButton(params KeyCode[] keyCodeList)
		{
			return new UnityKeyCodeComboSource(keyCodeList);
		}
	}
}
