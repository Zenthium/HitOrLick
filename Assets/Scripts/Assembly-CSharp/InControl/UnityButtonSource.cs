using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000042 RID: 66
	public class UnityButtonSource : InputControlSource
	{
		// Token: 0x060002DF RID: 735 RVA: 0x0000C11E File Offset: 0x0000A51E
		public UnityButtonSource()
		{
			UnityButtonSource.SetupButtonQueries();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000C12B File Offset: 0x0000A52B
		public UnityButtonSource(int buttonId)
		{
			this.ButtonId = buttonId;
			UnityButtonSource.SetupButtonQueries();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C13F File Offset: 0x0000A53F
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C15C File Offset: 0x0000A55C
		public bool GetState(InputDevice inputDevice)
		{
			int joystickId = (inputDevice as UnityInputDevice).JoystickId;
			string buttonKey = UnityButtonSource.GetButtonKey(joystickId, this.ButtonId);
			return Input.GetKey(buttonKey);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C188 File Offset: 0x0000A588
		private static void SetupButtonQueries()
		{
			if (UnityButtonSource.buttonQueries == null)
			{
				UnityButtonSource.buttonQueries = new string[10, 20];
				for (int i = 1; i <= 10; i++)
				{
					for (int j = 0; j < 20; j++)
					{
						UnityButtonSource.buttonQueries[i - 1, j] = string.Concat(new object[]
						{
							"joystick ",
							i,
							" button ",
							j
						});
					}
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C20E File Offset: 0x0000A60E
		private static string GetButtonKey(int joystickId, int buttonId)
		{
			return UnityButtonSource.buttonQueries[joystickId - 1, buttonId];
		}

		// Token: 0x0400028B RID: 651
		private static string[,] buttonQueries;

		// Token: 0x0400028C RID: 652
		public int ButtonId;
	}
}
