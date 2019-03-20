using System;

namespace InControl
{
	// Token: 0x02000006 RID: 6
	public interface BindingSourceListener
	{
		// Token: 0x06000018 RID: 24
		void Reset();

		// Token: 0x06000019 RID: 25
		BindingSource Listen(BindingListenOptions listenOptions, InputDevice device);
	}
}
