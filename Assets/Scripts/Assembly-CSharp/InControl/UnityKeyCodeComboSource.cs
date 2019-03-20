using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000046 RID: 70
	public class UnityKeyCodeComboSource : InputControlSource
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000C37F File Offset: 0x0000A77F
		public UnityKeyCodeComboSource()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C387 File Offset: 0x0000A787
		public UnityKeyCodeComboSource(params KeyCode[] keyCodeList)
		{
			this.KeyCodeList = keyCodeList;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C396 File Offset: 0x0000A796
		public float GetValue(InputDevice inputDevice)
		{
			return (!this.GetState(inputDevice)) ? 0f : 1f;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000C3B4 File Offset: 0x0000A7B4
		public bool GetState(InputDevice inputDevice)
		{
			for (int i = 0; i < this.KeyCodeList.Length; i++)
			{
				if (!Input.GetKey(this.KeyCodeList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000294 RID: 660
		public KeyCode[] KeyCodeList;
	}
}
