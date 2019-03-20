using System;
using UnityEngine;

namespace ProGrids
{
	// Token: 0x020000D5 RID: 213
	public static class PGExtensions
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x000241CC File Offset: 0x000225CC
		public static bool Contains(this Transform[] t_arr, Transform t)
		{
			for (int i = 0; i < t_arr.Length; i++)
			{
				if (t_arr[i] == t)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000241FE File Offset: 0x000225FE
		public static float Sum(this Vector3 v)
		{
			return v[0] + v[1] + v[2];
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0002421C File Offset: 0x0002261C
		public static bool InFrustum(this Camera cam, Vector3 point)
		{
			Vector3 vector = cam.WorldToViewportPoint(point);
			return vector.x >= 0f && vector.x <= 1f && vector.y >= 0f && vector.y <= 1f && vector.z >= 0f;
		}
	}
}
