using System;
using UnityEngine;

namespace FreeLives
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public class SoundInfo
	{
		// Token: 0x040003E0 RID: 992
		public string name;

		// Token: 0x040003E1 RID: 993
		public AudioClip[] clips;

		// Token: 0x040003E2 RID: 994
		public float volumeMod = 1f;

		// Token: 0x040003E3 RID: 995
		public float pitchVariance = 0.1f;
	}
}
