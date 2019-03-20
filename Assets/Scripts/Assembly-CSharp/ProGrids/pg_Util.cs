using System;
using System.Linq;
using UnityEngine;

namespace ProGrids
{
	// Token: 0x020000D4 RID: 212
	public static class pg_Util
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00023B98 File Offset: 0x00021F98
		public static Color ColorWithString(string value)
		{
			string valid = "01234567890.,";
			value = new string((from c in value
			where valid.Contains(c)
			select c).ToArray<char>());
			string[] array = value.Split(new char[]
			{
				','
			});
			if (array.Length < 4)
			{
				return new Color(1f, 0f, 1f, 1f);
			}
			return new Color(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00023C30 File Offset: 0x00022030
		private static Vector3 VectorToMask(Vector3 vec)
		{
			return new Vector3((Mathf.Abs(vec.x) <= Mathf.Epsilon) ? 0f : 1f, (Mathf.Abs(vec.y) <= Mathf.Epsilon) ? 0f : 1f, (Mathf.Abs(vec.z) <= Mathf.Epsilon) ? 0f : 1f);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00023CB4 File Offset: 0x000220B4
		private static Axis MaskToAxis(Vector3 vec)
		{
			Axis axis = Axis.None;
			if (Mathf.Abs(vec.x) > 0f)
			{
				axis |= Axis.X;
			}
			if (Mathf.Abs(vec.y) > 0f)
			{
				axis |= Axis.Y;
			}
			if (Mathf.Abs(vec.z) > 0f)
			{
				axis |= Axis.Z;
			}
			return axis;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00023D14 File Offset: 0x00022114
		private static Axis BestAxis(Vector3 vec)
		{
			float num = Mathf.Abs(vec.x);
			float num2 = Mathf.Abs(vec.y);
			float num3 = Mathf.Abs(vec.z);
			return (num <= num2 || num <= num3) ? ((num2 <= num || num2 <= num3) ? Axis.Z : Axis.Y) : Axis.X;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00023D74 File Offset: 0x00022174
		public static Axis CalcDragAxis(Vector3 movement, Camera cam)
		{
			Vector3 vector = pg_Util.VectorToMask(movement);
			if (vector.x + vector.y + vector.z == 2f)
			{
				return pg_Util.MaskToAxis(Vector3.one - vector);
			}
			switch (pg_Util.MaskToAxis(vector))
			{
			case Axis.X:
				if (Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.up)) < Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.forward)))
				{
					return Axis.Z;
				}
				return Axis.Y;
			case Axis.Y:
				if (Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.right)) < Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.forward)))
				{
					return Axis.Z;
				}
				return Axis.X;
			case Axis.Z:
				if (Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.right)) < Mathf.Abs(Vector3.Dot(cam.transform.forward, Vector3.up)))
				{
					return Axis.Y;
				}
				return Axis.X;
			}
			return Axis.None;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00023E98 File Offset: 0x00022298
		public static float ValueFromMask(Vector3 val, Vector3 mask)
		{
			if (Mathf.Abs(mask.x) > 0.0001f)
			{
				return val.x;
			}
			if (Mathf.Abs(mask.y) > 0.0001f)
			{
				return val.y;
			}
			return val.z;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00023EE8 File Offset: 0x000222E8
		public static Vector3 SnapValue(Vector3 val, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3(pg_Util.Snap(x, snapValue), pg_Util.Snap(y, snapValue), pg_Util.Snap(z, snapValue));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00023F28 File Offset: 0x00022328
		public static Vector3 SnapValue(Vector3 val, Vector3 mask, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3((Mathf.Abs(mask.x) >= 0.0001f) ? pg_Util.Snap(x, snapValue) : x, (Mathf.Abs(mask.y) >= 0.0001f) ? pg_Util.Snap(y, snapValue) : y, (Mathf.Abs(mask.z) >= 0.0001f) ? pg_Util.Snap(z, snapValue) : z);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00023FBC File Offset: 0x000223BC
		public static Vector3 SnapToCeil(Vector3 val, Vector3 mask, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3((Mathf.Abs(mask.x) >= 0.0001f) ? pg_Util.SnapToCeil(x, snapValue) : x, (Mathf.Abs(mask.y) >= 0.0001f) ? pg_Util.SnapToCeil(y, snapValue) : y, (Mathf.Abs(mask.z) >= 0.0001f) ? pg_Util.SnapToCeil(z, snapValue) : z);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00024050 File Offset: 0x00022450
		public static Vector3 SnapToFloor(Vector3 val, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3(pg_Util.SnapToFloor(x, snapValue), pg_Util.SnapToFloor(y, snapValue), pg_Util.SnapToFloor(z, snapValue));
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00024090 File Offset: 0x00022490
		public static Vector3 SnapToFloor(Vector3 val, Vector3 mask, float snapValue)
		{
			float x = val.x;
			float y = val.y;
			float z = val.z;
			return new Vector3((Mathf.Abs(mask.x) >= 0.0001f) ? pg_Util.SnapToFloor(x, snapValue) : x, (Mathf.Abs(mask.y) >= 0.0001f) ? pg_Util.SnapToFloor(y, snapValue) : y, (Mathf.Abs(mask.z) >= 0.0001f) ? pg_Util.SnapToFloor(z, snapValue) : z);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00024123 File Offset: 0x00022523
		public static float Snap(float val, float round)
		{
			return round * Mathf.Round(val / round);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0002412F File Offset: 0x0002252F
		public static float SnapToFloor(float val, float snapValue)
		{
			return snapValue * Mathf.Floor(val / snapValue);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0002413B File Offset: 0x0002253B
		public static float SnapToCeil(float val, float snapValue)
		{
			return snapValue * Mathf.Ceil(val / snapValue);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00024148 File Offset: 0x00022548
		public static Vector3 CeilFloor(Vector3 v)
		{
			v.x = (float)((v.x >= 0f) ? 1 : -1);
			v.y = (float)((v.y >= 0f) ? 1 : -1);
			v.z = (float)((v.z >= 0f) ? 1 : -1);
			return v;
		}

		// Token: 0x040003A1 RID: 929
		private const float EPSILON = 0.0001f;
	}
}
