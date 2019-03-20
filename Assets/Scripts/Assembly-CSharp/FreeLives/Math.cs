using System;
using UnityEngine;

namespace FreeLives
{
	// Token: 0x020000DA RID: 218
	public static class Math
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00024DF8 File Offset: 0x000231F8
		public static float GetAngle(float x, float y)
		{
			return Mathf.Atan2(y, x);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00024E01 File Offset: 0x00023201
		public static float GetAngle(Vector2 v)
		{
			return Mathf.Atan2(v.y, v.x);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00024E16 File Offset: 0x00023216
		public static float GetAngle(Vector3 v)
		{
			return Mathf.Atan2(v.y, v.x);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00024E2C File Offset: 0x0002322C
		public static Vector2 NearestPointOnLine(Vector2 start, Vector2 end, Vector2 point, bool clampToSegment)
		{
			float num = point.x - start.x;
			float num2 = point.y - start.y;
			float num3 = end.x - start.x;
			float num4 = end.y - start.y;
			float num5 = num3 * num3 + num4 * num4;
			float num6 = num * num3 + num2 * num4;
			float num7 = num6 / num5;
			if (clampToSegment)
			{
				if (num7 < 0f)
				{
					num7 = 0f;
				}
				else if (num7 > 1f)
				{
					num7 = 1f;
				}
			}
			return new Vector2(start.x + num3 * num7, start.y + num4 * num7);
		}
	}
}
