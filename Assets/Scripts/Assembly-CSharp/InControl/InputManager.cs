using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000030 RID: 48
	public class InputManager
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001CF RID: 463 RVA: 0x0000850C File Offset: 0x0000690C
		// (remove) Token: 0x060001D0 RID: 464 RVA: 0x00008540 File Offset: 0x00006940
		public static event Action OnSetup;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001D1 RID: 465 RVA: 0x00008574 File Offset: 0x00006974
		// (remove) Token: 0x060001D2 RID: 466 RVA: 0x000085A8 File Offset: 0x000069A8
		public static event Action<ulong, float> OnUpdate;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001D3 RID: 467 RVA: 0x000085DC File Offset: 0x000069DC
		// (remove) Token: 0x060001D4 RID: 468 RVA: 0x00008610 File Offset: 0x00006A10
		public static event Action OnReset;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001D5 RID: 469 RVA: 0x00008644 File Offset: 0x00006A44
		// (remove) Token: 0x060001D6 RID: 470 RVA: 0x00008678 File Offset: 0x00006A78
		public static event Action<InputDevice> OnDeviceAttached;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001D7 RID: 471 RVA: 0x000086AC File Offset: 0x00006AAC
		// (remove) Token: 0x060001D8 RID: 472 RVA: 0x000086E0 File Offset: 0x00006AE0
		public static event Action<InputDevice> OnDeviceDetached;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060001D9 RID: 473 RVA: 0x00008714 File Offset: 0x00006B14
		// (remove) Token: 0x060001DA RID: 474 RVA: 0x00008748 File Offset: 0x00006B48
		public static event Action<InputDevice> OnActiveDeviceChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001DB RID: 475 RVA: 0x0000877C File Offset: 0x00006B7C
		// (remove) Token: 0x060001DC RID: 476 RVA: 0x000087B0 File Offset: 0x00006BB0
		internal static event Action<ulong, float> OnUpdateDevices;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001DD RID: 477 RVA: 0x000087E4 File Offset: 0x00006BE4
		// (remove) Token: 0x060001DE RID: 478 RVA: 0x00008818 File Offset: 0x00006C18
		internal static event Action<ulong, float> OnCommitDevices;

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000884C File Offset: 0x00006C4C
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00008853 File Offset: 0x00006C53
		public static bool MenuWasPressed { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000885B File Offset: 0x00006C5B
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00008862 File Offset: 0x00006C62
		public static bool InvertYAxis { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000886A File Offset: 0x00006C6A
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00008871 File Offset: 0x00006C71
		public static bool IsSetup { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00008879 File Offset: 0x00006C79
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00008880 File Offset: 0x00006C80
		internal static string Platform { get; private set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x00008888 File Offset: 0x00006C88
		[Obsolete("Calling InputManager.Setup() directly is no longer supported. Use the InControlManager component to manage the lifecycle of the input manager instead.", true)]
		public static void Setup()
		{
			InputManager.SetupInternal();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008890 File Offset: 0x00006C90
		internal static bool SetupInternal()
		{
			if (InputManager.IsSetup)
			{
				return false;
			}
			InputManager.Platform = (Utility.GetWindowsVersion() + " " + SystemInfo.deviceModel).ToUpper();
			InputManager.initialTime = 0f;
			InputManager.currentTime = 0f;
			InputManager.lastUpdateTime = 0f;
			InputManager.currentTick = 0UL;
			InputManager.deviceManagers.Clear();
			InputManager.deviceManagerTable.Clear();
			InputManager.devices.Clear();
			InputManager.Devices = new ReadOnlyCollection<InputDevice>(InputManager.devices);
			InputManager.activeDevice = InputDevice.Null;
			InputManager.playerActionSets.Clear();
			InputManager.IsSetup = true;
			if (InputManager.EnableXInput)
			{
				XInputDeviceManager.Enable();
			}
			if (InputManager.OnSetup != null)
			{
				InputManager.OnSetup();
				InputManager.OnSetup = null;
			}
			bool flag = true;
			if (flag)
			{
				InputManager.AddDeviceManager<UnityInputDeviceManager>();
			}
			return true;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000896A File Offset: 0x00006D6A
		[Obsolete("Calling InputManager.Reset() method directly is no longer supported. Use the InControlManager component to manage the lifecycle of the input manager instead.", true)]
		public static void Reset()
		{
			InputManager.ResetInternal();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008974 File Offset: 0x00006D74
		internal static void ResetInternal()
		{
			if (InputManager.OnReset != null)
			{
				InputManager.OnReset();
			}
			InputManager.OnSetup = null;
			InputManager.OnUpdate = null;
			InputManager.OnReset = null;
			InputManager.OnActiveDeviceChanged = null;
			InputManager.OnDeviceAttached = null;
			InputManager.OnDeviceDetached = null;
			InputManager.OnUpdateDevices = null;
			InputManager.OnCommitDevices = null;
			InputManager.DestroyDeviceManagers();
			InputManager.DestroyDevices();
			InputManager.playerActionSets.Clear();
			InputManager.IsSetup = false;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000089DF File Offset: 0x00006DDF
		[Obsolete("Calling InputManager.Update() directly is no longer supported. Use the InControlManager component to manage the lifecycle of the input manager instead.", true)]
		public static void Update()
		{
			InputManager.UpdateInternal();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000089E8 File Offset: 0x00006DE8
		internal static void UpdateInternal()
		{
			InputManager.AssertIsSetup();
			if (InputManager.OnSetup != null)
			{
				InputManager.OnSetup();
				InputManager.OnSetup = null;
			}
			InputManager.currentTick += 1UL;
			InputManager.UpdateCurrentTime();
			float num = InputManager.currentTime - InputManager.lastUpdateTime;
			InputManager.UpdateDeviceManagers(num);
			InputManager.MenuWasPressed = false;
			InputManager.UpdateDevices(num);
			InputManager.CommitDevices(num);
			InputManager.UpdatePlayerActionSets(num);
			InputManager.UpdateActiveDevice();
			if (InputManager.OnUpdate != null)
			{
				InputManager.OnUpdate(InputManager.currentTick, num);
			}
			InputManager.lastUpdateTime = InputManager.currentTime;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00008A79 File Offset: 0x00006E79
		public static void Reload()
		{
			InputManager.ResetInternal();
			InputManager.SetupInternal();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008A86 File Offset: 0x00006E86
		private static void AssertIsSetup()
		{
			if (!InputManager.IsSetup)
			{
				throw new Exception("InputManager is not initialized. Call InputManager.Setup() first.");
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008AA0 File Offset: 0x00006EA0
		private static void SetZeroTickOnAllControls()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputControl[] controls = InputManager.devices[i].Controls;
				int num = controls.Length;
				for (int j = 0; j < num; j++)
				{
					InputControl inputControl = controls[j];
					if (inputControl != null)
					{
						inputControl.SetZeroTick();
					}
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008B0C File Offset: 0x00006F0C
		public static void ClearInputState()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.devices[i].ClearInputState();
			}
			int count2 = InputManager.playerActionSets.Count;
			for (int j = 0; j < count2; j++)
			{
				InputManager.playerActionSets[j].ClearInputState();
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008B73 File Offset: 0x00006F73
		internal static void OnApplicationFocus(bool focusState)
		{
			if (!focusState)
			{
				InputManager.SetZeroTickOnAllControls();
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008B80 File Offset: 0x00006F80
		internal static void OnApplicationPause(bool pauseState)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00008B82 File Offset: 0x00006F82
		internal static void OnApplicationQuit()
		{
			InputManager.ResetInternal();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00008B89 File Offset: 0x00006F89
		internal static void OnLevelWasLoaded()
		{
			InputManager.SetZeroTickOnAllControls();
			InputManager.UpdateInternal();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008B98 File Offset: 0x00006F98
		public static void AddDeviceManager(InputDeviceManager deviceManager)
		{
			InputManager.AssertIsSetup();
			Type type = deviceManager.GetType();
			if (InputManager.deviceManagerTable.ContainsKey(type))
			{
				Logger.LogError("A device manager of type '" + type.Name + "' already exists; cannot add another.");
				return;
			}
			InputManager.deviceManagers.Add(deviceManager);
			InputManager.deviceManagerTable.Add(type, deviceManager);
			deviceManager.Update(InputManager.currentTick, InputManager.currentTime - InputManager.lastUpdateTime);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008C09 File Offset: 0x00007009
		public static void AddDeviceManager<T>() where T : InputDeviceManager, new()
		{
			InputManager.AddDeviceManager(Activator.CreateInstance<T>());
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008C1C File Offset: 0x0000701C
		public static T GetDeviceManager<T>() where T : InputDeviceManager
		{
			InputDeviceManager inputDeviceManager;
			if (InputManager.deviceManagerTable.TryGetValue(typeof(T), out inputDeviceManager))
			{
				return inputDeviceManager as T;
			}
			return (T)((object)null);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008C56 File Offset: 0x00007056
		public static bool HasDeviceManager<T>() where T : InputDeviceManager
		{
			return InputManager.deviceManagerTable.ContainsKey(typeof(T));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008C6C File Offset: 0x0000706C
		private static void UpdateCurrentTime()
		{
			if (InputManager.initialTime < 1.401298E-45f)
			{
				InputManager.initialTime = Time.realtimeSinceStartup;
			}
			InputManager.currentTime = Mathf.Max(0f, Time.realtimeSinceStartup - InputManager.initialTime);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008CA4 File Offset: 0x000070A4
		private static void UpdateDeviceManagers(float deltaTime)
		{
			int count = InputManager.deviceManagers.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.deviceManagers[i].Update(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008CE4 File Offset: 0x000070E4
		private static void DestroyDeviceManagers()
		{
			int count = InputManager.deviceManagers.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.deviceManagers[i].Destroy();
			}
			InputManager.deviceManagers.Clear();
			InputManager.deviceManagerTable.Clear();
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008D34 File Offset: 0x00007134
		private static void DestroyDevices()
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.StopVibration();
				inputDevice.IsAttached = false;
			}
			InputManager.devices.Clear();
			InputManager.activeDevice = InputDevice.Null;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008D8C File Offset: 0x0000718C
		private static void UpdateDevices(float deltaTime)
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.Update(InputManager.currentTick, deltaTime);
			}
			if (InputManager.OnUpdateDevices != null)
			{
				InputManager.OnUpdateDevices(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008DE8 File Offset: 0x000071E8
		private static void CommitDevices(float deltaTime)
		{
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice = InputManager.devices[i];
				inputDevice.Commit(InputManager.currentTick, deltaTime);
				if (inputDevice.MenuWasPressed)
				{
					InputManager.MenuWasPressed = true;
				}
			}
			if (InputManager.OnCommitDevices != null)
			{
				InputManager.OnCommitDevices(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008E58 File Offset: 0x00007258
		private static void UpdateActiveDevice()
		{
			InputDevice inputDevice = InputManager.ActiveDevice;
			int count = InputManager.devices.Count;
			for (int i = 0; i < count; i++)
			{
				InputDevice inputDevice2 = InputManager.devices[i];
				if (inputDevice2.LastChangedAfter(InputManager.ActiveDevice))
				{
					InputManager.ActiveDevice = inputDevice2;
				}
			}
			if (inputDevice != InputManager.ActiveDevice && InputManager.OnActiveDeviceChanged != null)
			{
				InputManager.OnActiveDeviceChanged(InputManager.ActiveDevice);
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008ED0 File Offset: 0x000072D0
		public static void AttachDevice(InputDevice inputDevice)
		{
			InputManager.AssertIsSetup();
			if (!inputDevice.IsSupportedOnThisPlatform)
			{
				return;
			}
			if (InputManager.devices.Contains(inputDevice))
			{
				inputDevice.IsAttached = true;
				return;
			}
			InputManager.devices.Add(inputDevice);
			InputManager.devices.Sort((InputDevice d1, InputDevice d2) => d1.SortOrder.CompareTo(d2.SortOrder));
			inputDevice.IsAttached = true;
			if (InputManager.OnDeviceAttached != null)
			{
				InputManager.OnDeviceAttached(inputDevice);
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008F54 File Offset: 0x00007354
		public static void DetachDevice(InputDevice inputDevice)
		{
			if (!inputDevice.IsAttached)
			{
				return;
			}
			if (!InputManager.IsSetup)
			{
				inputDevice.IsAttached = false;
				return;
			}
			if (!InputManager.devices.Contains(inputDevice))
			{
				inputDevice.IsAttached = false;
				return;
			}
			InputManager.devices.Remove(inputDevice);
			inputDevice.IsAttached = false;
			if (InputManager.ActiveDevice == inputDevice)
			{
				InputManager.ActiveDevice = InputDevice.Null;
			}
			if (InputManager.OnDeviceDetached != null)
			{
				InputManager.OnDeviceDetached(inputDevice);
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008FD4 File Offset: 0x000073D4
		public static void HideDevicesWithProfile(Type type)
		{
			if (type.IsSubclassOf(typeof(UnityInputDeviceProfile)))
			{
				InputDeviceProfile.Hide(type);
			}
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008FF1 File Offset: 0x000073F1
		internal static void AttachPlayerActionSet(PlayerActionSet playerActionSet)
		{
			if (InputManager.playerActionSets.Contains(playerActionSet))
			{
				Logger.LogWarning("Player action set is already attached.");
			}
			else
			{
				InputManager.playerActionSets.Add(playerActionSet);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000901D File Offset: 0x0000741D
		internal static void DetachPlayerActionSet(PlayerActionSet playerActionSet)
		{
			InputManager.playerActionSets.Remove(playerActionSet);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000902C File Offset: 0x0000742C
		internal static void UpdatePlayerActionSets(float deltaTime)
		{
			int count = InputManager.playerActionSets.Count;
			for (int i = 0; i < count; i++)
			{
				InputManager.playerActionSets[i].Update(InputManager.currentTick, deltaTime);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000906C File Offset: 0x0000746C
		public static bool AnyKeyIsPressed
		{
			get
			{
				return KeyCombo.Detect(true).Count > 0;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000207 RID: 519 RVA: 0x0000908A File Offset: 0x0000748A
		// (set) Token: 0x06000208 RID: 520 RVA: 0x000090A5 File Offset: 0x000074A5
		public static InputDevice ActiveDevice
		{
			get
			{
				return (InputManager.activeDevice != null) ? InputManager.activeDevice : InputDevice.Null;
			}
			private set
			{
				InputManager.activeDevice = ((value != null) ? value : InputDevice.Null);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000090BD File Offset: 0x000074BD
		// (set) Token: 0x0600020A RID: 522 RVA: 0x000090C4 File Offset: 0x000074C4
		public static bool EnableXInput { get; internal set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000090CC File Offset: 0x000074CC
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000090D3 File Offset: 0x000074D3
		public static uint XInputUpdateRate { get; internal set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000090DB File Offset: 0x000074DB
		// (set) Token: 0x0600020E RID: 526 RVA: 0x000090E2 File Offset: 0x000074E2
		public static uint XInputBufferSize { get; internal set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000090EA File Offset: 0x000074EA
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000090F1 File Offset: 0x000074F1
		public static bool EnableICade { get; internal set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000090F9 File Offset: 0x000074F9
		internal static VersionInfo UnityVersion
		{
			get
			{
				if (InputManager.unityVersion == null)
				{
					InputManager.unityVersion = new VersionInfo?(VersionInfo.UnityVersion());
				}
				return InputManager.unityVersion.Value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00009123 File Offset: 0x00007523
		internal static ulong CurrentTick
		{
			get
			{
				return InputManager.currentTick;
			}
		}

		// Token: 0x040001B9 RID: 441
		public static readonly VersionInfo Version = VersionInfo.InControlVersion();

		// Token: 0x040001C2 RID: 450
		private static List<InputDeviceManager> deviceManagers = new List<InputDeviceManager>();

		// Token: 0x040001C3 RID: 451
		private static Dictionary<Type, InputDeviceManager> deviceManagerTable = new Dictionary<Type, InputDeviceManager>();

		// Token: 0x040001C4 RID: 452
		private static InputDevice activeDevice = InputDevice.Null;

		// Token: 0x040001C5 RID: 453
		private static List<InputDevice> devices = new List<InputDevice>();

		// Token: 0x040001C6 RID: 454
		private static List<PlayerActionSet> playerActionSets = new List<PlayerActionSet>();

		// Token: 0x040001C7 RID: 455
		public static ReadOnlyCollection<InputDevice> Devices;

		// Token: 0x040001CC RID: 460
		private static float initialTime;

		// Token: 0x040001CD RID: 461
		private static float currentTime;

		// Token: 0x040001CE RID: 462
		private static float lastUpdateTime;

		// Token: 0x040001CF RID: 463
		private static ulong currentTick;

		// Token: 0x040001D0 RID: 464
		private static VersionInfo? unityVersion;
	}
}
