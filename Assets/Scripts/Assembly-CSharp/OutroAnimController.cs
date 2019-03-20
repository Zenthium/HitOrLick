using System;
using UnityEngine;
using FreeLives;
public class OutroAnimController : MonoBehaviour
{
	// Token: 0x06000538 RID: 1336 RVA: 0x0002D2BF File Offset: 0x0002B6BF
	public void SwitchToCloseup()
	{
		this.closeUp.SetActive(true);
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x0002D2CD File Offset: 0x0002B6CD
	public void StopFrogAnimating()
	{
		this.anim.stopAnimating = true;
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0002D2DC File Offset: 0x0002B6DC
	private void Start()
	{
		foreach (SpriteRenderer spriteRenderer in this.frogSprites)
		{
			spriteRenderer.color = GameController.overallWinnerColor;
		}
		this.input = new InputState ();
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x0002D313 File Offset: 0x0002B713
	private void Update()
	{
		InputReader.GetInput (this.input);
		if (this.input.start && !this.input.wasStart) {
			this.StopFrogAnimating ();
		}
	}

	// Token: 0x0400050C RID: 1292
	public OutroVictoryAnim anim;

	// Token: 0x0400050D RID: 1293
	public SpriteRenderer[] frogSprites;

	// Token: 0x0400050E RID: 1294
	public GameObject closeUp;

	private InputState input;
}
