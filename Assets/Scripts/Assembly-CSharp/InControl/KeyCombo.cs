using System;
using System.Collections.Generic;
using System.IO;

namespace InControl
{
	// Token: 0x0200000E RID: 14
	public struct KeyCombo
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002AFC File Offset: 0x00000EFC
		public KeyCombo(params Key[] keys)
		{
			this.data = 0UL;
			this.size = 0;
			for (int i = 0; i < keys.Length; i++)
			{
				this.Add(keys[i]);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B35 File Offset: 0x00000F35
		private void AddInt(int key)
		{
			if (this.size == 8)
			{
				return;
			}
			this.data |= (ulong)((ulong)((long)key & 255L) << this.size * 8);
			this.size++;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B74 File Offset: 0x00000F74
		private int GetInt(int index)
		{
			return (int)(this.data >> index * 8 & 255UL);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B8B File Offset: 0x00000F8B
		public void Add(Key key)
		{
			this.AddInt((int)key);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B94 File Offset: 0x00000F94
		public Key Get(int index)
		{
			if (index < 0 || index >= this.size)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"Index ",
					index,
					" is out of the range 0..",
					this.size
				}));
			}
			return (Key)this.GetInt(index);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002BF3 File Offset: 0x00000FF3
		public void Clear()
		{
			this.data = 0UL;
			this.size = 0;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002C04 File Offset: 0x00001004
		public int Count
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002C0C File Offset: 0x0000100C
		public bool IsPressed
		{
			get
			{
				if (this.size == 0)
				{
					return false;
				}
				bool flag = true;
				for (int i = 0; i < this.size; i++)
				{
					int @int = this.GetInt(i);
					flag = (flag && KeyInfo.KeyList[@int].IsPressed);
				}
				return flag;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C64 File Offset: 0x00001064
		public static KeyCombo Detect(bool modifiersAsKeys)
		{
			KeyCombo result = default(KeyCombo);
			if (modifiersAsKeys)
			{
				for (int i = 1; i < 5; i++)
				{
					if (KeyInfo.KeyList[i].IsPressed)
					{
						result.AddInt(i);
					}
				}
			}
			else
			{
				for (int j = 5; j < 13; j++)
				{
					if (KeyInfo.KeyList[j].IsPressed)
					{
						result.AddInt(j);
						return result;
					}
				}
			}
			for (int k = 13; k < KeyInfo.KeyList.Length; k++)
			{
				if (KeyInfo.KeyList[k].IsPressed)
				{
					result.AddInt(k);
					return result;
				}
			}
			result.Clear();
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D28 File Offset: 0x00001128
		public override string ToString()
		{
			string text;
			if (!KeyCombo.cachedStrings.TryGetValue(this.data, out text))
			{
				text = string.Empty;
				for (int i = 0; i < this.size; i++)
				{
					if (i != 0)
					{
						text += " ";
					}
					int @int = this.GetInt(i);
					text += KeyInfo.KeyList[@int].Name;
				}
			}
			return text;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D9B File Offset: 0x0000119B
		public static bool operator ==(KeyCombo a, KeyCombo b)
		{
			return a.data == b.data;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DAD File Offset: 0x000011AD
		public static bool operator !=(KeyCombo a, KeyCombo b)
		{
			return a.data != b.data;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002DC4 File Offset: 0x000011C4
		public override bool Equals(object other)
		{
			if (other is KeyCombo)
			{
				KeyCombo keyCombo = (KeyCombo)other;
				return this.data == keyCombo.data;
			}
			return false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002DF4 File Offset: 0x000011F4
		public override int GetHashCode()
		{
			return this.data.GetHashCode();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E07 File Offset: 0x00001207
		internal void Load(BinaryReader reader)
		{
			this.size = reader.ReadInt32();
			this.data = reader.ReadUInt64();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E21 File Offset: 0x00001221
		internal void Save(BinaryWriter writer)
		{
			writer.Write(this.size);
			writer.Write(this.data);
		}

		// Token: 0x04000095 RID: 149
		private int size;

		// Token: 0x04000096 RID: 150
		private ulong data;

		// Token: 0x04000097 RID: 151
		private static Dictionary<ulong, string> cachedStrings = new Dictionary<ulong, string>();
	}
}
