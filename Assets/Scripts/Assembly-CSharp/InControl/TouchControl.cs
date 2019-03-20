using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000036 RID: 54
	public abstract class TouchControl : MonoBehaviour
	{
		// Token: 0x06000268 RID: 616
		public abstract void CreateControl();

		// Token: 0x06000269 RID: 617
		public abstract void DestroyControl();

		// Token: 0x0600026A RID: 618
		public abstract void ConfigureControl();

		// Token: 0x0600026B RID: 619
		public abstract void SubmitControlState(ulong updateTick, float deltaTime);

		// Token: 0x0600026C RID: 620
		public abstract void CommitControlState(ulong updateTick, float deltaTime);

		// Token: 0x0600026D RID: 621
		public abstract void TouchBegan(Touch touch);

		// Token: 0x0600026E RID: 622
		public abstract void TouchMoved(Touch touch);

		// Token: 0x0600026F RID: 623
		public abstract void TouchEnded(Touch touch);

		// Token: 0x06000270 RID: 624
		public abstract void DrawGizmos();

		// Token: 0x06000271 RID: 625 RVA: 0x00009183 File Offset: 0x00007583
		private void OnEnable()
		{
			TouchManager.OnSetup += this.Setup;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009196 File Offset: 0x00007596
		private void OnDisable()
		{
			this.DestroyControl();
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000091A4 File Offset: 0x000075A4
		private void Setup()
		{
			if (!base.enabled)
			{
				return;
			}
			this.CreateControl();
			this.ConfigureControl();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000091C0 File Offset: 0x000075C0
		protected Vector3 OffsetToWorldPosition(TouchControlAnchor anchor, Vector2 offset, TouchUnitType offsetUnitType, bool lockAspectRatio)
		{
			Vector3 b;
			if (offsetUnitType == TouchUnitType.Pixels)
			{
				b = TouchUtility.RoundVector(offset) * TouchManager.PixelToWorld;
			}
			else if (lockAspectRatio)
			{
				b = offset * TouchManager.PercentToWorld;
			}
			else
			{
				b = Vector3.Scale(offset, TouchManager.ViewSize);
			}
			return TouchManager.ViewToWorldPoint(TouchUtility.AnchorToViewPoint(anchor)) + b;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009230 File Offset: 0x00007630
		protected void SubmitButtonState(TouchControl.ButtonTarget target, bool state, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null)
			{
				control.UpdateWithState(state, updateTick, deltaTime);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000926C File Offset: 0x0000766C
		protected void CommitButton(TouchControl.ButtonTarget target)
		{
			if (TouchManager.Device == null || target == TouchControl.ButtonTarget.None)
			{
				return;
			}
			InputControl control = TouchManager.Device.GetControl((InputControlType)target);
			if (control != null)
			{
				control.Commit();
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000092A4 File Offset: 0x000076A4
		protected void SubmitAnalogValue(TouchControl.AnalogTarget target, Vector2 value, float lowerDeadZone, float upperDeadZone, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null)
			{
				return;
			}
			Vector2 value2 = Utility.ApplyCircularDeadZone(value, lowerDeadZone, upperDeadZone);
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateLeftStickWithValue(value2, updateTick, deltaTime);
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateRightStickWithValue(value2, updateTick, deltaTime);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00009300 File Offset: 0x00007700
		protected void CommitAnalog(TouchControl.AnalogTarget target)
		{
			if (TouchManager.Device == null)
			{
				return;
			}
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.CommitLeftStick();
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.CommitRightStick();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00009340 File Offset: 0x00007740
		protected void SubmitRawAnalogValue(TouchControl.AnalogTarget target, Vector2 rawValue, ulong updateTick, float deltaTime)
		{
			if (TouchManager.Device == null)
			{
				return;
			}
			if (target == TouchControl.AnalogTarget.LeftStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateLeftStickWithRawValue(rawValue, updateTick, deltaTime);
			}
			if (target == TouchControl.AnalogTarget.RightStick || target == TouchControl.AnalogTarget.Both)
			{
				TouchManager.Device.UpdateRightStickWithRawValue(rawValue, updateTick, deltaTime);
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00009390 File Offset: 0x00007790
		protected static Vector2 SnapTo(Vector2 vector, TouchControl.SnapAngles snapAngles)
		{
			if (snapAngles == TouchControl.SnapAngles.None)
			{
				return vector;
			}
			float snapAngle = 360f / (float)snapAngles;
			return TouchControl.SnapTo(vector, snapAngle);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x000093B8 File Offset: 0x000077B8
		protected static Vector2 SnapTo(Vector2 vector, float snapAngle)
		{
			float num = Vector2.Angle(vector, Vector2.up);
			if (num < snapAngle / 2f)
			{
				return Vector2.up * vector.magnitude;
			}
			if (num > 180f - snapAngle / 2f)
			{
				return -Vector2.up * vector.magnitude;
			}
			float num2 = Mathf.Round(num / snapAngle);
			float angle = num2 * snapAngle - num;
			Vector3 axis = Vector3.Cross(Vector2.up, vector);
			Quaternion rotation = Quaternion.AngleAxis(angle, axis);
			return rotation * vector;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000945C File Offset: 0x0000785C
		private void OnDrawGizmosSelected()
		{
			if (!base.enabled)
			{
				return;
			}
			if (TouchManager.ControlsShowGizmos != TouchManager.GizmoShowOption.WhenSelected)
			{
				return;
			}
			if (Utility.GameObjectIsCulledOnCurrentCamera(base.gameObject))
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.ConfigureControl();
			}
			this.DrawGizmos();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000094A8 File Offset: 0x000078A8
		private void OnDrawGizmos()
		{
			if (!base.enabled)
			{
				return;
			}
			if (TouchManager.ControlsShowGizmos == TouchManager.GizmoShowOption.UnlessPlaying)
			{
				if (Application.isPlaying)
				{
					return;
				}
			}
			else if (TouchManager.ControlsShowGizmos != TouchManager.GizmoShowOption.Always)
			{
				return;
			}
			if (Utility.GameObjectIsCulledOnCurrentCamera(base.gameObject))
			{
				return;
			}
			if (!Application.isPlaying)
			{
				this.ConfigureControl();
			}
			this.DrawGizmos();
		}

		// Token: 0x02000037 RID: 55
		public enum ButtonTarget
		{
			// Token: 0x04000223 RID: 547
			None,
			// Token: 0x04000224 RID: 548
			Action1 = 15,
			// Token: 0x04000225 RID: 549
			Action2,
			// Token: 0x04000226 RID: 550
			Action3,
			// Token: 0x04000227 RID: 551
			Action4,
			// Token: 0x04000228 RID: 552
			LeftTrigger,
			// Token: 0x04000229 RID: 553
			RightTrigger,
			// Token: 0x0400022A RID: 554
			LeftBumper,
			// Token: 0x0400022B RID: 555
			RightBumper,
			// Token: 0x0400022C RID: 556
			DPadDown = 12,
			// Token: 0x0400022D RID: 557
			DPadLeft,
			// Token: 0x0400022E RID: 558
			DPadRight,
			// Token: 0x0400022F RID: 559
			DPadUp = 11,
			// Token: 0x04000230 RID: 560
			Menu = 30,
			// Token: 0x04000231 RID: 561
			Button0 = 62,
			// Token: 0x04000232 RID: 562
			Button1,
			// Token: 0x04000233 RID: 563
			Button2,
			// Token: 0x04000234 RID: 564
			Button3,
			// Token: 0x04000235 RID: 565
			Button4,
			// Token: 0x04000236 RID: 566
			Button5,
			// Token: 0x04000237 RID: 567
			Button6,
			// Token: 0x04000238 RID: 568
			Button7,
			// Token: 0x04000239 RID: 569
			Button8,
			// Token: 0x0400023A RID: 570
			Button9,
			// Token: 0x0400023B RID: 571
			Button10,
			// Token: 0x0400023C RID: 572
			Button11,
			// Token: 0x0400023D RID: 573
			Button12,
			// Token: 0x0400023E RID: 574
			Button13,
			// Token: 0x0400023F RID: 575
			Button14,
			// Token: 0x04000240 RID: 576
			Button15,
			// Token: 0x04000241 RID: 577
			Button16,
			// Token: 0x04000242 RID: 578
			Button17,
			// Token: 0x04000243 RID: 579
			Button18,
			// Token: 0x04000244 RID: 580
			Button19
		}

		// Token: 0x02000038 RID: 56
		public enum AnalogTarget
		{
			// Token: 0x04000246 RID: 582
			None,
			// Token: 0x04000247 RID: 583
			LeftStick,
			// Token: 0x04000248 RID: 584
			RightStick,
			// Token: 0x04000249 RID: 585
			Both
		}

		// Token: 0x02000039 RID: 57
		public enum SnapAngles
		{
			// Token: 0x0400024B RID: 587
			None,
			// Token: 0x0400024C RID: 588
			Four = 4,
			// Token: 0x0400024D RID: 589
			Eight = 8,
			// Token: 0x0400024E RID: 590
			Sixteen = 16
		}
	}
}
