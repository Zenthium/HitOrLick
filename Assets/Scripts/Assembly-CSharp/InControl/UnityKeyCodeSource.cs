using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000047 RID: 71
	public class UnityKeyCodeSource : InputControlSource
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000C3EF File Offset: 0x0000A7EF
		public UnityKeyCodeSource()
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000C3F7 File Offset: 0x0000A7F7
		public UnityKeyCodeSource(params KeyCode[] keyCodeList)
		{
			this.KeyCodeList = keyCodeList;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C406 File Offset: 0x0000A806
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C424 File Offset: 0x0000A824
		public bool GetState(InputDevice inputDevice)
		{
			for (int i = 0; i < this.KeyCodeList.Length; i++)
			{
				if (Input.GetKey(this.KeyCodeList[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000295 RID: 661
		public KeyCode[] KeyCodeList;
	}
}
