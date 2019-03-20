using System;

namespace InControl
{
	// Token: 0x0200001C RID: 28
	public interface IInputControl
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D9 RID: 217
		bool HasChanged { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000DA RID: 218
		bool IsPressed { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000DB RID: 219
		bool WasPressed { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DC RID: 220
		bool WasReleased { get; }

		// Token: 0x060000DD RID: 221
		void ClearInputState();
	}
}
