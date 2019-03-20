using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using XInputDotNetPure;

namespace InControl
{
	// Token: 0x020000BB RID: 187
	public class XInputDeviceManager : InputDeviceManager
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x0002144C File Offset: 0x0001F84C
		public XInputDeviceManager()
		{
			if (InputManager.XInputUpdateRate == 0u)
			{
				this.timeStep = Mathf.FloorToInt(Time.fixedDeltaTime * 1000f);
			}
			else
			{
				this.timeStep = Mathf.FloorToInt(1f / InputManager.XInputUpdateRate * 1000f);
			}
			this.bufferSize = (int)Math.Max(InputManager.XInputBufferSize, 1u);
			for (int i = 0; i < 4; i++)
			{
				this.gamePadState[i] = new RingBuffer<GamePadState>(this.bufferSize);
			}
			this.StartWorker();
			for (int j = 0; j < 4; j++)
			{
				this.devices.Add(new XInputDevice(j, this));
			}
			this.Update(0UL, 0f);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00021527 File Offset: 0x0001F927
		private void StartWorker()
		{
			if (this.thread == null)
			{
				this.thread = new Thread(new ThreadStart(this.Worker));
				this.thread.IsBackground = true;
				this.thread.Start();
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00021562 File Offset: 0x0001F962
		private void StopWorker()
		{
			if (this.thread != null)
			{
				this.thread.Abort();
				this.thread.Join();
				this.thread = null;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0002158C File Offset: 0x0001F98C
		private void Worker()
		{
			for (;;)
			{
				for (int i = 0; i < 4; i++)
				{
					this.gamePadState[i].Enqueue(GamePad.GetState((PlayerIndex)i));
				}
				Thread.Sleep(this.timeStep);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000215CD File Offset: 0x0001F9CD
		internal GamePadState GetState(int deviceIndex)
		{
			return this.gamePadState[deviceIndex].Dequeue();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000215DC File Offset: 0x0001F9DC
		public override void Update(ulong updateTick, float deltaTime)
		{
			for (int i = 0; i < 4; i++)
			{
				XInputDevice xinputDevice = this.devices[i] as XInputDevice;
				if (!xinputDevice.IsConnected)
				{
					xinputDevice.GetState();
				}
				if (xinputDevice.IsConnected != this.deviceConnected[i])
				{
					if (xinputDevice.IsConnected)
					{
						InputManager.AttachDevice(xinputDevice);
					}
					else
					{
						InputManager.DetachDevice(xinputDevice);
					}
					this.deviceConnected[i] = xinputDevice.IsConnected;
				}
			}
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0002165B File Offset: 0x0001FA5B
		public override void Destroy()
		{
			this.StopWorker();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00021664 File Offset: 0x0001FA64
		public static bool CheckPlatformSupport(ICollection<string> errors)
		{
			if (Application.platform != RuntimePlatform.WindowsPlayer && Application.platform != RuntimePlatform.WindowsEditor)
			{
				return false;
			}
			try
			{
				GamePad.GetState(PlayerIndex.One);
			}
			catch (DllNotFoundException ex)
			{
				if (errors != null)
				{
					errors.Add(ex.Message + ".dll could not be found or is missing a dependency.");
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000216CC File Offset: 0x0001FACC
		internal static void Enable()
		{
			List<string> list = new List<string>();
			if (XInputDeviceManager.CheckPlatformSupport(list))
			{
				InputManager.HideDevicesWithProfile(typeof(Xbox360WinProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWinProfile));
				InputManager.HideDevicesWithProfile(typeof(XboxOneWin10Profile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF310ModeXWinProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF510ModeXWinProfile));
				InputManager.HideDevicesWithProfile(typeof(LogitechF710ModeXWinProfile));
				InputManager.AddDeviceManager<XInputDeviceManager>();
			}
			else
			{
				foreach (string text in list)
				{
					Logger.LogError(text);
				}
			}
		}

		// Token: 0x040002FB RID: 763
		private bool[] deviceConnected = new bool[4];

		// Token: 0x040002FC RID: 764
		private const int maxDevices = 4;

		// Token: 0x040002FD RID: 765
		private RingBuffer<GamePadState>[] gamePadState = new RingBuffer<GamePadState>[4];

		// Token: 0x040002FE RID: 766
		private Thread thread;

		// Token: 0x040002FF RID: 767
		private int timeStep;

		// Token: 0x04000300 RID: 768
		private int bufferSize;
	}
}
