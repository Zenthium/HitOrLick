using System;

namespace InControl
{
	// Token: 0x02000004 RID: 4
	public class BindingListenOptions
	{
		// Token: 0x04000008 RID: 8
		public bool IncludeControllers = true;

		// Token: 0x04000009 RID: 9
		public bool IncludeUnknownControllers;

		// Token: 0x0400000A RID: 10
		public bool IncludeNonStandardControls = true;

		// Token: 0x0400000B RID: 11
		public bool IncludeMouseButtons;

		// Token: 0x0400000C RID: 12
		public bool IncludeKeys = true;

		// Token: 0x0400000D RID: 13
		public bool IncludeModifiersAsFirstClassKeys;

		// Token: 0x0400000E RID: 14
		public uint MaxAllowedBindings;

		// Token: 0x0400000F RID: 15
		public uint MaxAllowedBindingsPerType;

		// Token: 0x04000010 RID: 16
		public bool AllowDuplicateBindingsPerSet;

		// Token: 0x04000011 RID: 17
		public bool UnsetDuplicateBindingsOnSet;

		// Token: 0x04000012 RID: 18
		public BindingSource ReplaceBinding;

		// Token: 0x04000013 RID: 19
		public Func<PlayerAction, BindingSource, bool> OnBindingFound;

		// Token: 0x04000014 RID: 20
		public Action<PlayerAction, BindingSource> OnBindingAdded;

		// Token: 0x04000015 RID: 21
		public Action<PlayerAction, BindingSource, BindingSourceRejectionType> OnBindingRejected;
	}
}
