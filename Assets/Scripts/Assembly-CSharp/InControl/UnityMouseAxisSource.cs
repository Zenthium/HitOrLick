using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000048 RID: 72
	public class UnityMouseAxisSource : InputControlSource
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000C45F File Offset: 0x0000A85F
		public UnityMouseAxisSource()
		{
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C467 File Offset: 0x0000A867
		public UnityMouseAxisSource(string axis)
		{
			this.MouseAxisQuery = "mouse " + axis;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C480 File Offset: 0x0000A880
		public float GetValue(InputDevice inputDevice)
		{
			return Input.GetAxisRaw(this.MouseAxisQuery);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000C48D File Offset: 0x0000A88D
		public bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x04000296 RID: 662
		public string MouseAxisQuery;
	}
}
