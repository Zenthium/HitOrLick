using System;
using System.Collections.Generic;
using FreeLives;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200001B RID: 27
	public class InControlManager : SingletonMonoBehavior<InControlManager>
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000067B0 File Offset: 0x00004BB0
		private void OnEnable()
		{
			if (!base.SetupSingleton())
			{
				return;
			}
			InputManager.InvertYAxis = this.invertYAxis;
			InputManager.EnableXInput = this.enableXInput;
			InputManager.XInputUpdateRate = (uint)Mathf.Max(this.xInputUpdateRate, 0);
			InputManager.XInputBufferSize = (uint)Mathf.Max(this.xInputBufferSize, 0);
			InputManager.EnableICade = this.enableICade;
			if (InputManager.SetupInternal())
			{
				if (this.logDebugInfo)
				{
					Debug.Log("InControl (version " + InputManager.Version + ")");
					Logger.OnLogMessage += this.LogMessage;
				}
				foreach (string text in this.customProfiles)
				{
					Type type = Type.GetType(text);
					if (type == null)
					{
						Debug.LogError("Cannot find class for custom profile: " + text);
					}
					else
					{
						InputDeviceProfile inputDeviceProfile = Activator.CreateInstance(type) as InputDeviceProfile;
						if (inputDeviceProfile != null)
						{
							InputManager.AttachDevice(new UnityInputDevice(inputDeviceProfile));
						}
					}
				}
			}
			if (this.dontDestroyOnLoad)
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000068E8 File Offset: 0x00004CE8
		private void OnDisable()
		{
			if (SingletonMonoBehavior<InControlManager>.Instance == this)
			{
				InputManager.ResetInternal();
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000068FF File Offset: 0x00004CFF
		private void Update()
		{
			if (!this.useFixedUpdate || Utility.IsZero(Time.timeScale))
			{
				InputManager.UpdateInternal();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006920 File Offset: 0x00004D20
		private void Start()
		{
			InputManager.OnDeviceAttached += InputReader.DeviceAttached;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006933 File Offset: 0x00004D33
		private void FixedUpdate()
		{
			if (this.useFixedUpdate)
			{
				InputManager.UpdateInternal();
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006945 File Offset: 0x00004D45
		private void OnApplicationFocus(bool focusState)
		{
			InputManager.OnApplicationFocus(focusState);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000694D File Offset: 0x00004D4D
		private void OnApplicationPause(bool pauseState)
		{
			InputManager.OnApplicationPause(pauseState);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006955 File Offset: 0x00004D55
		private void OnApplicationQuit()
		{
			InputManager.OnApplicationQuit();
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000695C File Offset: 0x00004D5C
		private void OnLevelWasLoaded(int level)
		{
			InputManager.OnLevelWasLoaded();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006964 File Offset: 0x00004D64
		private void LogMessage(LogMessage logMessage)
		{
			LogMessageType type = logMessage.type;
			if (type != LogMessageType.Info)
			{
				if (type != LogMessageType.Warning)
				{
					if (type == LogMessageType.Error)
					{
						Debug.LogError(logMessage.text);
					}
				}
				else
				{
					Debug.LogWarning(logMessage.text);
				}
			}
			else
			{
				Debug.Log(logMessage.text);
			}
		}

		// Token: 0x040000DB RID: 219
		public bool logDebugInfo;

		// Token: 0x040000DC RID: 220
		public bool invertYAxis;

		// Token: 0x040000DD RID: 221
		public bool useFixedUpdate;

		// Token: 0x040000DE RID: 222
		public bool dontDestroyOnLoad;

		// Token: 0x040000DF RID: 223
		public bool enableXInput;

		// Token: 0x040000E0 RID: 224
		public int xInputUpdateRate;

		// Token: 0x040000E1 RID: 225
		public int xInputBufferSize;

		// Token: 0x040000E2 RID: 226
		public bool enableICade;

		// Token: 0x040000E3 RID: 227
		public List<string> customProfiles = new List<string>();
	}
}
