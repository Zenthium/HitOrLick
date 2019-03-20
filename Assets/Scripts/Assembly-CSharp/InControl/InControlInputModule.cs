using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InControl
{
	// Token: 0x0200002E RID: 46
	[AddComponentMenu("Event/InControl Input Module")]
	public class InControlInputModule : StandaloneInputModule
	{
		// Token: 0x060001AF RID: 431 RVA: 0x00007E88 File Offset: 0x00006288
		protected InControlInputModule()
		{
			this.direction = new TwoAxisInputControl();
			this.direction.StateThreshold = this.analogMoveThreshold;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00007EF6 File Offset: 0x000062F6
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00007EFE File Offset: 0x000062FE
		public PlayerAction SubmitAction { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00007F07 File Offset: 0x00006307
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00007F0F File Offset: 0x0000630F
		public PlayerAction CancelAction { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00007F18 File Offset: 0x00006318
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00007F20 File Offset: 0x00006320
		public PlayerTwoAxisAction MoveAction { get; set; }

		// Token: 0x060001B6 RID: 438 RVA: 0x00007F29 File Offset: 0x00006329
		public override void UpdateModule()
		{
			this.lastMousePosition = this.thisMousePosition;
			this.thisMousePosition = Input.mousePosition;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007F42 File Offset: 0x00006342
		public override bool IsModuleSupported()
		{
			return this.allowMobileDevice || Input.mousePresent;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007F58 File Offset: 0x00006358
		public override bool ShouldActivateModule()
		{
			if (!base.enabled || !base.gameObject.activeInHierarchy)
			{
				return false;
			}
			this.UpdateInputState();
			bool flag = false;
			flag |= this.SubmitWasPressed;
			flag |= this.CancelWasPressed;
			flag |= this.VectorWasPressed;
			if (this.allowMouseInput)
			{
				flag |= this.MouseHasMoved;
				flag |= this.MouseButtonIsPressed;
			}
			return flag;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007FC4 File Offset: 0x000063C4
		public override void ActivateModule()
		{
			base.ActivateModule();
			this.thisMousePosition = Input.mousePosition;
			this.lastMousePosition = Input.mousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008024 File Offset: 0x00006424
		public override void Process()
		{
			bool flag = base.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag = this.SendVectorEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendButtonEventToSelectedObject();
				}
			}
			if (this.allowMouseInput)
			{
				base.ProcessMouseEvent();
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008074 File Offset: 0x00006474
		private bool SendButtonEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			if (this.SubmitWasPressed)
			{
				ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
			}
			else if (this.SubmitWasReleased)
			{
			}
			if (this.CancelWasPressed)
			{
				ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
			}
			return baseEventData.used;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000080FC File Offset: 0x000064FC
		private bool SendVectorEventToSelectedObject()
		{
			if (!this.VectorWasPressed)
			{
				return false;
			}
			AxisEventData axisEventData = this.GetAxisEventData(this.thisVectorState.x, this.thisVectorState.y, 0.5f);
			if (axisEventData.moveDir != MoveDirection.None)
			{
				if (base.eventSystem.currentSelectedGameObject == null)
				{
					base.eventSystem.SetSelectedGameObject(base.eventSystem.firstSelectedGameObject, this.GetBaseEventData());
				}
				else
				{
					ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				}
				this.SetVectorRepeatTimer();
			}
			return axisEventData.used;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000081A0 File Offset: 0x000065A0
		protected override void ProcessMove(PointerEventData pointerEvent)
		{
			GameObject pointerEnter = pointerEvent.pointerEnter;
			base.ProcessMove(pointerEvent);
			if (this.focusOnMouseHover && pointerEnter != pointerEvent.pointerEnter)
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(pointerEvent.pointerEnter);
				base.eventSystem.SetSelectedGameObject(eventHandler, pointerEvent);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000081F0 File Offset: 0x000065F0
		private void Update()
		{
			this.direction.Filter(this.Device.Direction, Time.deltaTime);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008210 File Offset: 0x00006610
		private void UpdateInputState()
		{
			this.lastVectorState = this.thisVectorState;
			this.thisVectorState = Vector2.zero;
			TwoAxisInputControl twoAxisInputControl = this.MoveAction ?? this.direction;
			if (Utility.AbsoluteIsOverThreshold(twoAxisInputControl.X, this.analogMoveThreshold))
			{
				this.thisVectorState.x = Mathf.Sign(twoAxisInputControl.X);
			}
			if (Utility.AbsoluteIsOverThreshold(twoAxisInputControl.Y, this.analogMoveThreshold))
			{
				this.thisVectorState.y = Mathf.Sign(twoAxisInputControl.Y);
			}
			if (this.VectorIsReleased)
			{
				this.nextMoveRepeatTime = 0f;
			}
			if (this.VectorIsPressed)
			{
				if (this.lastVectorState == Vector2.zero)
				{
					if (Time.realtimeSinceStartup > this.lastVectorPressedTime + 0.1f)
					{
						this.nextMoveRepeatTime = Time.realtimeSinceStartup + this.moveRepeatFirstDuration;
					}
					else
					{
						this.nextMoveRepeatTime = Time.realtimeSinceStartup + this.moveRepeatDelayDuration;
					}
				}
				this.lastVectorPressedTime = Time.realtimeSinceStartup;
			}
			this.lastSubmitState = this.thisSubmitState;
			this.thisSubmitState = ((this.SubmitAction != null) ? this.SubmitAction.IsPressed : this.SubmitButton.IsPressed);
			this.lastCancelState = this.thisCancelState;
			this.thisCancelState = ((this.CancelAction != null) ? this.CancelAction.IsPressed : this.CancelButton.IsPressed);
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00008399 File Offset: 0x00006799
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00008390 File Offset: 0x00006790
		private InputDevice Device
		{
			get
			{
				return this.inputDevice ?? InputManager.ActiveDevice;
			}
			set
			{
				this.inputDevice = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000083AD File Offset: 0x000067AD
		private InputControl SubmitButton
		{
			get
			{
				return this.Device.GetControl((InputControlType)this.submitButton);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000083C0 File Offset: 0x000067C0
		private InputControl CancelButton
		{
			get
			{
				return this.Device.GetControl((InputControlType)this.cancelButton);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000083D3 File Offset: 0x000067D3
		private void SetVectorRepeatTimer()
		{
			this.nextMoveRepeatTime = Mathf.Max(this.nextMoveRepeatTime, Time.realtimeSinceStartup + this.moveRepeatDelayDuration);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000083F2 File Offset: 0x000067F2
		private bool VectorIsPressed
		{
			get
			{
				return this.thisVectorState != Vector2.zero;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008404 File Offset: 0x00006804
		private bool VectorIsReleased
		{
			get
			{
				return this.thisVectorState == Vector2.zero;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00008416 File Offset: 0x00006816
		private bool VectorHasChanged
		{
			get
			{
				return this.thisVectorState != this.lastVectorState;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008429 File Offset: 0x00006829
		private bool VectorWasPressed
		{
			get
			{
				return (this.VectorIsPressed && Time.realtimeSinceStartup > this.nextMoveRepeatTime) || (this.VectorIsPressed && this.lastVectorState == Vector2.zero);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00008466 File Offset: 0x00006866
		private bool SubmitWasPressed
		{
			get
			{
				return this.thisSubmitState && this.thisSubmitState != this.lastSubmitState;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008487 File Offset: 0x00006887
		private bool SubmitWasReleased
		{
			get
			{
				return !this.thisSubmitState && this.thisSubmitState != this.lastSubmitState;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000084A8 File Offset: 0x000068A8
		private bool CancelWasPressed
		{
			get
			{
				return this.thisCancelState && this.thisCancelState != this.lastCancelState;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CC RID: 460 RVA: 0x000084CC File Offset: 0x000068CC
		private bool MouseHasMoved
		{
			get
			{
				return (this.thisMousePosition - this.lastMousePosition).sqrMagnitude > 0f;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001CD RID: 461 RVA: 0x000084F9 File Offset: 0x000068F9
		private bool MouseButtonIsPressed
		{
			get
			{
				return Input.GetMouseButtonDown(0);
			}
		}

		// Token: 0x0400019D RID: 413
		public new InControlInputModule.Button submitButton = InControlInputModule.Button.Action1;

		// Token: 0x0400019E RID: 414
		public new InControlInputModule.Button cancelButton = InControlInputModule.Button.Action2;

		// Token: 0x0400019F RID: 415
		[Range(0.1f, 0.9f)]
		public float analogMoveThreshold = 0.5f;

		// Token: 0x040001A0 RID: 416
		public float moveRepeatFirstDuration = 0.8f;

		// Token: 0x040001A1 RID: 417
		public float moveRepeatDelayDuration = 0.1f;

		// Token: 0x040001A2 RID: 418
		public bool allowMobileDevice = true;

		// Token: 0x040001A3 RID: 419
		public bool allowMouseInput = true;

		// Token: 0x040001A4 RID: 420
		public bool focusOnMouseHover;

		// Token: 0x040001A5 RID: 421
		private InputDevice inputDevice;

		// Token: 0x040001A6 RID: 422
		private Vector3 thisMousePosition;

		// Token: 0x040001A7 RID: 423
		private Vector3 lastMousePosition;

		// Token: 0x040001A8 RID: 424
		private Vector2 thisVectorState;

		// Token: 0x040001A9 RID: 425
		private Vector2 lastVectorState;

		// Token: 0x040001AA RID: 426
		private bool thisSubmitState;

		// Token: 0x040001AB RID: 427
		private bool lastSubmitState;

		// Token: 0x040001AC RID: 428
		private bool thisCancelState;

		// Token: 0x040001AD RID: 429
		private bool lastCancelState;

		// Token: 0x040001AE RID: 430
		private float nextMoveRepeatTime;

		// Token: 0x040001AF RID: 431
		private float lastVectorPressedTime;

		// Token: 0x040001B0 RID: 432
		private TwoAxisInputControl direction;

		// Token: 0x0200002F RID: 47
		public enum Button
		{
			// Token: 0x040001B5 RID: 437
			Action1 = 15,
			// Token: 0x040001B6 RID: 438
			Action2,
			// Token: 0x040001B7 RID: 439
			Action3,
			// Token: 0x040001B8 RID: 440
			Action4
		}
	}
}
