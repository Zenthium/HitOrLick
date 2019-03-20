using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200003B RID: 59
	[ExecuteInEditMode]
	public class TouchManager : SingletonMonoBehavior<TouchManager>
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000A8E8 File Offset: 0x00008CE8
		protected TouchManager()
		{
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600027F RID: 639 RVA: 0x0000A908 File Offset: 0x00008D08
		// (remove) Token: 0x06000280 RID: 640 RVA: 0x0000A93C File Offset: 0x00008D3C
		public static event Action OnSetup;

		// Token: 0x06000281 RID: 641 RVA: 0x0000A970 File Offset: 0x00008D70
		private void OnEnable()
		{
			if (!base.SetupSingleton())
			{
				return;
			}
			this.touchControls = base.GetComponentsInChildren<TouchControl>(true);
			if (Application.isPlaying)
			{
				InputManager.OnSetup += this.Setup;
				InputManager.OnUpdateDevices += this.UpdateDevice;
				InputManager.OnCommitDevices += this.CommitDevice;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A9D4 File Offset: 0x00008DD4
		private void OnDisable()
		{
			if (Application.isPlaying)
			{
				InputManager.OnSetup -= this.Setup;
				InputManager.OnUpdateDevices -= this.UpdateDevice;
				InputManager.OnCommitDevices -= this.CommitDevice;
			}
			this.Reset();
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000AA24 File Offset: 0x00008E24
		private void Setup()
		{
			this.UpdateScreenSize(new Vector2((float)Screen.width, (float)Screen.height));
			this.CreateDevice();
			this.CreateTouches();
			if (TouchManager.OnSetup != null)
			{
				TouchManager.OnSetup();
				TouchManager.OnSetup = null;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000AA63 File Offset: 0x00008E63
		private void Reset()
		{
			this.device = null;
			this.mouseTouch = null;
			this.cachedTouches = null;
			this.activeTouches = null;
			this.readOnlyActiveTouches = null;
			this.touchControls = null;
			TouchManager.OnSetup = null;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000AA95 File Offset: 0x00008E95
		private void Start()
		{
			base.StartCoroutine(this.Ready());
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000AAA4 File Offset: 0x00008EA4
		private IEnumerator Ready()
		{
			yield return new WaitForEndOfFrame();
			this.isReady = true;
			this.UpdateScreenSize(new Vector2((float)Screen.width, (float)Screen.height));
			yield return null;
			yield break;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000AAC0 File Offset: 0x00008EC0
		private void Update()
		{
			if (!this.isReady)
			{
				return;
			}
			Vector2 vector = new Vector2((float)Screen.width, (float)Screen.height);
			if (this.screenSize != vector)
			{
				this.UpdateScreenSize(vector);
			}
			if (TouchManager.OnSetup != null)
			{
				TouchManager.OnSetup();
				TouchManager.OnSetup = null;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000AB20 File Offset: 0x00008F20
		private void CreateDevice()
		{
			this.device = new InputDevice("TouchDevice");
			this.device.RawSticks = true;
			this.device.AddControl(InputControlType.LeftStickLeft, "LeftStickLeft");
			this.device.AddControl(InputControlType.LeftStickRight, "LeftStickRight");
			this.device.AddControl(InputControlType.LeftStickUp, "LeftStickUp");
			this.device.AddControl(InputControlType.LeftStickDown, "LeftStickDown");
			this.device.AddControl(InputControlType.RightStickLeft, "RightStickLeft");
			this.device.AddControl(InputControlType.RightStickRight, "RightStickRight");
			this.device.AddControl(InputControlType.RightStickUp, "RightStickUp");
			this.device.AddControl(InputControlType.RightStickDown, "RightStickDown");
			this.device.AddControl(InputControlType.LeftTrigger, "LeftTrigger");
			this.device.AddControl(InputControlType.RightTrigger, "RightTrigger");
			this.device.AddControl(InputControlType.DPadUp, "DPadUp");
			this.device.AddControl(InputControlType.DPadDown, "DPadDown");
			this.device.AddControl(InputControlType.DPadLeft, "DPadLeft");
			this.device.AddControl(InputControlType.DPadRight, "DPadRight");
			this.device.AddControl(InputControlType.Action1, "Action1");
			this.device.AddControl(InputControlType.Action2, "Action2");
			this.device.AddControl(InputControlType.Action3, "Action3");
			this.device.AddControl(InputControlType.Action4, "Action4");
			this.device.AddControl(InputControlType.LeftBumper, "LeftBumper");
			this.device.AddControl(InputControlType.RightBumper, "RightBumper");
			this.device.AddControl(InputControlType.Menu, "Menu");
			for (InputControlType inputControlType = InputControlType.Button0; inputControlType <= InputControlType.Button19; inputControlType++)
			{
				this.device.AddControl(inputControlType, inputControlType.ToString());
			}
			InputManager.AttachDevice(this.device);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000AD0A File Offset: 0x0000910A
		private void UpdateDevice(ulong updateTick, float deltaTime)
		{
			this.UpdateTouches(updateTick, deltaTime);
			this.SubmitControlStates(updateTick, deltaTime);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000AD1C File Offset: 0x0000911C
		private void CommitDevice(ulong updateTick, float deltaTime)
		{
			this.CommitControlStates(updateTick, deltaTime);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000AD28 File Offset: 0x00009128
		private void SubmitControlStates(ulong updateTick, float deltaTime)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.SubmitControlState(updateTick, deltaTime);
				}
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000AD7C File Offset: 0x0000917C
		private void CommitControlStates(ulong updateTick, float deltaTime)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.CommitControlState(updateTick, deltaTime);
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000ADD0 File Offset: 0x000091D0
		private void UpdateScreenSize(Vector2 currentScreenSize)
		{
			this.screenSize = currentScreenSize;
			this.halfScreenSize = this.screenSize / 2f;
			this.viewSize = this.ConvertViewToWorldPoint(Vector2.one) * 0.02f;
			this.percentToWorld = Mathf.Min(this.viewSize.x, this.viewSize.y);
			this.halfPercentToWorld = this.percentToWorld / 2f;
			if (this.touchCamera != null)
			{
				this.halfPixelToWorld = this.touchCamera.orthographicSize / this.screenSize.y;
				this.pixelToWorld = this.halfPixelToWorld * 2f;
			}
			if (this.touchControls != null)
			{
				int num = this.touchControls.Length;
				for (int i = 0; i < num; i++)
				{
					this.touchControls[i].ConfigureControl();
				}
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AEBC File Offset: 0x000092BC
		private void CreateTouches()
		{
			this.cachedTouches = new Touch[16];
			for (int i = 0; i < 16; i++)
			{
				this.cachedTouches[i] = new Touch(i);
			}
			this.mouseTouch = this.cachedTouches[15];
			this.activeTouches = new List<Touch>(16);
			this.readOnlyActiveTouches = new ReadOnlyCollection<Touch>(this.activeTouches);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AF24 File Offset: 0x00009324
		private void UpdateTouches(ulong updateTick, float deltaTime)
		{
			this.activeTouches.Clear();
			if (this.mouseTouch.SetWithMouseData(updateTick, deltaTime))
			{
				this.activeTouches.Add(this.mouseTouch);
			}
			for (int i = 0; i < Input.touchCount; i++)
			{
				UnityEngine.Touch touch = Input.GetTouch(i);
				Touch touch2 = this.cachedTouches[touch.fingerId];
				touch2.SetWithTouchData(touch, updateTick, deltaTime);
				this.activeTouches.Add(touch2);
			}
			for (int j = 0; j < 16; j++)
			{
				Touch touch3 = this.cachedTouches[j];
				if (touch3.phase != TouchPhase.Ended && touch3.updateTick != updateTick)
				{
					touch3.phase = TouchPhase.Ended;
					this.activeTouches.Add(touch3);
				}
			}
			this.InvokeTouchEvents();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AFF4 File Offset: 0x000093F4
		private void SendTouchBegan(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchBegan(touch);
				}
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000B048 File Offset: 0x00009448
		private void SendTouchMoved(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchMoved(touch);
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000B09C File Offset: 0x0000949C
		private void SendTouchEnded(Touch touch)
		{
			int num = this.touchControls.Length;
			for (int i = 0; i < num; i++)
			{
				TouchControl touchControl = this.touchControls[i];
				if (touchControl.enabled && touchControl.gameObject.activeInHierarchy)
				{
					touchControl.TouchEnded(touch);
				}
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000B0F0 File Offset: 0x000094F0
		private void InvokeTouchEvents()
		{
			int count = this.activeTouches.Count;
			if (this.enableControlsOnTouch && count > 0 && !this.controlsEnabled)
			{
				TouchManager.Device.RequestActivation();
				this.controlsEnabled = true;
			}
			for (int i = 0; i < count; i++)
			{
				Touch touch = this.activeTouches[i];
				switch (touch.phase)
				{
				case TouchPhase.Began:
					this.SendTouchBegan(touch);
					break;
				case TouchPhase.Moved:
					this.SendTouchMoved(touch);
					break;
				case TouchPhase.Ended:
					this.SendTouchEnded(touch);
					break;
				case TouchPhase.Canceled:
					this.SendTouchEnded(touch);
					break;
				}
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000B1AC File Offset: 0x000095AC
		private bool TouchCameraIsValid()
		{
			return !(this.touchCamera == null) && !Utility.IsZero(this.touchCamera.orthographicSize) && (!Utility.IsZero(this.touchCamera.rect.width) || !Utility.IsZero(this.touchCamera.rect.height)) && (!Utility.IsZero(this.touchCamera.pixelRect.width) || !Utility.IsZero(this.touchCamera.pixelRect.height));
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000B25C File Offset: 0x0000965C
		private Vector3 ConvertScreenToWorldPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ScreenToWorldPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000B2B4 File Offset: 0x000096B4
		private Vector3 ConvertViewToWorldPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ViewportToWorldPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000B30C File Offset: 0x0000970C
		private Vector3 ConvertScreenToViewPoint(Vector2 point)
		{
			if (this.TouchCameraIsValid())
			{
				return this.touchCamera.ScreenToViewportPoint(new Vector3(point.x, point.y, -this.touchCamera.transform.position.z));
			}
			return Vector3.zero;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000B361 File Offset: 0x00009761
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000B36C File Offset: 0x0000976C
		public bool controlsEnabled
		{
			get
			{
				return this._controlsEnabled;
			}
			set
			{
				if (this._controlsEnabled != value)
				{
					int num = this.touchControls.Length;
					for (int i = 0; i < num; i++)
					{
						this.touchControls[i].enabled = value;
					}
					this._controlsEnabled = value;
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000B3B5 File Offset: 0x000097B5
		public static ReadOnlyCollection<Touch> Touches
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.readOnlyActiveTouches;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000B3C1 File Offset: 0x000097C1
		public static int TouchCount
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.activeTouches.Count;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B3D2 File Offset: 0x000097D2
		public static Touch GetTouch(int touchIndex)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.activeTouches[touchIndex];
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B3E4 File Offset: 0x000097E4
		public static Touch GetTouchByFingerId(int fingerId)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.cachedTouches[fingerId];
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B3F2 File Offset: 0x000097F2
		public static Vector3 ScreenToWorldPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertScreenToWorldPoint(point);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B3FF File Offset: 0x000097FF
		public static Vector3 ViewToWorldPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertViewToWorldPoint(point);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B40C File Offset: 0x0000980C
		public static Vector3 ScreenToViewPoint(Vector2 point)
		{
			return SingletonMonoBehavior<TouchManager>.Instance.ConvertScreenToViewPoint(point);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B419 File Offset: 0x00009819
		public static float ConvertToWorld(float value, TouchUnitType unitType)
		{
			return value * ((unitType != TouchUnitType.Pixels) ? TouchManager.PercentToWorld : TouchManager.PixelToWorld);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000B434 File Offset: 0x00009834
		public static Rect PercentToWorldRect(Rect rect)
		{
			return new Rect((rect.xMin - 50f) * TouchManager.ViewSize.x, (rect.yMin - 50f) * TouchManager.ViewSize.y, rect.width * TouchManager.ViewSize.x, rect.height * TouchManager.ViewSize.y);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B4A8 File Offset: 0x000098A8
		public static Rect PixelToWorldRect(Rect rect)
		{
			return new Rect(Mathf.Round(rect.xMin - TouchManager.HalfScreenSize.x) * TouchManager.PixelToWorld, Mathf.Round(rect.yMin - TouchManager.HalfScreenSize.y) * TouchManager.PixelToWorld, Mathf.Round(rect.width) * TouchManager.PixelToWorld, Mathf.Round(rect.height) * TouchManager.PixelToWorld);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B51E File Offset: 0x0000991E
		public static Rect ConvertToWorld(Rect rect, TouchUnitType unitType)
		{
			return (unitType != TouchUnitType.Pixels) ? TouchManager.PercentToWorldRect(rect) : TouchManager.PixelToWorldRect(rect);
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B538 File Offset: 0x00009938
		public static Camera Camera
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.touchCamera;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000B544 File Offset: 0x00009944
		public static InputDevice Device
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.device;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B550 File Offset: 0x00009950
		public static Vector3 ViewSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.viewSize;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B55C File Offset: 0x0000995C
		public static float PercentToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.percentToWorld;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B568 File Offset: 0x00009968
		public static float HalfPercentToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfPercentToWorld;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B574 File Offset: 0x00009974
		public static float PixelToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.pixelToWorld;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B580 File Offset: 0x00009980
		public static float HalfPixelToWorld
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfPixelToWorld;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B58C File Offset: 0x0000998C
		public static Vector2 ScreenSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.screenSize;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B598 File Offset: 0x00009998
		public static Vector2 HalfScreenSize
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.halfScreenSize;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000B5A4 File Offset: 0x000099A4
		public static TouchManager.GizmoShowOption ControlsShowGizmos
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.controlsShowGizmos;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B5B0 File Offset: 0x000099B0
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B5BC File Offset: 0x000099BC
		public static bool ControlsEnabled
		{
			get
			{
				return SingletonMonoBehavior<TouchManager>.Instance.controlsEnabled;
			}
			set
			{
				SingletonMonoBehavior<TouchManager>.Instance.controlsEnabled = value;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B5C9 File Offset: 0x000099C9
		public static implicit operator bool(TouchManager instance)
		{
			return instance != null;
		}

		// Token: 0x04000259 RID: 601
		private const int MaxTouches = 16;

		// Token: 0x0400025A RID: 602
		[Space(10f)]
		public Camera touchCamera;

		// Token: 0x0400025B RID: 603
		public TouchManager.GizmoShowOption controlsShowGizmos = TouchManager.GizmoShowOption.Always;

		// Token: 0x0400025C RID: 604
		[HideInInspector]
		public bool enableControlsOnTouch;

		// Token: 0x0400025D RID: 605
		[SerializeField]
		[HideInInspector]
		private bool _controlsEnabled = true;

		// Token: 0x0400025E RID: 606
		[HideInInspector]
		public int controlsLayer = 5;

		// Token: 0x04000260 RID: 608
		private InputDevice device;

		// Token: 0x04000261 RID: 609
		private Vector3 viewSize;

		// Token: 0x04000262 RID: 610
		private Vector2 screenSize;

		// Token: 0x04000263 RID: 611
		private Vector2 halfScreenSize;

		// Token: 0x04000264 RID: 612
		private float percentToWorld;

		// Token: 0x04000265 RID: 613
		private float halfPercentToWorld;

		// Token: 0x04000266 RID: 614
		private float pixelToWorld;

		// Token: 0x04000267 RID: 615
		private float halfPixelToWorld;

		// Token: 0x04000268 RID: 616
		private TouchControl[] touchControls;

		// Token: 0x04000269 RID: 617
		private Touch[] cachedTouches;

		// Token: 0x0400026A RID: 618
		private List<Touch> activeTouches;

		// Token: 0x0400026B RID: 619
		private ReadOnlyCollection<Touch> readOnlyActiveTouches;

		// Token: 0x0400026C RID: 620
		private Vector2 lastMousePosition;

		// Token: 0x0400026D RID: 621
		private bool isReady;

		// Token: 0x0400026E RID: 622
		private Touch mouseTouch;

		// Token: 0x0200003C RID: 60
		public enum GizmoShowOption
		{
			// Token: 0x04000270 RID: 624
			Never,
			// Token: 0x04000271 RID: 625
			WhenSelected,
			// Token: 0x04000272 RID: 626
			UnlessPlaying,
			// Token: 0x04000273 RID: 627
			Always
		}
	}
}
