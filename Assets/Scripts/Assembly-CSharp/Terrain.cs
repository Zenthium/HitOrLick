using System;
using UnityEngine;
public class Terrain : MonoBehaviour
{
	// Token: 0x06000541 RID: 1345 RVA: 0x0002D335 File Offset: 0x0002B735
	private void Awake()
	{
		global::Terrain.instance = this;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0002D33D File Offset: 0x0002B73D
	public static Vector3 GetFlySpawnPoint()
	{
		if (global::Terrain.instance.flySpawn != null)
		{
			return global::Terrain.instance.flySpawn.position;
		}
		return Vector3.zero;
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x0002D369 File Offset: 0x0002B769
	public static Vector3 GetSpawnPoint(int index)
	{
		return global::Terrain.instance.spawnPoints[index].position;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x0002D37C File Offset: 0x0002B77C
	public static Vector3 GetSpawnPoint()
	{
		float[] array = new float[global::Terrain.instance.spawnPoints.Length];
		int num = 0;
		foreach (Transform transform in global::Terrain.instance.spawnPoints)
		{
			array[num] = float.MaxValue;
			foreach (Player player in GameController.activePlayers)
			{
				if (player.character != null)
				{
					float num2 = Vector2.Distance(player.character.transform.position, transform.position);
					if (num2 < array[num])
					{
						array[num] = num2;
					}
				}
			}
			num++;
		}
		int num3 = 0;
		float num4 = float.MinValue;
		num = 0;
		foreach (Transform transform2 in global::Terrain.instance.spawnPoints)
		{
			if (array[num] == num4 && UnityEngine.Random.value < 0.5f)
			{
				num3 = num;
				num4 = array[num];
			}
			else if (array[num] > num4)
			{
				num3 = num;
				num4 = array[num];
			}
			num++;
		}
		return global::Terrain.instance.spawnPoints[num3].position;
	}

	// Token: 0x17000120 RID: 288
	// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002D4EC File Offset: 0x0002B8EC
	public static float LeftKillPoint
	{
		get
		{
			return global::Terrain.instance.leftKillPoint.position.x;
		}
	}

	// Token: 0x17000121 RID: 289
	// (get) Token: 0x06000546 RID: 1350 RVA: 0x0002D510 File Offset: 0x0002B910
	public static float RightKillPoint
	{
		get
		{
			return global::Terrain.instance.rightKillPoint.position.x;
		}
	}

	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000547 RID: 1351 RVA: 0x0002D534 File Offset: 0x0002B934
	public static float TopKillPoint
	{
		get
		{
			return global::Terrain.instance.topKillPoint.position.y;
		}
	}

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000548 RID: 1352 RVA: 0x0002D558 File Offset: 0x0002B958
	public static float BotKillPoint
	{
		get
		{
			return global::Terrain.instance.botKillPoint.position.y;
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000549 RID: 1353 RVA: 0x0002D57C File Offset: 0x0002B97C
	public static float ScreenTop
	{
		get
		{
			return global::Terrain.instance.screenTop.position.y;
		}
	}

	// Token: 0x0400050F RID: 1295
	private static global::Terrain instance;

	// Token: 0x04000510 RID: 1296
	public Transform leftKillPoint;

	// Token: 0x04000511 RID: 1297
	public Transform rightKillPoint;

	// Token: 0x04000512 RID: 1298
	public Transform botKillPoint;

	// Token: 0x04000513 RID: 1299
	public Transform topKillPoint;

	// Token: 0x04000514 RID: 1300
	public Transform screenTop;

	// Token: 0x04000515 RID: 1301
	public Transform flySpawn;

	// Token: 0x04000516 RID: 1302
	public Transform[] spawnPoints;
}
