using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using UnityEngine;

namespace InControl
{
	// Token: 0x02000014 RID: 20
	public abstract class PlayerActionSet
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000053BC File Offset: 0x000037BC
		protected PlayerActionSet()
		{
			this.Actions = new ReadOnlyCollection<PlayerAction>(this.actions);
			this.Enabled = true;
			InputManager.AttachPlayerActionSet(this);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005424 File Offset: 0x00003824
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000542C File Offset: 0x0000382C
		public InputDevice Device { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005435 File Offset: 0x00003835
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000543D File Offset: 0x0000383D
		public ReadOnlyCollection<PlayerAction> Actions { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005446 File Offset: 0x00003846
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000544E File Offset: 0x0000384E
		public ulong UpdateTick { get; protected set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005457 File Offset: 0x00003857
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000545F File Offset: 0x0000385F
		public bool Enabled { get; set; }

		// Token: 0x0600009B RID: 155 RVA: 0x00005468 File Offset: 0x00003868
		public void Destroy()
		{
			InputManager.DetachPlayerActionSet(this);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005470 File Offset: 0x00003870
		protected PlayerAction CreatePlayerAction(string name)
		{
			PlayerAction playerAction = new PlayerAction(name, this);
			playerAction.Device = (this.Device ?? InputManager.ActiveDevice);
			if (this.actionsByName.ContainsKey(name))
			{
				throw new InControlException("Action '" + name + "' already exists in this set.");
			}
			this.actions.Add(playerAction);
			this.actionsByName.Add(name, playerAction);
			return playerAction;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000054E0 File Offset: 0x000038E0
		protected PlayerOneAxisAction CreateOneAxisPlayerAction(PlayerAction negativeAction, PlayerAction positiveAction)
		{
			PlayerOneAxisAction playerOneAxisAction = new PlayerOneAxisAction(negativeAction, positiveAction);
			this.oneAxisActions.Add(playerOneAxisAction);
			return playerOneAxisAction;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005504 File Offset: 0x00003904
		protected PlayerTwoAxisAction CreateTwoAxisPlayerAction(PlayerAction negativeXAction, PlayerAction positiveXAction, PlayerAction negativeYAction, PlayerAction positiveYAction)
		{
			PlayerTwoAxisAction playerTwoAxisAction = new PlayerTwoAxisAction(negativeXAction, positiveXAction, negativeYAction, positiveYAction);
			this.twoAxisActions.Add(playerTwoAxisAction);
			return playerTwoAxisAction;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000552C File Offset: 0x0000392C
		internal void Update(ulong updateTick, float deltaTime)
		{
			InputDevice device = this.Device ?? InputManager.ActiveDevice;
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				PlayerAction playerAction = this.actions[i];
				playerAction.Update(updateTick, deltaTime, device);
				if (playerAction.UpdateTick > this.UpdateTick)
				{
					this.UpdateTick = playerAction.UpdateTick;
					this.LastInputType = playerAction.LastInputType;
				}
			}
			int count2 = this.oneAxisActions.Count;
			for (int j = 0; j < count2; j++)
			{
				this.oneAxisActions[j].Update(updateTick, deltaTime);
			}
			int count3 = this.twoAxisActions.Count;
			for (int k = 0; k < count3; k++)
			{
				this.twoAxisActions[k].Update(updateTick, deltaTime);
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000561C File Offset: 0x00003A1C
		public void Reset()
		{
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].ResetBindings();
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00005658 File Offset: 0x00003A58
		public void ClearInputState()
		{
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].ClearInputState();
			}
			int count2 = this.oneAxisActions.Count;
			for (int j = 0; j < count2; j++)
			{
				this.oneAxisActions[j].ClearInputState();
			}
			int count3 = this.twoAxisActions.Count;
			for (int k = 0; k < count3; k++)
			{
				this.twoAxisActions[k].ClearInputState();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000056FC File Offset: 0x00003AFC
		internal bool HasBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return false;
			}
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.actions[i].HasBinding(binding))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00005750 File Offset: 0x00003B50
		internal void RemoveBinding(BindingSource binding)
		{
			if (binding == null)
			{
				return;
			}
			int count = this.actions.Count;
			for (int i = 0; i < count; i++)
			{
				this.actions[i].FindAndRemoveBinding(binding);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000579A File Offset: 0x00003B9A
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000057A2 File Offset: 0x00003BA2
		public BindingListenOptions ListenOptions
		{
			get
			{
				return this.listenOptions;
			}
			set
			{
				this.listenOptions = (value ?? new BindingListenOptions());
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000057B8 File Offset: 0x00003BB8
		public string Save()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					binaryWriter.Write(66);
					binaryWriter.Write(73);
					binaryWriter.Write(78);
					binaryWriter.Write(68);
					binaryWriter.Write(1);
					int count = this.actions.Count;
					binaryWriter.Write(count);
					for (int i = 0; i < count; i++)
					{
						this.actions[i].Save(binaryWriter);
					}
				}
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005884 File Offset: 0x00003C84
		public void Load(string data)
		{
			if (data == null)
			{
				return;
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(data)))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						if (binaryReader.ReadUInt32() != 1145981250u)
						{
							throw new Exception("Unknown data format.");
						}
						if (binaryReader.ReadUInt16() != 1)
						{
							throw new Exception("Unknown data version.");
						}
						int num = binaryReader.ReadInt32();
						for (int i = 0; i < num; i++)
						{
							PlayerAction playerAction;
							if (this.actionsByName.TryGetValue(binaryReader.ReadString(), out playerAction))
							{
								playerAction.Load(binaryReader);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Provided state could not be loaded:\n" + ex.Message);
				this.Reset();
			}
		}

		// Token: 0x040000C2 RID: 194
		public BindingSourceType LastInputType;

		// Token: 0x040000C4 RID: 196
		private List<PlayerAction> actions = new List<PlayerAction>();

		// Token: 0x040000C5 RID: 197
		private List<PlayerOneAxisAction> oneAxisActions = new List<PlayerOneAxisAction>();

		// Token: 0x040000C6 RID: 198
		private List<PlayerTwoAxisAction> twoAxisActions = new List<PlayerTwoAxisAction>();

		// Token: 0x040000C7 RID: 199
		private Dictionary<string, PlayerAction> actionsByName = new Dictionary<string, PlayerAction>();

		// Token: 0x040000C8 RID: 200
		private BindingListenOptions listenOptions = new BindingListenOptions();

		// Token: 0x040000C9 RID: 201
		internal PlayerAction listenWithAction;
	}
}
