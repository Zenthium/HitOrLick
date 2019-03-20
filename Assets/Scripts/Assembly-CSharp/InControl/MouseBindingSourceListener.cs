using System;

namespace InControl
{
	// Token: 0x02000012 RID: 18
	public class MouseBindingSourceListener : BindingSourceListener
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000041F3 File Offset: 0x000025F3
		public void Reset()
		{
			this.detectFound = Mouse.None;
			this.detectPhase = 0;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004204 File Offset: 0x00002604
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeMouseButtons)
			{
				return null;
			}
			if (this.detectFound != Mouse.None && !MouseBindingSource.ButtonIsPressed(this.detectFound) && this.detectPhase == 2)
			{
				MouseBindingSource result = new MouseBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			Mouse mouse = this.ListenForControl();
			if (mouse != Mouse.None)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = mouse;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004298 File Offset: 0x00002698
		private Mouse ListenForControl()
		{
			for (Mouse mouse = Mouse.None; mouse <= Mouse.Button9; mouse++)
			{
				if (MouseBindingSource.ButtonIsPressed(mouse))
				{
					return mouse;
				}
			}
			return Mouse.None;
		}

		// Token: 0x040000B3 RID: 179
		private Mouse detectFound;

		// Token: 0x040000B4 RID: 180
		private int detectPhase;
	}
}
