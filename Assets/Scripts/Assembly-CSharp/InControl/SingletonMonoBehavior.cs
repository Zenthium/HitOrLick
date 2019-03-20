using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000B7 RID: 183
	public abstract class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000648E File Offset: 0x0000488E
		public static T Instance
		{
			get
			{
				return SingletonMonoBehavior<T>.GetInstance();
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00006498 File Offset: 0x00004898
		private static void CreateInstance()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(T).ToString();
			Debug.Log("Creating instance of singleton: " + gameObject.name);
			SingletonMonoBehavior<T>.instance = gameObject.AddComponent<T>();
			SingletonMonoBehavior<T>.hasInstance = true;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000064E8 File Offset: 0x000048E8
		private static T GetInstance()
		{
			object obj = SingletonMonoBehavior<T>.lockObject;
			T result;
			lock (obj)
			{
				if (SingletonMonoBehavior<T>.hasInstance)
				{
					result = SingletonMonoBehavior<T>.instance;
				}
				else
				{
					Type typeFromHandle = typeof(T);
					T[] array = UnityEngine.Object.FindObjectsOfType<T>();
					if (array.Length > 0)
					{
						SingletonMonoBehavior<T>.instance = array[0];
						SingletonMonoBehavior<T>.hasInstance = true;
						if (array.Length > 1)
						{
							Debug.LogWarning("Multiple instances of singleton " + typeFromHandle + " found; destroying all but the first.");
							for (int i = 1; i < array.Length; i++)
							{
								UnityEngine.Object.DestroyImmediate(array[i].gameObject);
							}
						}
						result = SingletonMonoBehavior<T>.instance;
					}
					else
					{
						SingletonPrefabAttribute singletonPrefabAttribute = Attribute.GetCustomAttribute(typeFromHandle, typeof(SingletonPrefabAttribute)) as SingletonPrefabAttribute;
						if (singletonPrefabAttribute == null)
						{
							SingletonMonoBehavior<T>.CreateInstance();
						}
						else
						{
							string name = singletonPrefabAttribute.Name;
							GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>(name));
							if (gameObject == null)
							{
								Debug.LogError(string.Concat(new object[]
								{
									"Could not find prefab ",
									name,
									" for singleton of type ",
									typeFromHandle,
									"."
								}));
								SingletonMonoBehavior<T>.CreateInstance();
							}
							else
							{
								gameObject.name = name;
								SingletonMonoBehavior<T>.instance = gameObject.GetComponent<T>();
								if (SingletonMonoBehavior<T>.instance == null)
								{
									Debug.LogWarning(string.Concat(new object[]
									{
										"There wasn't a component of type \"",
										typeFromHandle,
										"\" inside prefab \"",
										name,
										"\"; creating one now."
									}));
									SingletonMonoBehavior<T>.instance = gameObject.AddComponent<T>();
									SingletonMonoBehavior<T>.hasInstance = true;
								}
							}
						}
						result = SingletonMonoBehavior<T>.instance;
					}
				}
			}
			return result;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000066C0 File Offset: 0x00004AC0
		private static void EnforceSingleton()
		{
			object obj = SingletonMonoBehavior<T>.lockObject;
			lock (obj)
			{
				if (SingletonMonoBehavior<T>.hasInstance)
				{
					T[] array = UnityEngine.Object.FindObjectsOfType<T>();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].GetInstanceID() != SingletonMonoBehavior<T>.instance.GetInstanceID())
						{
							UnityEngine.Object.DestroyImmediate(array[i].gameObject);
						}
					}
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000675C File Offset: 0x00004B5C
		protected bool SetupSingleton()
		{
			SingletonMonoBehavior<T>.EnforceSingleton();
			int instanceID = base.GetInstanceID();
			T t = SingletonMonoBehavior<T>.Instance;
			return instanceID == t.GetInstanceID();
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00006789 File Offset: 0x00004B89
		private void OnDestroy()
		{
			SingletonMonoBehavior<T>.hasInstance = false;
		}

		// Token: 0x040002ED RID: 749
		private static T instance;

		// Token: 0x040002EE RID: 750
		private static bool hasInstance;

		// Token: 0x040002EF RID: 751
		private static object lockObject = new object();
	}
}
