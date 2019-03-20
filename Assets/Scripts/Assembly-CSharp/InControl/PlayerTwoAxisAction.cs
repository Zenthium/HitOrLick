using System;

namespace InControl
{
	// Token: 0x02000016 RID: 22
	public class PlayerTwoAxisAction : TwoAxisInputControl
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005E4A File Offset: 0x0000424A
		internal PlayerTwoAxisAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
		{
			this.negativeXAction = negativeXAction;
			this.positiveXAction = positiveXAction;
			this.negativeYAction = negativeYAction;
			this.positiveYAction = positiveYAction;
			this.InvertYAxis = false;
			this.Raw = true;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00005E7D File Offset: 0x0000427D
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00005E85 File Offset: 0x00004285
		public bool InvertYAxis { get; set; }

		// Token: 0x060000AD RID: 173 RVA: 0x00005E90 File Offset: 0x00004290
		internal void Update(ulong updateTick, float deltaTime)
		{
			float x = Utility.ValueFromSides(this.negativeXAction, this.positiveXAction, false);
			float y = Utility.ValueFromSides(this.negativeYAction, this.positiveYAction, InputManager.InvertYAxis || this.InvertYAxis);
			base.UpdateWithAxes(x, y, updateTick, deltaTime);
		}

		// Token: 0x040000CC RID: 204
		private PlayerAction negativeXAction;

		// Token: 0x040000CD RID: 205
		private PlayerAction positiveXAction;

		// Token: 0x040000CE RID: 206
		private PlayerAction negativeYAction;

		// Token: 0x040000CF RID: 207
		private PlayerAction positiveYAction;
	}
}
