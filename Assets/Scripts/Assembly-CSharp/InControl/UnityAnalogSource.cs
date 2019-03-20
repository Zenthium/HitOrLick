using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000041 RID: 65
	public class UnityAnalogSource : InputControlSource
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000C028 File Offset: 0x0000A428
		public UnityAnalogSource()
		{
			UnityAnalogSource.SetupAnalogQueries();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C035 File Offset: 0x0000A435
		public UnityAnalogSource(int analogId)
		{
			this.AnalogId = analogId;
			UnityAnalogSource.SetupAnalogQueries();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C04C File Offset: 0x0000A44C
		public float GetValue(InputDevice inputDevice)
		{
			int joystickId = (inputDevice as UnityInputDevice).JoystickId;
			string analogKey = UnityAnalogSource.GetAnalogKey(joystickId, this.AnalogId);
			return Input.GetAxisRaw(analogKey);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C078 File Offset: 0x0000A478
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C088 File Offset: 0x0000A488
		private static void SetupAnalogQueries()
		{
			if (UnityAnalogSource.analogQueries == null)
			{
				UnityAnalogSource.analogQueries = new string[10, 20];
				for (int i = 1; i <= 10; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						UnityAnalogSource.analogQueries[i - 1, j] = string.Concat(new object[]
						{
							"joystick ",
							i,
							" analog ",
							j
						});
					}
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C10E File Offset: 0x0000A50E
		private static string GetAnalogKey(int joystickId, int analogId)
		{
			return UnityAnalogSource.analogQueries[joystickId - 1, analogId];
		}

		// Token: 0x04000289 RID: 649
		private static string[,] analogQueries;

		// Token: 0x0400028A RID: 650
		public int AnalogId;
	}
}
