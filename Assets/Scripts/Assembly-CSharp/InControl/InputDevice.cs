using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200002C RID: 44
	public class InputDevice
	{
		// Token: 0x06000153 RID: 339 RVA: 0x00006FE8 File Offset: 0x000053E8
		public InputDevice(string name)
		{
			this.Name = name;
			this.Meta = string.Empty;
			this.LastChangeTick = 0UL;
			this.Controls = new InputControl[83];
			this.LeftStickX = new OneAxisInputControl();
			this.LeftStickY = new OneAxisInputControl();
			this.LeftStick = new TwoAxisInputControl();
			this.RightStickX = new OneAxisInputControl();
			this.RightStickY = new OneAxisInputControl();
			this.RightStick = new TwoAxisInputControl();
			this.DPadX = new OneAxisInputControl();
			this.DPadY = new OneAxisInputControl();
			this.DPad = new TwoAxisInputControl();
			this.Command = this.AddControl(InputControlType.Command, "Command");
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000070A3 File Offset: 0x000054A3
		// (set) Token: 0x06000155 RID: 341 RVA: 0x000070AB File Offset: 0x000054AB
		public string Name { get; protected set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000070B4 File Offset: 0x000054B4
		// (set) Token: 0x06000157 RID: 343 RVA: 0x000070BC File Offset: 0x000054BC
		public string Meta { get; protected set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000070C5 File Offset: 0x000054C5
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000070CD File Offset: 0x000054CD
		public ulong LastChangeTick { get; protected set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000070D6 File Offset: 0x000054D6
		// (set) Token: 0x0600015B RID: 347 RVA: 0x000070DE File Offset: 0x000054DE
		public InputControl[] Controls { get; protected set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000070E7 File Offset: 0x000054E7
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000070EF File Offset: 0x000054EF
		public OneAxisInputControl LeftStickX { get; protected set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000070F8 File Offset: 0x000054F8
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007100 File Offset: 0x00005500
		public OneAxisInputControl LeftStickY { get; protected set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007109 File Offset: 0x00005509
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00007111 File Offset: 0x00005511
		public TwoAxisInputControl LeftStick { get; protected set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000711A File Offset: 0x0000551A
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00007122 File Offset: 0x00005522
		public OneAxisInputControl RightStickX { get; protected set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000712B File Offset: 0x0000552B
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00007133 File Offset: 0x00005533
		public OneAxisInputControl RightStickY { get; protected set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000713C File Offset: 0x0000553C
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00007144 File Offset: 0x00005544
		public TwoAxisInputControl RightStick { get; protected set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000714D File Offset: 0x0000554D
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00007155 File Offset: 0x00005555
		public OneAxisInputControl DPadX { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000715E File Offset: 0x0000555E
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00007166 File Offset: 0x00005566
		public OneAxisInputControl DPadY { get; protected set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000716F File Offset: 0x0000556F
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00007177 File Offset: 0x00005577
		public TwoAxisInputControl DPad { get; protected set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007180 File Offset: 0x00005580
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00007188 File Offset: 0x00005588
		public InputControl Command { get; protected set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00007191 File Offset: 0x00005591
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00007199 File Offset: 0x00005599
		public bool IsAttached { get; internal set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000071A2 File Offset: 0x000055A2
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000071AA File Offset: 0x000055AA
		internal bool RawSticks { get; set; }

		// Token: 0x06000174 RID: 372 RVA: 0x000071B3 File Offset: 0x000055B3
		public bool HasControl(InputControlType inputControlType)
		{
			return this.Controls[(int)inputControlType] != null;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000071C4 File Offset: 0x000055C4
		public InputControl GetControl(InputControlType inputControlType)
		{
			InputControl inputControl = this.Controls[(int)inputControlType];
			return inputControl ?? InputControl.Null;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000071E7 File Offset: 0x000055E7
		public static InputControlType GetInputControlTypeByName(string inputControlName)
		{
			return (InputControlType)Enum.Parse(typeof(InputControlType), inputControlName);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007200 File Offset: 0x00005600
		public InputControl GetControlByName(string inputControlName)
		{
			InputControlType inputControlTypeByName = InputDevice.GetInputControlTypeByName(inputControlName);
			return this.GetControl(inputControlTypeByName);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000721C File Offset: 0x0000561C
		public InputControl AddControl(InputControlType inputControlType, string handle)
		{
			InputControl inputControl = new InputControl(handle, inputControlType);
			this.Controls[(int)inputControlType] = inputControl;
			return inputControl;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000723C File Offset: 0x0000563C
		public InputControl AddControl(InputControlType inputControlType, string handle, float lowerDeadZone, float upperDeadZone)
		{
			InputControl inputControl = this.AddControl(inputControlType, handle);
			inputControl.LowerDeadZone = lowerDeadZone;
			inputControl.UpperDeadZone = upperDeadZone;
			return inputControl;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007264 File Offset: 0x00005664
		public void ClearInputState()
		{
			this.LeftStickX.ClearInputState();
			this.LeftStickY.ClearInputState();
			this.LeftStick.ClearInputState();
			this.RightStickX.ClearInputState();
			this.RightStickY.ClearInputState();
			this.RightStick.ClearInputState();
			this.DPadX.ClearInputState();
			this.DPadY.ClearInputState();
			this.DPad.ClearInputState();
			int num = this.Controls.Length;
			for (int i = 0; i < num; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null)
				{
					inputControl.ClearInputState();
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007304 File Offset: 0x00005704
		internal void UpdateWithState(InputControlType inputControlType, bool state, ulong updateTick, float deltaTime)
		{
			this.GetControl(inputControlType).UpdateWithState(state, updateTick, deltaTime);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007317 File Offset: 0x00005717
		internal void UpdateWithValue(InputControlType inputControlType, float value, ulong updateTick, float deltaTime)
		{
			this.GetControl(inputControlType).UpdateWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000732C File Offset: 0x0000572C
		internal void UpdateLeftStickWithValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.LeftStickLeft.UpdateWithValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.LeftStickRight.UpdateWithValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.LeftStickUp.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.LeftStickUp.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007408 File Offset: 0x00005808
		internal void UpdateLeftStickWithRawValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.LeftStickLeft.UpdateWithRawValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.LeftStickRight.UpdateWithRawValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.LeftStickUp.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.LeftStickUp.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.LeftStickDown.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000074E1 File Offset: 0x000058E1
		internal void CommitLeftStick()
		{
			this.LeftStickUp.Commit();
			this.LeftStickDown.Commit();
			this.LeftStickLeft.Commit();
			this.LeftStickRight.Commit();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007510 File Offset: 0x00005910
		internal void UpdateRightStickWithValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.RightStickLeft.UpdateWithValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.RightStickRight.UpdateWithValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.RightStickUp.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.RightStickUp.UpdateWithValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000075EC File Offset: 0x000059EC
		internal void UpdateRightStickWithRawValue(Vector2 value, ulong updateTick, float deltaTime)
		{
			this.RightStickLeft.UpdateWithRawValue(Mathf.Max(0f, -value.x), updateTick, deltaTime);
			this.RightStickRight.UpdateWithRawValue(Mathf.Max(0f, value.x), updateTick, deltaTime);
			if (InputManager.InvertYAxis)
			{
				this.RightStickUp.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
			}
			else
			{
				this.RightStickUp.UpdateWithRawValue(Mathf.Max(0f, value.y), updateTick, deltaTime);
				this.RightStickDown.UpdateWithRawValue(Mathf.Max(0f, -value.y), updateTick, deltaTime);
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000076C5 File Offset: 0x00005AC5
		internal void CommitRightStick()
		{
			this.RightStickUp.Commit();
			this.RightStickDown.Commit();
			this.RightStickLeft.Commit();
			this.RightStickRight.Commit();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000076F3 File Offset: 0x00005AF3
		public virtual void Update(ulong updateTick, float deltaTime)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000076F8 File Offset: 0x00005AF8
		private bool AnyCommandControlIsPressed()
		{
			for (int i = 24; i <= 34; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null && inputControl.IsPressed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007738 File Offset: 0x00005B38
		internal void ProcessLeftStick(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.LeftStickLeft.NextRawValue, this.LeftStickRight.NextRawValue);
			float y = Utility.ValueFromSides(this.LeftStickDown.NextRawValue, this.LeftStickUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.LeftStickLeft.LowerDeadZone, this.LeftStickRight.LowerDeadZone, this.LeftStickUp.LowerDeadZone, this.LeftStickDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.LeftStickLeft.UpperDeadZone, this.LeftStickRight.UpperDeadZone, this.LeftStickUp.UpperDeadZone, this.LeftStickDown.UpperDeadZone);
				vector = Utility.ApplyCircularDeadZone(x, y, lowerDeadZone, upperDeadZone);
			}
			this.LeftStick.Raw = true;
			this.LeftStick.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.LeftStickX.Raw = true;
			this.LeftStickX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.LeftStickY.Raw = true;
			this.LeftStickY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.LeftStickLeft.SetValue(this.LeftStick.Left.Value, updateTick);
			this.LeftStickRight.SetValue(this.LeftStick.Right.Value, updateTick);
			this.LeftStickUp.SetValue(this.LeftStick.Up.Value, updateTick);
			this.LeftStickDown.SetValue(this.LeftStick.Down.Value, updateTick);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000078E4 File Offset: 0x00005CE4
		internal void ProcessRightStick(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.RightStickLeft.NextRawValue, this.RightStickRight.NextRawValue);
			float y = Utility.ValueFromSides(this.RightStickDown.NextRawValue, this.RightStickUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector;
			if (this.RawSticks)
			{
				vector = new Vector2(x, y);
			}
			else
			{
				float lowerDeadZone = Utility.Max(this.RightStickLeft.LowerDeadZone, this.RightStickRight.LowerDeadZone, this.RightStickUp.LowerDeadZone, this.RightStickDown.LowerDeadZone);
				float upperDeadZone = Utility.Min(this.RightStickLeft.UpperDeadZone, this.RightStickRight.UpperDeadZone, this.RightStickUp.UpperDeadZone, this.RightStickDown.UpperDeadZone);
				vector = Utility.ApplyCircularDeadZone(x, y, lowerDeadZone, upperDeadZone);
			}
			this.RightStick.Raw = true;
			this.RightStick.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.RightStickX.Raw = true;
			this.RightStickX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.RightStickY.Raw = true;
			this.RightStickY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.RightStickLeft.SetValue(this.RightStick.Left.Value, updateTick);
			this.RightStickRight.SetValue(this.RightStick.Right.Value, updateTick);
			this.RightStickUp.SetValue(this.RightStick.Up.Value, updateTick);
			this.RightStickDown.SetValue(this.RightStick.Down.Value, updateTick);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007A90 File Offset: 0x00005E90
		internal void ProcessDPad(ulong updateTick, float deltaTime)
		{
			float lowerDeadZone = Utility.Max(this.DPadLeft.LowerDeadZone, this.DPadRight.LowerDeadZone, this.DPadUp.LowerDeadZone, this.DPadDown.LowerDeadZone);
			float upperDeadZone = Utility.Min(this.DPadLeft.UpperDeadZone, this.DPadRight.UpperDeadZone, this.DPadUp.UpperDeadZone, this.DPadDown.UpperDeadZone);
			float x = Utility.ValueFromSides(this.DPadLeft.NextRawValue, this.DPadRight.NextRawValue);
			float y = Utility.ValueFromSides(this.DPadDown.NextRawValue, this.DPadUp.NextRawValue, InputManager.InvertYAxis);
			Vector2 vector = Utility.ApplyCircularDeadZone(x, y, lowerDeadZone, upperDeadZone);
			this.DPad.Raw = true;
			this.DPad.UpdateWithAxes(vector.x, vector.y, updateTick, deltaTime);
			this.DPadX.Raw = true;
			this.DPadX.CommitWithValue(vector.x, updateTick, deltaTime);
			this.DPadY.Raw = true;
			this.DPadY.CommitWithValue(vector.y, updateTick, deltaTime);
			this.DPadLeft.SetValue(this.DPad.Left.Value, updateTick);
			this.DPadRight.SetValue(this.DPad.Right.Value, updateTick);
			this.DPadUp.SetValue(this.DPad.Up.Value, updateTick);
			this.DPadDown.SetValue(this.DPad.Down.Value, updateTick);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007C20 File Offset: 0x00006020
		public void Commit(ulong updateTick, float deltaTime)
		{
			this.ProcessLeftStick(updateTick, deltaTime);
			this.ProcessRightStick(updateTick, deltaTime);
			this.ProcessDPad(updateTick, deltaTime);
			int num = this.Controls.Length;
			for (int i = 0; i < num; i++)
			{
				InputControl inputControl = this.Controls[i];
				if (inputControl != null)
				{
					inputControl.Commit();
					if (inputControl.HasChanged)
					{
						this.LastChangeTick = updateTick;
					}
				}
			}
			if (this.IsKnown)
			{
				this.Command.CommitWithState(this.AnyCommandControlIsPressed(), updateTick, deltaTime);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007CA5 File Offset: 0x000060A5
		public bool LastChangedAfter(InputDevice otherDevice)
		{
			return this.LastChangeTick > otherDevice.LastChangeTick;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007CB5 File Offset: 0x000060B5
		internal void RequestActivation()
		{
			this.LastChangeTick = InputManager.CurrentTick;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007CC2 File Offset: 0x000060C2
		public virtual void Vibrate(float leftMotor, float rightMotor)
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007CC4 File Offset: 0x000060C4
		public void Vibrate(float intensity)
		{
			this.Vibrate(intensity, intensity);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007CCE File Offset: 0x000060CE
		public void StopVibration()
		{
			this.Vibrate(0f);
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00007CDB File Offset: 0x000060DB
		public virtual bool IsSupportedOnThisPlatform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00007CDE File Offset: 0x000060DE
		public virtual bool IsKnown
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007CE1 File Offset: 0x000060E1
		public bool IsUnknown
		{
			get
			{
				return !this.IsKnown;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00007CEC File Offset: 0x000060EC
		public bool MenuWasPressed
		{
			get
			{
				return this.GetControl(InputControlType.Command).WasPressed;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007CFC File Offset: 0x000060FC
		public InputControl AnyButton
		{
			get
			{
				int length = this.Controls.GetLength(0);
				for (int i = 0; i < length; i++)
				{
					InputControl inputControl = this.Controls[i];
					if (inputControl != null && inputControl.IsButton && inputControl.IsPressed)
					{
						return inputControl;
					}
				}
				return InputControl.Null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007D54 File Offset: 0x00006154
		public InputControl LeftStickUp
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickUp);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007D5D File Offset: 0x0000615D
		public InputControl LeftStickDown
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickDown);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007D66 File Offset: 0x00006166
		public InputControl LeftStickLeft
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickLeft);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00007D6F File Offset: 0x0000616F
		public InputControl LeftStickRight
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickRight);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007D78 File Offset: 0x00006178
		public InputControl RightStickUp
		{
			get
			{
				return this.GetControl(InputControlType.RightStickUp);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007D81 File Offset: 0x00006181
		public InputControl RightStickDown
		{
			get
			{
				return this.GetControl(InputControlType.RightStickDown);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00007D8A File Offset: 0x0000618A
		public InputControl RightStickLeft
		{
			get
			{
				return this.GetControl(InputControlType.RightStickLeft);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00007D93 File Offset: 0x00006193
		public InputControl RightStickRight
		{
			get
			{
				return this.GetControl(InputControlType.RightStickRight);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007D9D File Offset: 0x0000619D
		public InputControl DPadUp
		{
			get
			{
				return this.GetControl(InputControlType.DPadUp);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007DA7 File Offset: 0x000061A7
		public InputControl DPadDown
		{
			get
			{
				return this.GetControl(InputControlType.DPadDown);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00007DB1 File Offset: 0x000061B1
		public InputControl DPadLeft
		{
			get
			{
				return this.GetControl(InputControlType.DPadLeft);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00007DBB File Offset: 0x000061BB
		public InputControl DPadRight
		{
			get
			{
				return this.GetControl(InputControlType.DPadRight);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00007DC5 File Offset: 0x000061C5
		public InputControl Action1
		{
			get
			{
				return this.GetControl(InputControlType.Action1);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00007DCF File Offset: 0x000061CF
		public InputControl Action2
		{
			get
			{
				return this.GetControl(InputControlType.Action2);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00007DD9 File Offset: 0x000061D9
		public InputControl Action3
		{
			get
			{
				return this.GetControl(InputControlType.Action3);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007DE3 File Offset: 0x000061E3
		public InputControl Action4
		{
			get
			{
				return this.GetControl(InputControlType.Action4);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007DED File Offset: 0x000061ED
		public InputControl LeftTrigger
		{
			get
			{
				return this.GetControl(InputControlType.LeftTrigger);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00007DF7 File Offset: 0x000061F7
		public InputControl RightTrigger
		{
			get
			{
				return this.GetControl(InputControlType.RightTrigger);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007E01 File Offset: 0x00006201
		public InputControl LeftBumper
		{
			get
			{
				return this.GetControl(InputControlType.LeftBumper);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007E0B File Offset: 0x0000620B
		public InputControl RightBumper
		{
			get
			{
				return this.GetControl(InputControlType.RightBumper);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007E15 File Offset: 0x00006215
		public InputControl LeftStickButton
		{
			get
			{
				return this.GetControl(InputControlType.LeftStickButton);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007E1E File Offset: 0x0000621E
		public InputControl RightStickButton
		{
			get
			{
				return this.GetControl(InputControlType.RightStickButton);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007E28 File Offset: 0x00006228
		public TwoAxisInputControl Direction
		{
			get
			{
				return (this.DPad.UpdateTick <= this.LeftStick.UpdateTick) ? this.LeftStick : this.DPad;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007E56 File Offset: 0x00006256
		public static implicit operator bool(InputDevice device)
		{
			return device != null;
		}

		// Token: 0x0400018A RID: 394
		public static readonly InputDevice Null = new InputDevice("None");

		// Token: 0x0400018B RID: 395
		internal int SortOrder = int.MaxValue;
	}
}
