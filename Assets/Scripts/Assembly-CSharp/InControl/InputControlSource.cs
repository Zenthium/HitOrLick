using System;

namespace InControl
{
	// Token: 0x02000020 RID: 32
	public interface InputControlSource
	{
		// Token: 0x06000111 RID: 273
		float GetValue(InputDevice inputDevice);

		// Token: 0x06000112 RID: 274
		bool GetState(InputDevice inputDevice);
	}
}
