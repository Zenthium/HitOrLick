using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InControl
{
	// Token: 0x020000C9 RID: 201
	public class TestInputManager : MonoBehaviour
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00021CA8 File Offset: 0x000200A8
		private void OnEnable()
		{
			this.isPaused = false;
			Time.timeScale = 1f;
			Logger.OnLogMessage += delegate(LogMessage logMessage)
			{
				this.logMessages.Add(logMessage);
			};
			InputManager.OnDeviceAttached += delegate(InputDevice inputDevice)
			{
				Debug.Log("Attached: " + inputDevice.Name);
			};
			InputManager.OnDeviceDetached += delegate(InputDevice inputDevice)
			{
				Debug.Log("Detached: " + inputDevice.Name);
			};
			InputManager.OnActiveDeviceChanged += delegate(InputDevice inputDevice)
			{
				Debug.Log("Active device changed to: " + inputDevice.Name);
			};
			InputManager.OnUpdate += this.HandleInputUpdate;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00021D4E File Offset: 0x0002014E
		private void HandleInputUpdate(ulong updateTick, float deltaTime)
		{
			this.CheckForPauseButton();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00021D56 File Offset: 0x00020156
		private void Start()
		{
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00021D58 File Offset: 0x00020158
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				SceneManager.LoadScene("TestInputManager");
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00021D70 File Offset: 0x00020170
		private void CheckForPauseButton()
		{
			if (Input.GetKeyDown(KeyCode.P) || InputManager.MenuWasPressed)
			{
				Time.timeScale = ((!this.isPaused) ? 0f : 1f);
				this.isPaused = !this.isPaused;
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00021DC1 File Offset: 0x000201C1
		private void SetColor(Color color)
		{
			this.style.normal.textColor = color;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00021DD4 File Offset: 0x000201D4
		private void OnGUI()
		{
			int num = 300;
			int num2 = 10;
			int num3 = 10;
			int num4 = 15;
			GUI.skin.font = this.font;
			this.SetColor(Color.white);
			string text = "Devices:";
			text = text + " (Platform: " + InputManager.Platform + ")";
			text = text + " " + InputManager.ActiveDevice.Direction.Vector;
			if (this.isPaused)
			{
				this.SetColor(Color.red);
				text = "+++ PAUSED +++";
			}
			GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text, this.style);
			this.SetColor(Color.white);
			foreach (InputDevice inputDevice in InputManager.Devices)
			{
				bool flag = InputManager.ActiveDevice == inputDevice;
				Color color = (!flag) ? Color.white : Color.yellow;
				num3 = 35;
				this.SetColor(color);
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), inputDevice.Name, this.style);
				num3 += num4;
				if (inputDevice.IsUnknown)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), inputDevice.Meta, this.style);
					num3 += num4;
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "SortOrder: " + inputDevice.SortOrder, this.style);
				num3 += num4;
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "LastChangeTick: " + inputDevice.LastChangeTick, this.style);
				num3 += num4;
				foreach (InputControl inputControl in inputDevice.Controls)
				{
					if (inputControl != null)
					{
						string arg;
						if (inputDevice.IsKnown)
						{
							arg = string.Format("{0} ({1})", inputControl.Target, inputControl.Handle);
						}
						else
						{
							arg = inputControl.Handle;
						}
						this.SetColor((!inputControl.State) ? color : Color.green);
						string text2 = string.Format("{0} {1}", arg, (!inputControl.State) ? string.Empty : ("= " + inputControl.Value));
						GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
						num3 += num4;
					}
				}
				num3 += num4;
				color = ((!flag) ? Color.white : new Color(1f, 0.7f, 0.2f));
				if (inputDevice.IsKnown)
				{
					OneAxisInputControl oneAxisInputControl = inputDevice.LeftStickX;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					string text3 = string.Format("{0} {1}", "Left Stick X", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.LeftStickY;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Left Stick Y", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					this.SetColor((!inputDevice.LeftStick.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Left Stick A", (!inputDevice.LeftStick.State) ? string.Empty : ("= " + inputDevice.LeftStick.Angle));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.RightStickX;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Right Stick X", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.RightStickY;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Right Stick Y", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					this.SetColor((!inputDevice.RightStick.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "Right Stick A", (!inputDevice.RightStick.State) ? string.Empty : ("= " + inputDevice.RightStick.Angle));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.DPadX;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "DPad X", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
					oneAxisInputControl = inputDevice.DPadY;
					this.SetColor((!oneAxisInputControl.State) ? color : Color.green);
					text3 = string.Format("{0} {1}", "DPad Y", (!oneAxisInputControl.State) ? string.Empty : ("= " + oneAxisInputControl.Value));
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text3, this.style);
					num3 += num4;
				}
				this.SetColor(Color.cyan);
				InputControl anyButton = inputDevice.AnyButton;
				if (anyButton)
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), "AnyButton = " + anyButton.Handle, this.style);
				}
				num2 += 200;
			}
			Color[] array = new Color[]
			{
				Color.gray,
				Color.yellow,
				Color.white
			};
			this.SetColor(Color.white);
			num2 = 10;
			num3 = Screen.height - (10 + num4);
			for (int j = this.logMessages.Count - 1; j >= 0; j--)
			{
				LogMessage logMessage = this.logMessages[j];
				this.SetColor(array[(int)logMessage.type]);
				foreach (string text4 in logMessage.text.Split(new char[]
				{
					'\n'
				}))
				{
					GUI.Label(new Rect((float)num2, (float)num3, (float)Screen.width, (float)(num3 + 10)), text4, this.style);
					num3 -= num4;
				}
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00022694 File Offset: 0x00020A94
		private void DrawUnityInputDebugger()
		{
			int num = 300;
			int num2 = Screen.width / 2;
			int num3 = 10;
			int num4 = 20;
			this.SetColor(Color.white);
			string[] joystickNames = Input.GetJoystickNames();
			int num5 = joystickNames.Length;
			for (int i = 0; i < num5; i++)
			{
				string text = joystickNames[i];
				int num6 = i + 1;
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), string.Concat(new object[]
				{
					"Joystick ",
					num6,
					": \"",
					text,
					"\""
				}), this.style);
				num3 += num4;
				string text2 = "Buttons: ";
				for (int j = 0; j < 20; j++)
				{
					string name = string.Concat(new object[]
					{
						"joystick ",
						num6,
						" button ",
						j
					});
					bool key = Input.GetKey(name);
					if (key)
					{
						string text3 = text2;
						text2 = string.Concat(new object[]
						{
							text3,
							"B",
							j,
							"  "
						});
					}
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text2, this.style);
				num3 += num4;
				string text4 = "Analogs: ";
				for (int k = 0; k < 20; k++)
				{
					string axisName = string.Concat(new object[]
					{
						"joystick ",
						num6,
						" analog ",
						k
					});
					float axisRaw = Input.GetAxisRaw(axisName);
					if (Utility.AbsoluteIsOverThreshold(axisRaw, 0.2f))
					{
						string text3 = text4;
						text4 = string.Concat(new object[]
						{
							text3,
							"A",
							k,
							": ",
							axisRaw.ToString("0.00"),
							"  "
						});
					}
				}
				GUI.Label(new Rect((float)num2, (float)num3, (float)(num2 + num), (float)(num3 + 10)), text4, this.style);
				num3 += num4;
				num3 += 25;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000228D0 File Offset: 0x00020CD0
		private void OnDrawGizmos()
		{
			InputDevice activeDevice = InputManager.ActiveDevice;
			Vector2 a = new Vector2(activeDevice.LeftStickX, activeDevice.LeftStickY);
			Gizmos.color = Color.blue;
			Vector2 vector = new Vector2(-3f, -1f);
			Vector2 v = vector + a * 2f;
			Gizmos.DrawSphere(vector, 0.1f);
			Gizmos.DrawLine(vector, v);
			Gizmos.DrawSphere(v, 1f);
			Gizmos.color = Color.red;
			Vector2 vector2 = new Vector2(3f, -1f);
			Vector2 v2 = vector2 + activeDevice.RightStick.Vector * 2f;
			Gizmos.DrawSphere(vector2, 0.1f);
			Gizmos.DrawLine(vector2, v2);
			Gizmos.DrawSphere(v2, 1f);
		}

		// Token: 0x0400033A RID: 826
		public Font font;

		// Token: 0x0400033B RID: 827
		private GUIStyle style = new GUIStyle();

		// Token: 0x0400033C RID: 828
		private List<LogMessage> logMessages = new List<LogMessage>();

		// Token: 0x0400033D RID: 829
		private bool isPaused;
	}
}
