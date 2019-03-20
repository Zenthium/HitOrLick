using System;
using UnityEngine;

namespace ProGrids
{
	// Token: 0x020000D3 RID: 211
	public static class pg_Enum
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x000239FC File Offset: 0x00021DFC
		public static Vector3 InverseAxisMask(Vector3 v, Axis axis)
		{
			switch (axis)
			{
			case Axis.X:
			case Axis.NegX:
				return Vector3.Scale(v, new Vector3(0f, 1f, 1f));
			case Axis.Y:
				break;
			default:
				if (axis != Axis.NegY)
				{
					if (axis != Axis.NegZ)
					{
						return v;
					}
					goto IL_73;
				}
				break;
			case Axis.Z:
				goto IL_73;
			}
			return Vector3.Scale(v, new Vector3(1f, 0f, 1f));
			IL_73:
			return Vector3.Scale(v, new Vector3(1f, 1f, 0f));
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00023A98 File Offset: 0x00021E98
		public static Vector3 AxisMask(Vector3 v, Axis axis)
		{
			switch (axis)
			{
			case Axis.X:
			case Axis.NegX:
				return Vector3.Scale(v, new Vector3(1f, 0f, 0f));
			case Axis.Y:
				break;
			default:
				if (axis != Axis.NegY)
				{
					if (axis != Axis.NegZ)
					{
						return v;
					}
					goto IL_73;
				}
				break;
			case Axis.Z:
				goto IL_73;
			}
			return Vector3.Scale(v, new Vector3(0f, 1f, 0f));
			IL_73:
			return Vector3.Scale(v, new Vector3(0f, 0f, 1f));
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00023B34 File Offset: 0x00021F34
		public static float SnapUnitValue(SnapUnit su)
		{
			switch (su)
			{
			case SnapUnit.Meter:
				return 1f;
			case SnapUnit.Centimeter:
				return 0.01f;
			case SnapUnit.Millimeter:
				return 0.001f;
			case SnapUnit.Inch:
				return 0.0253999867f;
			case SnapUnit.Foot:
				return 0.3048f;
			case SnapUnit.Yard:
				return 1.09361f;
			case SnapUnit.Parsec:
				return 5f;
			default:
				return 1f;
			}
		}
	}
}
