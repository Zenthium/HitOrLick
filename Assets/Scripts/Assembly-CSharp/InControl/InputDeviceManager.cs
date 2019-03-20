using System;
using System.Collections.Generic;

namespace InControl
{
	// Token: 0x0200002D RID: 45
	public abstract class InputDeviceManager
	{
		// Token: 0x060001AD RID: 429
		public abstract void Update(ulong updateTick, float deltaTime);

		// Token: 0x060001AE RID: 430 RVA: 0x00007E83 File Offset: 0x00006283
		public virtual void Destroy()
		{
		}

		// Token: 0x0400019C RID: 412
		protected List<InputDevice> devices = new List<InputDevice>();
	}
}
