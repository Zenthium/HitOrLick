using System;

namespace InControl
{
	// Token: 0x02000015 RID: 21
	public class PlayerOneAxisAction : OneAxisInputControl
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00005A05 File Offset: 0x00003E05
		internal PlayerOneAxisAction(PlayerAction negativeAction, PlayerAction positiveAction)
		{
			this.negativeAction = negativeAction;
			this.positiveAction = positiveAction;
			this.Raw = true;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005A24 File Offset: 0x00003E24
		internal void Update(ulong updateTick, float deltaTime)
		{
			float value = Utility.ValueFromSides(this.negativeAction, this.positiveAction);
			base.CommitWithValue(value, updateTick, deltaTime);
		}

		// Token: 0x040000CA RID: 202
		private PlayerAction negativeAction;

		// Token: 0x040000CB RID: 203
		private PlayerAction positiveAction;
	}
}
