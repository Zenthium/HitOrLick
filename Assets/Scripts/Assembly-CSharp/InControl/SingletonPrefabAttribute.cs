using System;

namespace InControl
{
	// Token: 0x020000B6 RID: 182
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class SingletonPrefabAttribute : Attribute
	{
		// Token: 0x060003CE RID: 974 RVA: 0x000201FC File Offset: 0x0001E5FC
		public SingletonPrefabAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x040002EC RID: 748
		public readonly string Name;
	}
}
