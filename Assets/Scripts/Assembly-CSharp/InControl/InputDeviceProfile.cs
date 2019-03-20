using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000AD RID: 173
	public abstract class InputDeviceProfile
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0000C4DC File Offset: 0x0000A8DC
		public InputDeviceProfile()
		{
			this.Name = string.Empty;
			this.Meta = string.Empty;
			this.AnalogMappings = new InputControlMapping[0];
			this.ButtonMappings = new InputControlMapping[0];
			this.SupportedPlatforms = new string[0];
			this.ExcludePlatforms = new string[0];
			this.MinUnityVersion = new VersionInfo(3, 0, 0, 0);
			this.MaxUnityVersion = new VersionInfo(9, 0, 0, 0);
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000C56A File Offset: 0x0000A96A
		// (set) Token: 0x0600036D RID: 877 RVA: 0x0000C572 File Offset: 0x0000A972
		[SerializeField]
		public string Name { get; protected set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0000C57B File Offset: 0x0000A97B
		// (set) Token: 0x0600036F RID: 879 RVA: 0x0000C583 File Offset: 0x0000A983
		[SerializeField]
		public string Meta { get; protected set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000370 RID: 880 RVA: 0x0000C58C File Offset: 0x0000A98C
		// (set) Token: 0x06000371 RID: 881 RVA: 0x0000C594 File Offset: 0x0000A994
		[SerializeField]
		public InputControlMapping[] AnalogMappings { get; protected set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000C59D File Offset: 0x0000A99D
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000C5A5 File Offset: 0x0000A9A5
		[SerializeField]
		public InputControlMapping[] ButtonMappings { get; protected set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000C5AE File Offset: 0x0000A9AE
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000C5B6 File Offset: 0x0000A9B6
		[SerializeField]
		public string[] SupportedPlatforms { get; protected set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000C5BF File Offset: 0x0000A9BF
		// (set) Token: 0x06000377 RID: 887 RVA: 0x0000C5C7 File Offset: 0x0000A9C7
		[SerializeField]
		public string[] ExcludePlatforms { get; protected set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0000C5D0 File Offset: 0x0000A9D0
		// (set) Token: 0x06000379 RID: 889 RVA: 0x0000C5D8 File Offset: 0x0000A9D8
		[SerializeField]
		public VersionInfo MinUnityVersion { get; protected set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600037A RID: 890 RVA: 0x0000C5E1 File Offset: 0x0000A9E1
		// (set) Token: 0x0600037B RID: 891 RVA: 0x0000C5E9 File Offset: 0x0000A9E9
		[SerializeField]
		public VersionInfo MaxUnityVersion { get; protected set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000C5F2 File Offset: 0x0000A9F2
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000C5FA File Offset: 0x0000A9FA
		[SerializeField]
		public float Sensitivity
		{
			get
			{
				return this.sensitivity;
			}
			protected set
			{
				this.sensitivity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000C608 File Offset: 0x0000AA08
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000C610 File Offset: 0x0000AA10
		[SerializeField]
		public float LowerDeadZone
		{
			get
			{
				return this.lowerDeadZone;
			}
			protected set
			{
				this.lowerDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000C61E File Offset: 0x0000AA1E
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000C626 File Offset: 0x0000AA26
		[SerializeField]
		public float UpperDeadZone
		{
			get
			{
				return this.upperDeadZone;
			}
			protected set
			{
				this.upperDeadZone = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000C634 File Offset: 0x0000AA34
		public bool IsSupportedOnThisPlatform
		{
			get
			{
				if (!this.IsSupportedOnThisVersionOfUnity)
				{
					return false;
				}
				if (this.ExcludePlatforms != null)
				{
					foreach (string text in this.ExcludePlatforms)
					{
						if (InputManager.Platform.Contains(text.ToUpper()))
						{
							return false;
						}
					}
				}
				if (this.SupportedPlatforms == null || this.SupportedPlatforms.Length == 0)
				{
					return true;
				}
				foreach (string text2 in this.SupportedPlatforms)
				{
					if (InputManager.Platform.Contains(text2.ToUpper()))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000C6E8 File Offset: 0x0000AAE8
		private bool IsSupportedOnThisVersionOfUnity
		{
			get
			{
				VersionInfo a = VersionInfo.UnityVersion();
				return a >= this.MinUnityVersion && a <= this.MaxUnityVersion;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000384 RID: 900
		public abstract bool IsKnown { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000385 RID: 901
		public abstract bool IsJoystick { get; }

		// Token: 0x06000386 RID: 902
		public abstract bool HasJoystickName(string joystickName);

		// Token: 0x06000387 RID: 903
		public abstract bool HasLastResortRegex(string joystickName);

		// Token: 0x06000388 RID: 904
		public abstract bool HasJoystickOrRegexName(string joystickName);

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000C71B File Offset: 0x0000AB1B
		public bool IsNotJoystick
		{
			get
			{
				return !this.IsJoystick;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000C726 File Offset: 0x0000AB26
		internal static void Hide(Type type)
		{
			InputDeviceProfile.hideList.Add(type);
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000C734 File Offset: 0x0000AB34
		internal bool IsHidden
		{
			get
			{
				return InputDeviceProfile.hideList.Contains(base.GetType());
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000C746 File Offset: 0x0000AB46
		public int AnalogCount
		{
			get
			{
				return this.AnalogMappings.Length;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000C750 File Offset: 0x0000AB50
		public int ButtonCount
		{
			get
			{
				return this.ButtonMappings.Length;
			}
		}

		// Token: 0x040002A0 RID: 672
		private static HashSet<Type> hideList = new HashSet<Type>();

		// Token: 0x040002A1 RID: 673
		private float sensitivity = 1f;

		// Token: 0x040002A2 RID: 674
		private float lowerDeadZone;

		// Token: 0x040002A3 RID: 675
		private float upperDeadZone = 1f;

		// Token: 0x040002A4 RID: 676
		protected static InputControlSource MouseButton0 = new UnityMouseButtonSource(0);

		// Token: 0x040002A5 RID: 677
		protected static InputControlSource MouseButton1 = new UnityMouseButtonSource(1);

		// Token: 0x040002A6 RID: 678
		protected static InputControlSource MouseButton2 = new UnityMouseButtonSource(2);

		// Token: 0x040002A7 RID: 679
		protected static InputControlSource MouseXAxis = new UnityMouseAxisSource("x");

		// Token: 0x040002A8 RID: 680
		protected static InputControlSource MouseYAxis = new UnityMouseAxisSource("y");

		// Token: 0x040002A9 RID: 681
		protected static InputControlSource MouseScrollWheel = new UnityMouseAxisSource("z");
	}
}
