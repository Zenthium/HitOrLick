using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000049 RID: 73
	public class UnityMouseButtonSource : InputControlSource
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000C49B File Offset: 0x0000A89B
		public UnityMouseButtonSource()
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C4A3 File Offset: 0x0000A8A3
		public UnityMouseButtonSource(int buttonId)
		{
			this.ButtonId = buttonId;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C4B2 File Offset: 0x0000A8B2
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C4CF File Offset: 0x0000A8CF
		public bool GetState(InputDevice inputDevice)
		{
			return Input.GetMouseButton(this.ButtonId);
		}

		// Token: 0x04000297 RID: 663
		public int ButtonId;
	}
}
