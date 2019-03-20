using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000013 RID: 19
	public class PlayerAction : InputControlBase
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000047F0 File Offset: 0x00002BF0
		public PlayerAction(string name, PlayerActionSet owner)
		{
			this.Raw = true;
			this.Name = name;
			this.Owner = owner;
			this.bindings = new ReadOnlyCollection<BindingSource>(this.visibleBindings);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000484A File Offset: 0x00002C4A
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00004852 File Offset: 0x00002C52
		public string Name { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000485B File Offset: 0x00002C5B
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00004863 File Offset: 0x00002C63
		public PlayerActionSet Owner { get; private set; }

		// Token: 0x06000072 RID: 114 RVA: 0x0000486C File Offset: 0x00002C6C
		public void AddDefaultBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			if (binding.BoundTo != null)
			{
				throw new InControlException("Binding source is already bound to action " + binding.BoundTo.Name);
			}
			if (!this.defaultBindings.Contains(binding))
			{
				this.defaultBindings.Add(binding);
				binding.BoundTo = this;
			}
			if (!this.regularBindings.Contains(binding))
			{
				this.regularBindings.Add(binding);
				binding.BoundTo = this;
				if (binding.IsValid)
				{
					this.visibleBindings.Add(binding);
				}
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000490B File Offset: 0x00002D0B
		public void AddDefaultBinding(params Key[] keys)
		{
			this.AddDefaultBinding(new KeyBindingSource(keys));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004919 File Offset: 0x00002D19
		public void AddDefaultBinding(Mouse control)
		{
			this.AddDefaultBinding(new MouseBindingSource(control));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004927 File Offset: 0x00002D27
		public void AddDefaultBinding(InputControlType control)
		{
			this.AddDefaultBinding(new DeviceBindingSource(control));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004938 File Offset: 0x00002D38
		public bool AddBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			if (binding.BoundTo != null)
			{
				Debug.LogWarning("Binding source is already bound to action " + binding.BoundTo.Name);
				return false;
			}
			if (this.regularBindings.Contains(binding))
			{
				return false;
			}
			this.regularBindings.Add(binding);
			binding.BoundTo = this;
			if (binding.IsValid)
			{
				this.visibleBindings.Add(binding);
			}
			return true;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000049B8 File Offset: 0x00002DB8
		public bool InsertBindingAt(int index, BindingSource binding)
		{
			if (index < 0 || index > this.visibleBindings.Count)
			{
				throw new InControlException("Index is out of range for bindings on this action.");
			}
			if (index == this.visibleBindings.Count)
			{
				return this.AddBinding(binding);
			}
			if (binding == null)
			{
				return false;
			}
			if (binding.BoundTo != null)
			{
				Debug.LogWarning("Binding source is already bound to action " + binding.BoundTo.Name);
				return false;
			}
			if (this.regularBindings.Contains(binding))
			{
				return false;
			}
			int index2 = (index != 0) ? this.regularBindings.IndexOf(this.visibleBindings[index]) : 0;
			this.regularBindings.Insert(index2, binding);
			binding.BoundTo = this;
			if (binding.IsValid)
			{
				this.visibleBindings.Insert(index, binding);
			}
			return true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004A9C File Offset: 0x00002E9C
		public bool ReplaceBinding(BindingSource findBinding, BindingSource withBinding)
		{
			if (findBinding == null || withBinding == null)
			{
				return false;
			}
			if (withBinding.BoundTo != null)
			{
				Debug.LogWarning("Binding source is already bound to action " + withBinding.BoundTo.Name);
				return false;
			}
			int num = this.regularBindings.IndexOf(findBinding);
			if (num < 0)
			{
				Debug.LogWarning("Binding source to replace is not present in this action.");
				return false;
			}
			Debug.Log("index = " + num);
			findBinding.BoundTo = null;
			this.regularBindings[num] = withBinding;
			withBinding.BoundTo = this;
			num = this.visibleBindings.IndexOf(findBinding);
			if (num >= 0)
			{
				this.visibleBindings[num] = withBinding;
			}
			return true;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004B5C File Offset: 0x00002F5C
		internal bool HasBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			BindingSource bindingSource = this.FindBinding(binding);
			return !(bindingSource == null) && bindingSource.BoundTo == this;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004B98 File Offset: 0x00002F98
		internal BindingSource FindBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return null;
			}
			int num = this.regularBindings.IndexOf(binding);
			if (num >= 0)
			{
				return this.regularBindings[num];
			}
			return null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004BD8 File Offset: 0x00002FD8
		internal void FindAndRemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			int num = this.regularBindings.IndexOf(binding);
			if (num >= 0)
			{
				BindingSource bindingSource = this.regularBindings[num];
				if (bindingSource.BoundTo == this)
				{
					bindingSource.BoundTo = null;
					this.regularBindings.RemoveAt(num);
					this.UpdateVisibleBindings();
				}
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004C38 File Offset: 0x00003038
		internal int CountBindingsOfType(BindingSourceType bindingSourceType)
		{
			int num = 0;
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo == this && bindingSource.BindingSourceType == bindingSourceType)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004C90 File Offset: 0x00003090
		internal void RemoveFirstBindingOfType(BindingSourceType bindingSourceType)
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo == this && bindingSource.BindingSourceType == bindingSourceType)
				{
					bindingSource.BoundTo = null;
					this.regularBindings.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004CF4 File Offset: 0x000030F4
		internal int IndexOfFirstInvalidBinding()
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				if (!this.regularBindings[i].IsValid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004D38 File Offset: 0x00003138
		public void RemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			if (binding.BoundTo != this)
			{
				throw new InControlException("Cannot remove a binding source not bound to this action.");
			}
			binding.BoundTo = null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004D65 File Offset: 0x00003165
		public void RemoveBindingAt(int index)
		{
			if (index < 0 || index >= this.regularBindings.Count)
			{
				throw new InControlException("Index is out of range for bindings on this action.");
			}
			this.regularBindings[index].BoundTo = null;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004D9C File Offset: 0x0000319C
		public void ClearBindings()
		{
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				this.regularBindings[i].BoundTo = null;
			}
			this.regularBindings.Clear();
			this.visibleBindings.Clear();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004DF0 File Offset: 0x000031F0
		public void ResetBindings()
		{
			this.ClearBindings();
			this.regularBindings.AddRange(this.defaultBindings);
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				bindingSource.BoundTo = this;
				if (bindingSource.IsValid)
				{
					this.visibleBindings.Add(bindingSource);
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004E5D File Offset: 0x0000325D
		public void ListenForBinding()
		{
			this.ListenForBindingReplacing(null);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004E68 File Offset: 0x00003268
		public void ListenForBindingReplacing(BindingSource binding)
		{
			BindingListenOptions bindingListenOptions = this.ListenOptions ?? this.Owner.ListenOptions;
			bindingListenOptions.ReplaceBinding = binding;
			this.Owner.listenWithAction = this;
			int num = PlayerAction.bindingSourceListeners.Length;
			for (int i = 0; i < num; i++)
			{
				PlayerAction.bindingSourceListeners[i].Reset();
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004EC7 File Offset: 0x000032C7
		public void StopListeningForBinding()
		{
			if (this.IsListeningForBinding)
			{
				this.Owner.listenWithAction = null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004EE0 File Offset: 0x000032E0
		public bool IsListeningForBinding
		{
			get
			{
				return this.Owner.listenWithAction == this;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004EF0 File Offset: 0x000032F0
		public ReadOnlyCollection<BindingSource> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004EF8 File Offset: 0x000032F8
		private void RemoveOrphanedBindings()
		{
			int count = this.regularBindings.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				if (this.regularBindings[i].BoundTo != this)
				{
					this.regularBindings.RemoveAt(i);
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004F48 File Offset: 0x00003348
		internal void Update(ulong updateTick, float deltaTime, InputDevice device)
		{
			this.Device = device;
			this.UpdateBindings(updateTick, deltaTime);
			this.DetectBindings();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004F60 File Offset: 0x00003360
		private void UpdateBindings(ulong updateTick, float deltaTime)
		{
			int count = this.regularBindings.Count;
			for (int i = count - 1; i >= 0; i--)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.BoundTo != this)
				{
					this.regularBindings.RemoveAt(i);
					this.visibleBindings.Remove(bindingSource);
				}
				else
				{
					float value = bindingSource.GetValue(this.Device);
					if (base.UpdateWithValue(value, updateTick, deltaTime))
					{
						this.LastInputType = bindingSource.BindingSourceType;
					}
				}
			}
			base.Commit();
			this.Enabled = this.Owner.Enabled;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005004 File Offset: 0x00003404
		private void DetectBindings()
		{
			if (this.IsListeningForBinding)
			{
				BindingSource bindingSource = null;
				BindingListenOptions bindingListenOptions = this.ListenOptions ?? this.Owner.ListenOptions;
				int num = PlayerAction.bindingSourceListeners.Length;
				for (int i = 0; i < num; i++)
				{
					bindingSource = PlayerAction.bindingSourceListeners[i].Listen(bindingListenOptions, this.device);
					if (bindingSource != null)
					{
						break;
					}
				}
				if (bindingSource == null)
				{
					return;
				}
				Func<PlayerAction, BindingSource, bool> onBindingFound = bindingListenOptions.OnBindingFound;
				if (onBindingFound != null && !onBindingFound(this, bindingSource))
				{
					return;
				}
				if (this.HasBinding(bindingSource))
				{
					Action<PlayerAction, BindingSource, BindingSourceRejectionType> onBindingRejected = bindingListenOptions.OnBindingRejected;
					if (onBindingRejected != null)
					{
						onBindingRejected(this, bindingSource, BindingSourceRejectionType.DuplicateBindingOnAction);
					}
					return;
				}
				if (bindingListenOptions.UnsetDuplicateBindingsOnSet)
				{
					this.Owner.RemoveBinding(bindingSource);
				}
				if (!bindingListenOptions.AllowDuplicateBindingsPerSet && this.Owner.HasBinding(bindingSource))
				{
					Action<PlayerAction, BindingSource, BindingSourceRejectionType> onBindingRejected2 = bindingListenOptions.OnBindingRejected;
					if (onBindingRejected2 != null)
					{
						onBindingRejected2(this, bindingSource, BindingSourceRejectionType.DuplicateBindingOnActionSet);
					}
					return;
				}
				this.StopListeningForBinding();
				if (bindingListenOptions.ReplaceBinding == null)
				{
					if (bindingListenOptions.MaxAllowedBindingsPerType > 0u)
					{
						while ((long)this.CountBindingsOfType(bindingSource.BindingSourceType) >= (long)((ulong)bindingListenOptions.MaxAllowedBindingsPerType))
						{
							this.RemoveFirstBindingOfType(bindingSource.BindingSourceType);
						}
					}
					else if (bindingListenOptions.MaxAllowedBindings > 0u)
					{
						while ((long)this.regularBindings.Count >= (long)((ulong)bindingListenOptions.MaxAllowedBindings))
						{
							int index = Mathf.Max(0, this.IndexOfFirstInvalidBinding());
							this.regularBindings.RemoveAt(index);
						}
					}
					this.AddBinding(bindingSource);
				}
				else
				{
					this.ReplaceBinding(bindingListenOptions.ReplaceBinding, bindingSource);
				}
				this.UpdateVisibleBindings();
				Action<PlayerAction, BindingSource> onBindingAdded = bindingListenOptions.OnBindingAdded;
				if (onBindingAdded != null)
				{
					onBindingAdded(this, bindingSource);
				}
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000051E4 File Offset: 0x000035E4
		private void UpdateVisibleBindings()
		{
			this.visibleBindings.Clear();
			int count = this.regularBindings.Count;
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				if (bindingSource.IsValid)
				{
					this.visibleBindings.Add(bindingSource);
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000523E File Offset: 0x0000363E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00005268 File Offset: 0x00003668
		internal InputDevice Device
		{
			get
			{
				if (this.device == null)
				{
					this.device = this.Owner.Device;
					this.UpdateVisibleBindings();
				}
				return this.device;
			}
			set
			{
				if (this.device != value)
				{
					this.device = value;
					this.UpdateVisibleBindings();
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005284 File Offset: 0x00003684
		internal void Load(BinaryReader reader)
		{
			this.ClearBindings();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				BindingSourceType bindingSourceType = (BindingSourceType)reader.ReadInt32();
				BindingSource bindingSource;
				switch (bindingSourceType)
				{
				case BindingSourceType.DeviceBindingSource:
					bindingSource = new DeviceBindingSource();
					break;
				case BindingSourceType.KeyBindingSource:
					bindingSource = new KeyBindingSource();
					break;
				case BindingSourceType.MouseBindingSource:
					bindingSource = new MouseBindingSource();
					break;
				case BindingSourceType.UnknownDeviceBindingSource:
					bindingSource = new UnknownDeviceBindingSource();
					break;
				default:
					throw new InControlException("Don't know how to load BindingSourceType: " + bindingSourceType);
				}
				bindingSource.Load(reader);
				this.AddBinding(bindingSource);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005328 File Offset: 0x00003728
		internal void Save(BinaryWriter writer)
		{
			this.RemoveOrphanedBindings();
			writer.Write(this.Name);
			int count = this.regularBindings.Count;
			writer.Write(count);
			for (int i = 0; i < count; i++)
			{
				BindingSource bindingSource = this.regularBindings[i];
				writer.Write((int)bindingSource.BindingSourceType);
				bindingSource.Save(writer);
			}
		}

		// Token: 0x040000B7 RID: 183
		public BindingListenOptions ListenOptions;

		// Token: 0x040000B8 RID: 184
		public BindingSourceType LastInputType;

		// Token: 0x040000B9 RID: 185
		private List<BindingSource> defaultBindings = new List<BindingSource>();

		// Token: 0x040000BA RID: 186
		private List<BindingSource> regularBindings = new List<BindingSource>();

		// Token: 0x040000BB RID: 187
		private List<BindingSource> visibleBindings = new List<BindingSource>();

		// Token: 0x040000BC RID: 188
		private readonly ReadOnlyCollection<BindingSource> bindings;

		// Token: 0x040000BD RID: 189
		private static readonly BindingSourceListener[] bindingSourceListeners = new BindingSourceListener[]
		{
			new DeviceBindingSourceListener(),
			new UnknownDeviceBindingSourceListener(),
			new KeyBindingSourceListener(),
			new MouseBindingSourceListener()
		};

		// Token: 0x040000BE RID: 190
		private InputDevice device;
	}
}
