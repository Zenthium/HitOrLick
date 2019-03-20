using System;

namespace InControl
{
	// Token: 0x0200000D RID: 13
	public class KeyBindingSourceListener : BindingSourceListener
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002A40 File Offset: 0x00000E40
		public void Reset()
		{
			this.detectFound.Clear();
			this.detectPhase = 0;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A54 File Offset: 0x00000E54
		public BindingSource Listen(BindingListenOptions listenOptions, InputDevice device)
		{
			if (!listenOptions.IncludeKeys)
			{
				return null;
			}
			if (this.detectFound.Count > 0 && !this.detectFound.IsPressed && this.detectPhase == 2)
			{
				KeyBindingSource result = new KeyBindingSource(this.detectFound);
				this.Reset();
				return result;
			}
			KeyCombo keyCombo = KeyCombo.Detect(listenOptions.IncludeModifiersAsFirstClassKeys);
			if (keyCombo.Count > 0)
			{
				if (this.detectPhase == 1)
				{
					this.detectFound = keyCombo;
					this.detectPhase = 2;
				}
			}
			else if (this.detectPhase == 0)
			{
				this.detectPhase = 1;
			}
			return null;
		}

		// Token: 0x04000093 RID: 147
		private KeyCombo detectFound;

		// Token: 0x04000094 RID: 148
		private int detectPhase;
	}
}
