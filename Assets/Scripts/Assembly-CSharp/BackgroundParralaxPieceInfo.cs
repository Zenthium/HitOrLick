using System;
using UnityEngine;
[Serializable]
public class BackgroundParralaxPieceInfo
{
	// Token: 0x0400051C RID: 1308
	public SpriteRenderer[] prefabs;

	// Token: 0x0400051D RID: 1309
	public Vector2 speedMin;

	// Token: 0x0400051E RID: 1310
	public Vector2 speedMax;

	// Token: 0x0400051F RID: 1311
	public float depth;

	// Token: 0x04000520 RID: 1312
	public float probability;

	// Token: 0x04000521 RID: 1313
	public Vector2 spawnRangeY;

	// Token: 0x04000522 RID: 1314
	public Vector2 spawnRangeX;

	// Token: 0x04000523 RID: 1315
	public bool dontScale;

	// Token: 0x04000524 RID: 1316
	public bool parentToCamera;

	// Token: 0x04000525 RID: 1317
	public bool dropFrog;
}
