using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000043 RID: 67
	public class UnityGyroAxisSource : InputControlSource
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000C21E File Offset: 0x0000A61E
		public UnityGyroAxisSource()
		{
			UnityGyroAxisSource.Calibrate();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C22B File Offset: 0x0000A62B
		public UnityGyroAxisSource(UnityGyroAxisSource.GyroAxis axis)
		{
			this.Axis = (int)axis;
			UnityGyroAxisSource.Calibrate();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C240 File Offset: 0x0000A640
		public float GetValue(InputDevice inputDevice)
		{
			return UnityGyroAxisSource.GetAxis()[this.Axis];
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C260 File Offset: 0x0000A660
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C26E File Offset: 0x0000A66E
		private static Quaternion GetAttitude()
		{
			return Quaternion.Inverse(UnityGyroAxisSource.zeroAttitude) * Input.gyro.attitude;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C28C File Offset: 0x0000A68C
		private static Vector3 GetAxis()
		{
			Vector3 vector = UnityGyroAxisSource.GetAttitude() * Vector3.forward;
			float x = UnityGyroAxisSource.ApplyDeadZone(Mathf.Clamp(vector.x, -1f, 1f));
			float y = UnityGyroAxisSource.ApplyDeadZone(Mathf.Clamp(vector.y, -1f, 1f));
			return new Vector3(x, y);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C2E8 File Offset: 0x0000A6E8
		private static float ApplyDeadZone(float value)
		{
			return Mathf.InverseLerp(0.05f, 1f, Utility.Abs(value)) * Mathf.Sign(value);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C306 File Offset: 0x0000A706
		public static void Calibrate()
		{
			UnityGyroAxisSource.zeroAttitude = Input.gyro.attitude;
		}

		// Token: 0x0400028D RID: 653
		private static Quaternion zeroAttitude;

		// Token: 0x0400028E RID: 654
		public int Axis;

		// Token: 0x02000044 RID: 68
		public enum GyroAxis
		{
			// Token: 0x04000290 RID: 656
			X,
			// Token: 0x04000291 RID: 657
			Y
		}
	}
}
