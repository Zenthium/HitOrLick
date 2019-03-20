using System;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000011 RID: 17
	public class MouseBindingSource : BindingSource
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00003F34 File Offset: 0x00002334
		internal MouseBindingSource()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003F3C File Offset: 0x0000233C
		public MouseBindingSource(Mouse mouseControl)
		{
			this.Control = mouseControl;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F4B File Offset: 0x0000234B
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003F53 File Offset: 0x00002353
		public Mouse Control { get; protected set; }

		// Token: 0x0600005C RID: 92 RVA: 0x00003F5C File Offset: 0x0000235C
		internal static bool SafeGetMouseButton(int button)
		{
			try
			{
				return Input.GetMouseButton(button);
			}
			catch (ArgumentException)
			{
			}
			return false;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003F90 File Offset: 0x00002390
		internal static bool ButtonIsPressed(Mouse control)
		{
			int num = MouseBindingSource.buttonTable[(int)control];
			return num >= 0 && MouseBindingSource.SafeGetMouseButton(num);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003FB4 File Offset: 0x000023B4
		public override float GetValue(InputDevice inputDevice)
		{
			int num = MouseBindingSource.buttonTable[(int)this.Control];
			if (num >= 0)
			{
				return (!MouseBindingSource.SafeGetMouseButton(num)) ? 0f : 1f;
			}
			switch (this.Control)
			{
			case Mouse.NegativeX:
				return -Mathf.Min(Input.GetAxisRaw("mouse x") * MouseBindingSource.ScaleX, 0f);
			case Mouse.PositiveX:
				return Mathf.Max(0f, Input.GetAxisRaw("mouse x") * MouseBindingSource.ScaleX);
			case Mouse.NegativeY:
				return -Mathf.Min(Input.GetAxisRaw("mouse y") * MouseBindingSource.ScaleY, 0f);
			case Mouse.PositiveY:
				return Mathf.Max(0f, Input.GetAxisRaw("mouse y") * MouseBindingSource.ScaleY);
			case Mouse.PositiveScrollWheel:
				return Mathf.Max(0f, Input.GetAxisRaw("mouse z") * MouseBindingSource.ScaleZ);
			case Mouse.NegativeScrollWheel:
				return -Mathf.Min(Input.GetAxisRaw("mouse z") * MouseBindingSource.ScaleZ, 0f);
			default:
				return 0f;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000040C6 File Offset: 0x000024C6
		public override bool GetState(InputDevice inputDevice)
		{
			return Utility.IsNotZero(this.GetValue(inputDevice));
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000040D4 File Offset: 0x000024D4
		public override string Name
		{
			get
			{
				return this.Control.ToString();
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000040F5 File Offset: 0x000024F5
		public override string DeviceName
		{
			get
			{
				return "Mouse";
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000040FC File Offset: 0x000024FC
		public override bool Equals(BindingSource other)
		{
			if (other == null)
			{
				return false;
			}
			MouseBindingSource mouseBindingSource = other as MouseBindingSource;
			return mouseBindingSource != null && this.Control == mouseBindingSource.Control;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000413C File Offset: 0x0000253C
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			MouseBindingSource mouseBindingSource = other as MouseBindingSource;
			return mouseBindingSource != null && this.Control == mouseBindingSource.Control;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004174 File Offset: 0x00002574
		public override int GetHashCode()
		{
			return this.Control.GetHashCode();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004195 File Offset: 0x00002595
		internal override BindingSourceType BindingSourceType
		{
			get
			{
				return BindingSourceType.MouseBindingSource;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004198 File Offset: 0x00002598
		internal override void Save(BinaryWriter writer)
		{
			writer.Write((int)this.Control);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000041A6 File Offset: 0x000025A6
		internal override void Load(BinaryReader reader)
		{
			this.Control = (Mouse)reader.ReadInt32();
		}

		// Token: 0x040000AF RID: 175
		public static float ScaleX = 0.2f;

		// Token: 0x040000B0 RID: 176
		public static float ScaleY = 0.2f;

		// Token: 0x040000B1 RID: 177
		public static float ScaleZ = 0.2f;

		// Token: 0x040000B2 RID: 178
		private static readonly int[] buttonTable = new int[]
		{
			-1,
			0,
			1,
			2,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			3,
			4,
			5,
			6,
			7,
			8
		};
	}
}
