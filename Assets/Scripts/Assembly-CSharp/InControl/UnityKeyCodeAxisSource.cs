using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000045 RID: 69
	public class UnityKeyCodeAxisSource : InputControlSource
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000C317 File Offset: 0x0000A717
		public UnityKeyCodeAxisSource()
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C31F File Offset: 0x0000A71F
		public UnityKeyCodeAxisSource(KeyCode negativeKeyCode, KeyCode positiveKeyCode)
		{
			this.NegativeKeyCode = negativeKeyCode;
			this.PositiveKeyCode = positiveKeyCode;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000C338 File Offset: 0x0000A738
		public float GetValue(InputDevice inputDevice)
		{
			int num = 0;
			if (Input.GetKey(this.NegativeKeyCode))
			{
				num--;
			}
			if (Input.GetKey(this.PositiveKeyCode))
			{
				num++;
			}
			return (float)num;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000C371 File Offset: 0x0000A771
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x04000292 RID: 658
		public KeyCode NegativeKeyCode;

		// Token: 0x04000293 RID: 659
		public KeyCode PositiveKeyCode;
	}
}
