using System;

namespace InControl
{
	// Token: 0x020000B5 RID: 181
	internal class RingBuffer<T>
	{
		// Token: 0x060003CB RID: 971 RVA: 0x00020098 File Offset: 0x0001E498
		public RingBuffer(int size)
		{
			if (size <= 0)
			{
				throw new ArgumentException("RingBuffer size must be 1 or greater.");
			}
			this.size = size + 1;
			this.data = new T[this.size];
			this.head = 0;
			this.tail = 0;
			this.sync = new object();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000200F0 File Offset: 0x0001E4F0
		public void Enqueue(T value)
		{
			object obj = this.sync;
			lock (obj)
			{
				if (this.size > 1)
				{
					this.head = (this.head + 1) % this.size;
					if (this.head == this.tail)
					{
						this.tail = (this.tail + 1) % this.size;
					}
				}
				this.data[this.head] = value;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00020180 File Offset: 0x0001E580
		public T Dequeue()
		{
			object obj = this.sync;
			T result;
			lock (obj)
			{
				if (this.size > 1 && this.tail != this.head)
				{
					this.tail = (this.tail + 1) % this.size;
				}
				result = this.data[this.tail];
			}
			return result;
		}

		// Token: 0x040002E7 RID: 743
		private int size;

		// Token: 0x040002E8 RID: 744
		private T[] data;

		// Token: 0x040002E9 RID: 745
		private int head;

		// Token: 0x040002EA RID: 746
		private int tail;

		// Token: 0x040002EB RID: 747
		private object sync;
	}
}
