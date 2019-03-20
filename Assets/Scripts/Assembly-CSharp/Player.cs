using System;
using FreeLives;
using UnityEngine;
public class Player
{
	// Token: 0x0600057A RID: 1402 RVA: 0x0002EBF4 File Offset: 0x0002CFF4
	public Player(InputReader.Device inputDevice, Color color, int sortPriority)
	{
		this.inputDevice = inputDevice;
		this.color = color;
		this.sortPriority = sortPriority;
	}

	// Token: 0x0400057B RID: 1403
	public InputReader.Device inputDevice;

	// Token: 0x0400057C RID: 1404
	public Team team;

	// Token: 0x0400057D RID: 1405
	public Color color;

	// Token: 0x0400057E RID: 1406
	public int score;

	// Token: 0x0400057F RID: 1407
	public int roundWins;

	// Token: 0x04000580 RID: 1408
	public SpriteRenderer offscreenDot;

	// Token: 0x04000581 RID: 1409
	public int sortPriority;

	// Token: 0x04000582 RID: 1410
	public Character character;

	// Token: 0x04000583 RID: 1411
	public float spawnDelay;
}
