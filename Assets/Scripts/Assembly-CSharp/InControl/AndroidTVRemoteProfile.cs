using System;

namespace InControl
{
	// Token: 0x02000051 RID: 81
	[AutoDiscover]
	public class AndroidTVRemoteProfile : UnityInputDeviceProfile
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000DFFC File Offset: 0x0000C3FC
		public AndroidTVRemoteProfile()
		{
			base.Name = "Android TV Remote";
			base.Meta = "Android TV Remote on Android TV";
			base.SupportedPlatforms = new string[]
			{
				"Android"
			};
			this.JoystickNames = new string[]
			{
				string.Empty,
				"touch-input",
				"navigation-input"
			};
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
					Handle = "Back",
					Target = InputControlType.Back,
					Source = UnityInputDeviceProfile.EscapeKey
				}
			};
			base.AnalogMappings = new InputControlMapping[]
			{
				UnityInputDeviceProfile.DPadLeftMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.DPadRightMapping(UnityInputDeviceProfile.Analog4),
				UnityInputDeviceProfile.DPadUpMapping(UnityInputDeviceProfile.Analog5),
				UnityInputDeviceProfile.DPadDownMapping(UnityInputDeviceProfile.Analog5)
			};
		}
	}
}
