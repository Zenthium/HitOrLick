using System;

namespace InControl
{
	// Token: 0x02000081 RID: 129
	[AutoDiscover]
	public class NexusPlayerRemoteProfile : UnityInputDeviceProfile
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000168B0 File Offset: 0x00014CB0
		public NexusPlayerRemoteProfile()
		{
			base.Name = "Nexus Player Remote";
			base.Meta = "Nexus Player Remote";
			base.SupportedPlatforms = new string[]
			{
				"Android"
			};
			this.JoystickNames = new string[]
			{
				"Google Nexus Remote"
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
