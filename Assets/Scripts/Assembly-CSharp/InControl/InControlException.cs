using System;

namespace InControl
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class InControlException : Exception
	{
		// Token: 0x06000146 RID: 326 RVA: 0x00006EA6 File Offset: 0x000052A6
		public InControlException()
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006EAE File Offset: 0x000052AE
		public InControlException(string message) : base(message)
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006EB7 File Offset: 0x000052B7
		public InControlException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
