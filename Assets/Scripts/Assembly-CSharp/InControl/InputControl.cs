using System;

namespace InControl
{
	// Token: 0x0200001D RID: 29
	public class InputControl : InputControlBase
	{
		// Token: 0x060000DE RID: 222 RVA: 0x000069C5 File Offset: 0x00004DC5
		private InputControl()
		{
			this.Handle = "None";
			this.Target = InputControlType.None;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000069DF File Offset: 0x00004DDF
		public InputControl(string handle, InputControlType target)
		{
			this.Handle = handle;
			this.Target = target;
			this.IsButton = Utility.TargetIsButton(target);
			this.IsAnalog = !this.IsButton;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00006A10 File Offset: 0x00004E10
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00006A18 File Offset: 0x00004E18
		public string Handle { get; protected set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00006A21 File Offset: 0x00004E21
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00006A29 File Offset: 0x00004E29
		public InputControlType Target { get; protected set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00006A32 File Offset: 0x00004E32
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00006A3A File Offset: 0x00004E3A
		public bool IsButton { get; protected set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00006A43 File Offset: 0x00004E43
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00006A4B File Offset: 0x00004E4B
		public bool IsAnalog { get; protected set; }

		// Token: 0x060000E8 RID: 232 RVA: 0x00006A54 File Offset: 0x00004E54
		internal void SetZeroTick()
		{
			this.zeroTick = base.UpdateTick;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006A62 File Offset: 0x00004E62
		internal bool IsOnZeroTick
		{
			get
			{
				return base.UpdateTick == this.zeroTick;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00006A72 File Offset: 0x00004E72
		public bool IsStandard
		{
			get
			{
				return Utility.TargetIsStandard(this.Target);
			}
		}

		// Token: 0x040000E4 RID: 228
		public static readonly InputControl Null = new InputControl();

		// Token: 0x040000E9 RID: 233
		private ulong zeroTick;
	}
}
