using System;
using System.Collections.Generic;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000AF RID: 175
	public class UnityInputDeviceManager : InputDeviceManager
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0001F44D File Offset: 0x0001D84D
		public UnityInputDeviceManager()
		{
			this.AddSystemDeviceProfiles();
			this.QueryJoystickInfo();
			this.AttachDevices();
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001F480 File Offset: 0x0001D880
		public override void Update(ulong updateTick, float deltaTime)
		{
			this.deviceRefreshTimer += deltaTime;
			if (this.deviceRefreshTimer >= 1f)
			{
				this.deviceRefreshTimer = 0f;
				this.QueryJoystickInfo();
				if (this.JoystickInfoHasChanged)
				{
					Logger.LogInfo("Change in attached Unity joysticks detected; refreshing device list.");
					this.DetachDevices();
					this.AttachDevices();
				}
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001F4E0 File Offset: 0x0001D8E0
		private void QueryJoystickInfo()
		{
			this.joystickNames = Input.GetJoystickNames();
			this.joystickCount = this.joystickNames.Length;
			this.joystickHash = 527 + this.joystickCount;
			for (int i = 0; i < this.joystickCount; i++)
			{
				this.joystickHash = this.joystickHash * 31 + this.joystickNames[i].GetHashCode();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0001F54C File Offset: 0x0001D94C
		private bool JoystickInfoHasChanged
		{
			get
			{
				return this.joystickHash != this.lastJoystickHash || this.joystickCount != this.lastJoystickCount;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001F573 File Offset: 0x0001D973
		private void AttachDevices()
		{
			this.AttachKeyboardDevices();
			this.AttachJoystickDevices();
			this.lastJoystickCount = this.joystickCount;
			this.lastJoystickHash = this.joystickHash;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001F59C File Offset: 0x0001D99C
		private void DetachDevices()
		{
			int count = this.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.DetachDevice(this.devices[i]);
			}
			this.devices.Clear();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001F5E3 File Offset: 0x0001D9E3
		public void ReloadDevices()
		{
			this.QueryJoystickInfo();
			this.DetachDevices();
			this.AttachDevices();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001F5F7 File Offset: 0x0001D9F7
		private void AttachDevice(UnityInputDevice device)
		{
			this.devices.Add(device);
			InputManager.AttachDevice(device);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001F60C File Offset: 0x0001DA0C
		private void AttachKeyboardDevices()
		{
			int count = this.systemDeviceProfiles.Count;
			for (int i = 0; i < count; i++)
			{
				InputDeviceProfile inputDeviceProfile = this.systemDeviceProfiles[i];
				if (inputDeviceProfile.IsNotJoystick && inputDeviceProfile.IsSupportedOnThisPlatform)
				{
					this.AttachDevice(new UnityInputDevice(inputDeviceProfile));
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001F668 File Offset: 0x0001DA68
		private void AttachJoystickDevices()
		{
			try
			{
				for (int i = 0; i < this.joystickCount; i++)
				{
					this.DetectJoystickDevice(i + 1, this.joystickNames[i]);
				}
			}
			catch (Exception ex)
			{
				Logger.LogError(ex.Message);
				Logger.LogError(ex.StackTrace);
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001F6D0 File Offset: 0x0001DAD0
		private bool HasAttachedDeviceWithJoystickId(int unityJoystickId)
		{
			int count = this.devices.Count;
			for (int i = 0; i < count; i++)
			{
				UnityInputDevice unityInputDevice = this.devices[i] as UnityInputDevice;
				if (unityInputDevice != null && unityInputDevice.JoystickId == unityJoystickId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001F724 File Offset: 0x0001DB24
		private void DetectJoystickDevice(int unityJoystickId, string unityJoystickName)
		{
			if (this.HasAttachedDeviceWithJoystickId(unityJoystickId))
			{
				return;
			}
			if (unityJoystickName.IndexOf("webcam", StringComparison.OrdinalIgnoreCase) != -1)
			{
				return;
			}
			if (InputManager.UnityVersion < new VersionInfo(4, 5, 0, 0) && (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) && unityJoystickName == "Unknown Wireless Controller")
			{
				return;
			}
			if (InputManager.UnityVersion >= new VersionInfo(4, 6, 3, 0) && (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) && string.IsNullOrEmpty(unityJoystickName))
			{
				return;
			}
			InputDeviceProfile inputDeviceProfile = null;
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.customDeviceProfiles.Find((InputDeviceProfile config) => config.HasJoystickName(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.systemDeviceProfiles.Find((InputDeviceProfile config) => config.HasJoystickName(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.customDeviceProfiles.Find((InputDeviceProfile config) => config.HasLastResortRegex(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				inputDeviceProfile = this.systemDeviceProfiles.Find((InputDeviceProfile config) => config.HasLastResortRegex(unityJoystickName));
			}
			if (inputDeviceProfile == null)
			{
				Logger.LogWarning(string.Concat(new object[]
				{
					"Device ",
					unityJoystickId,
					" with name \"",
					unityJoystickName,
					"\" does not match any supported profiles and will be considered an unknown controller."
				}));
				UnknownUnityDeviceProfile profile = new UnknownUnityDeviceProfile(unityJoystickName);
				UnknownUnityInputDevice device = new UnknownUnityInputDevice(profile, unityJoystickId);
				this.AttachDevice(device);
				return;
			}
			if (!inputDeviceProfile.IsHidden)
			{
				UnityInputDevice device2 = new UnityInputDevice(inputDeviceProfile, unityJoystickId);
				this.AttachDevice(device2);
				Logger.LogInfo(string.Concat(new object[]
				{
					"Device ",
					unityJoystickId,
					" matched profile ",
					inputDeviceProfile.GetType().Name,
					" (",
					inputDeviceProfile.Name,
					")"
				}));
			}
			else
			{
				Logger.LogInfo(string.Concat(new object[]
				{
					"Device ",
					unityJoystickId,
					" matching profile ",
					inputDeviceProfile.GetType().Name,
					" (",
					inputDeviceProfile.Name,
					") is hidden and will not be attached."
				}));
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001F992 File Offset: 0x0001DD92
		private void AddSystemDeviceProfile(UnityInputDeviceProfile deviceProfile)
		{
			if (deviceProfile.IsSupportedOnThisPlatform)
			{
				this.systemDeviceProfiles.Add(deviceProfile);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001F9AC File Offset: 0x0001DDAC
		private void AddSystemDeviceProfiles()
		{
			foreach (string typeName in UnityInputDeviceProfileList.Profiles)
			{
				UnityInputDeviceProfile deviceProfile = (UnityInputDeviceProfile)Activator.CreateInstance(Type.GetType(typeName));
				this.AddSystemDeviceProfile(deviceProfile);
			}
		}

		// Token: 0x040002AF RID: 687
		private const float deviceRefreshInterval = 1f;

		// Token: 0x040002B0 RID: 688
		private float deviceRefreshTimer;

		// Token: 0x040002B1 RID: 689
		private List<InputDeviceProfile> systemDeviceProfiles = new List<InputDeviceProfile>();

		// Token: 0x040002B2 RID: 690
		private List<InputDeviceProfile> customDeviceProfiles = new List<InputDeviceProfile>();

		// Token: 0x040002B3 RID: 691
		private string[] joystickNames;

		// Token: 0x040002B4 RID: 692
		private int lastJoystickCount;

		// Token: 0x040002B5 RID: 693
		private int lastJoystickHash;

		// Token: 0x040002B6 RID: 694
		private int joystickCount;

		// Token: 0x040002B7 RID: 695
		private int joystickHash;
	}
}
