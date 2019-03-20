using System;
using XInputDotNetPure;

namespace InControl
{
	// Token: 0x020000BA RID: 186
	public class XInputDevice : InputDevice
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x00020F84 File Offset: 0x0001F384
		public XInputDevice(int deviceIndex, XInputDeviceManager owner) : base("XInput Controller")
		{
			this.owner = owner;
			this.DeviceIndex = deviceIndex;
			this.SortOrder = deviceIndex;
			base.Meta = "XInput Device #" + deviceIndex;
			base.AddControl(InputControlType.LeftStickLeft, "Left Stick Left", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftStickRight, "Left Stick Right", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftStickUp, "Left Stick Up", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftStickDown, "Left Stick Down", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickLeft, "Right Stick Left", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickRight, "Right Stick Right", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickUp, "Right Stick Up", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightStickDown, "Right Stick Down", 0.2f, 0.9f);
			base.AddControl(InputControlType.LeftTrigger, "Left Trigger", 0.2f, 0.9f);
			base.AddControl(InputControlType.RightTrigger, "Right Trigger", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadUp, "DPad Up", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadDown, "DPad Down", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadLeft, "DPad Left", 0.2f, 0.9f);
			base.AddControl(InputControlType.DPadRight, "DPad Right", 0.2f, 0.9f);
			base.AddControl(InputControlType.Action1, "A");
			base.AddControl(InputControlType.Action2, "B");
			base.AddControl(InputControlType.Action3, "X");
			base.AddControl(InputControlType.Action4, "Y");
			base.AddControl(InputControlType.LeftBumper, "Left Bumper");
			base.AddControl(InputControlType.RightBumper, "Right Bumper");
			base.AddControl(InputControlType.LeftStickButton, "Left Stick Button");
			base.AddControl(InputControlType.RightStickButton, "Right Stick Button");
			base.AddControl(InputControlType.Start, "Start");
			base.AddControl(InputControlType.Back, "Back");
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0002119B File Offset: 0x0001F59B
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x000211A3 File Offset: 0x0001F5A3
		public int DeviceIndex { get; private set; }

		// Token: 0x0600040B RID: 1035 RVA: 0x000211AC File Offset: 0x0001F5AC
		public override void Update(ulong updateTick, float deltaTime)
		{
			this.GetState();
			base.UpdateLeftStickWithValue(this.state.ThumbSticks.Left.Vector, updateTick, deltaTime);
			base.UpdateRightStickWithValue(this.state.ThumbSticks.Right.Vector, updateTick, deltaTime);
			base.UpdateWithValue(InputControlType.LeftTrigger, this.state.Triggers.Left, updateTick, deltaTime);
			base.UpdateWithValue(InputControlType.RightTrigger, this.state.Triggers.Right, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadUp, this.state.DPad.Up == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadDown, this.state.DPad.Down == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadLeft, this.state.DPad.Left == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.DPadRight, this.state.DPad.Right == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action1, this.state.Buttons.A == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action2, this.state.Buttons.B == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action3, this.state.Buttons.X == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Action4, this.state.Buttons.Y == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.LeftBumper, this.state.Buttons.LeftShoulder == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.RightBumper, this.state.Buttons.RightShoulder == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.LeftStickButton, this.state.Buttons.LeftStick == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.RightStickButton, this.state.Buttons.RightStick == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Start, this.state.Buttons.Start == ButtonState.Pressed, updateTick, deltaTime);
			base.UpdateWithState(InputControlType.Back, this.state.Buttons.Back == ButtonState.Pressed, updateTick, deltaTime);
			base.Commit(updateTick, deltaTime);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00021416 File Offset: 0x0001F816
		public override void Vibrate(float leftMotor, float rightMotor)
		{
			GamePad.SetVibration((PlayerIndex)this.DeviceIndex, leftMotor, rightMotor);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00021425 File Offset: 0x0001F825
		internal void GetState()
		{
			this.state = this.owner.GetState(this.DeviceIndex);
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0002143E File Offset: 0x0001F83E
		public bool IsConnected
		{
			get
			{
				return this.state.IsConnected;
			}
		}

		// Token: 0x040002F6 RID: 758
		private const float LowerDeadZone = 0.2f;

		// Token: 0x040002F7 RID: 759
		private const float UpperDeadZone = 0.9f;

		// Token: 0x040002F8 RID: 760
		private XInputDeviceManager owner;

		// Token: 0x040002F9 RID: 761
		private GamePadState state;
	}
}
