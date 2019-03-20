using System;
using System.IO;
using Microsoft.Win32;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000B8 RID: 184
	public static class Utility
	{
		// Token: 0x060003D7 RID: 983 RVA: 0x0002020C File Offset: 0x0001E60C
		public static void DrawCircleGizmo(Vector2 center, float radius)
		{
			Vector2 v = Utility.circleVertexList[0] * radius + center;
			int num = Utility.circleVertexList.Length;
			for (int i = 1; i < num; i++)
			{
				Gizmos.DrawLine(v, v = Utility.circleVertexList[i] * radius + center);
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0002027E File Offset: 0x0001E67E
		public static void DrawCircleGizmo(Vector2 center, float radius, Color color)
		{
			Gizmos.color = color;
			Utility.DrawCircleGizmo(center, radius);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00020290 File Offset: 0x0001E690
		public static void DrawOvalGizmo(Vector2 center, Vector2 size)
		{
			Vector2 b = size / 2f;
			Vector2 v = Vector2.Scale(Utility.circleVertexList[0], b) + center;
			int num = Utility.circleVertexList.Length;
			for (int i = 1; i < num; i++)
			{
				Gizmos.DrawLine(v, v = Vector2.Scale(Utility.circleVertexList[i], b) + center);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0002030E File Offset: 0x0001E70E
		public static void DrawOvalGizmo(Vector2 center, Vector2 size, Color color)
		{
			Gizmos.color = color;
			Utility.DrawOvalGizmo(center, size);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00020320 File Offset: 0x0001E720
		public static void DrawRectGizmo(Rect rect)
		{
			Vector3 vector = new Vector3(rect.xMin, rect.yMin);
			Vector3 vector2 = new Vector3(rect.xMax, rect.yMin);
			Vector3 vector3 = new Vector3(rect.xMax, rect.yMax);
			Vector3 vector4 = new Vector3(rect.xMin, rect.yMax);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0002039D File Offset: 0x0001E79D
		public static void DrawRectGizmo(Rect rect, Color color)
		{
			Gizmos.color = color;
			Utility.DrawRectGizmo(rect);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000203AC File Offset: 0x0001E7AC
		public static void DrawRectGizmo(Vector2 center, Vector2 size)
		{
			float num = size.x / 2f;
			float num2 = size.y / 2f;
			Vector3 vector = new Vector3(center.x - num, center.y - num2);
			Vector3 vector2 = new Vector3(center.x + num, center.y - num2);
			Vector3 vector3 = new Vector3(center.x + num, center.y + num2);
			Vector3 vector4 = new Vector3(center.x - num, center.y + num2);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00020459 File Offset: 0x0001E859
		public static void DrawRectGizmo(Vector2 center, Vector2 size, Color color)
		{
			Gizmos.color = color;
			Utility.DrawRectGizmo(center, size);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00020468 File Offset: 0x0001E868
		public static bool GameObjectIsCulledOnCurrentCamera(GameObject gameObject)
		{
			return (Camera.current.cullingMask & 1 << gameObject.layer) == 0;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00020484 File Offset: 0x0001E884
		public static Color MoveColorTowards(Color color0, Color color1, float maxDelta)
		{
			float r = Mathf.MoveTowards(color0.r, color1.r, maxDelta);
			float g = Mathf.MoveTowards(color0.g, color1.g, maxDelta);
			float b = Mathf.MoveTowards(color0.b, color1.b, maxDelta);
			float a = Mathf.MoveTowards(color0.a, color1.a, maxDelta);
			return new Color(r, g, b, a);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000204F0 File Offset: 0x0001E8F0
		public static float ApplyDeadZone(float value, float lowerDeadZone, float upperDeadZone)
		{
			if (value < 0f)
			{
				if (value > -lowerDeadZone)
				{
					return 0f;
				}
				if (value < -upperDeadZone)
				{
					return -1f;
				}
				return (value + lowerDeadZone) / (upperDeadZone - lowerDeadZone);
			}
			else
			{
				if (value < lowerDeadZone)
				{
					return 0f;
				}
				if (value > upperDeadZone)
				{
					return 1f;
				}
				return (value - lowerDeadZone) / (upperDeadZone - lowerDeadZone);
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00020550 File Offset: 0x0001E950
		public static Vector2 ApplyCircularDeadZone(Vector2 v, float lowerDeadZone, float upperDeadZone)
		{
			float d = Mathf.InverseLerp(lowerDeadZone, upperDeadZone, v.magnitude);
			return v.normalized * d;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00020579 File Offset: 0x0001E979
		public static Vector2 ApplyCircularDeadZone(float x, float y, float lowerDeadZone, float upperDeadZone)
		{
			return Utility.ApplyCircularDeadZone(new Vector2(x, y), lowerDeadZone, upperDeadZone);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0002058C File Offset: 0x0001E98C
		public static float ApplySmoothing(float thisValue, float lastValue, float deltaTime, float sensitivity)
		{
			if (Utility.Approximately(sensitivity, 1f))
			{
				return thisValue;
			}
			float maxDelta = deltaTime * sensitivity * 100f;
			if (Mathf.Sign(lastValue) != Mathf.Sign(thisValue))
			{
				lastValue = 0f;
			}
			return Mathf.MoveTowards(lastValue, thisValue, maxDelta);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x000205D5 File Offset: 0x0001E9D5
		public static float ApplySnapping(float value, float threshold)
		{
			if (value < -threshold)
			{
				return -1f;
			}
			if (value > threshold)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000205F7 File Offset: 0x0001E9F7
		internal static bool TargetIsButton(InputControlType target)
		{
			return (target >= InputControlType.Action1 && target <= InputControlType.Action4) || (target >= InputControlType.Button0 && target <= InputControlType.Button19);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0002061F File Offset: 0x0001EA1F
		internal static bool TargetIsStandard(InputControlType target)
		{
			return target >= InputControlType.LeftStickUp && target <= InputControlType.RightBumper;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00020634 File Offset: 0x0001EA34
		public static string ReadFromFile(string path)
		{
			StreamReader streamReader = new StreamReader(path);
			string result = streamReader.ReadToEnd();
			streamReader.Close();
			return result;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00020658 File Offset: 0x0001EA58
		public static void WriteToFile(string path, string data)
		{
			StreamWriter streamWriter = new StreamWriter(path);
			streamWriter.Write(data);
			streamWriter.Flush();
			streamWriter.Close();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0002067F File Offset: 0x0001EA7F
		public static float Abs(float value)
		{
			return (value >= 0f) ? value : (-value);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00020694 File Offset: 0x0001EA94
		public static bool Approximately(float value1, float value2)
		{
			float num = value1 - value2;
			return num >= -1E-07f && num <= 1E-07f;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000206BE File Offset: 0x0001EABE
		public static bool IsNotZero(float value)
		{
			return value < -1E-07f || value > 1E-07f;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000206D6 File Offset: 0x0001EAD6
		public static bool IsZero(float value)
		{
			return value >= -1E-07f && value <= 1E-07f;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000206F1 File Offset: 0x0001EAF1
		public static bool AbsoluteIsOverThreshold(float value, float threshold)
		{
			return value < -threshold || value > threshold;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00020702 File Offset: 0x0001EB02
		public static float NormalizeAngle(float angle)
		{
			while (angle < 0f)
			{
				angle += 360f;
			}
			while (angle > 360f)
			{
				angle -= 360f;
			}
			return angle;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00020738 File Offset: 0x0001EB38
		public static float VectorToAngle(Vector2 vector)
		{
			if (Utility.IsZero(vector.x) && Utility.IsZero(vector.y))
			{
				return 0f;
			}
			return Utility.NormalizeAngle(Mathf.Atan2(vector.x, vector.y) * 57.29578f);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0002078C File Offset: 0x0001EB8C
		public static float Min(float v0, float v1, float v2, float v3)
		{
			float num = (v0 < v1) ? v0 : v1;
			float num2 = (v2 < v3) ? v2 : v3;
			return (num < num2) ? num : num2;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000207C8 File Offset: 0x0001EBC8
		public static float Max(float v0, float v1, float v2, float v3)
		{
			float num = (v0 > v1) ? v0 : v1;
			float num2 = (v2 > v3) ? v2 : v3;
			return (num > num2) ? num : num2;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00020804 File Offset: 0x0001EC04
		internal static float ValueFromSides(float negativeSide, float positiveSide)
		{
			float num = Utility.Abs(negativeSide);
			float num2 = Utility.Abs(positiveSide);
			if (Utility.Approximately(num, num2))
			{
				return 0f;
			}
			return (num <= num2) ? num2 : (-num);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00020840 File Offset: 0x0001EC40
		internal static float ValueFromSides(float negativeSide, float positiveSide, bool invertSides)
		{
			if (invertSides)
			{
				return Utility.ValueFromSides(positiveSide, negativeSide);
			}
			return Utility.ValueFromSides(negativeSide, positiveSide);
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00020857 File Offset: 0x0001EC57
		internal static bool Is32Bit
		{
			get
			{
				return IntPtr.Size == 4;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00020861 File Offset: 0x0001EC61
		internal static bool Is64Bit
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0002086C File Offset: 0x0001EC6C
		public static string HKLM_GetString(string path, string key)
		{
			string result;
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path);
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					result = (string)registryKey.GetValue(key);
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000208C4 File Offset: 0x0001ECC4
		public static string GetWindowsVersion()
		{
			string text = Utility.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");
			if (text != null)
			{
				string text2 = Utility.HKLM_GetString("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CSDVersion");
				string str = (!Utility.Is32Bit) ? "64Bit" : "32Bit";
				return text + ((text2 == null) ? string.Empty : (" " + text2)) + " " + str;
			}
			return SystemInfo.operatingSystem;
		}

		// Token: 0x040002F0 RID: 752
		public const float Epsilon = 1E-07f;

		// Token: 0x040002F1 RID: 753
		private static Vector2[] circleVertexList = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(0.2588f, 0.9659f),
			new Vector2(0.5f, 0.866f),
			new Vector2(0.7071f, 0.7071f),
			new Vector2(0.866f, 0.5f),
			new Vector2(0.9659f, 0.2588f),
			new Vector2(1f, 0f),
			new Vector2(0.9659f, -0.2588f),
			new Vector2(0.866f, -0.5f),
			new Vector2(0.7071f, -0.7071f),
			new Vector2(0.5f, -0.866f),
			new Vector2(0.2588f, -0.9659f),
			new Vector2(0f, -1f),
			new Vector2(-0.2588f, -0.9659f),
			new Vector2(-0.5f, -0.866f),
			new Vector2(-0.7071f, -0.7071f),
			new Vector2(-0.866f, -0.5f),
			new Vector2(-0.9659f, -0.2588f),
			new Vector2(-1f, 0f),
			new Vector2(-0.9659f, 0.2588f),
			new Vector2(-0.866f, 0.5f),
			new Vector2(-0.7071f, 0.7071f),
			new Vector2(-0.5f, 0.866f),
			new Vector2(-0.2588f, 0.9659f),
			new Vector2(0f, 1f)
		};
	}
}
