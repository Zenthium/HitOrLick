using System;
using UnityEngine;
public class OutroVictoryAnim : MonoBehaviour
{
	// Token: 0x06000568 RID: 1384 RVA: 0x0002E228 File Offset: 0x0002C628
	private void Start()
	{
		this.rend = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0002E238 File Offset: 0x0002C638
	private void Update()
	{
		this.frameCounter += Time.deltaTime;
		if (this.frameCounter % 2f < 0.9f)
		{
			this.rend.sprite = this.frames[3];
			if (this.stopAnimating)
			{
				this.frameCounter -= Time.deltaTime;
			}
		}
		else if (this.frameCounter % 2f < 1f)
		{
			this.rend.sprite = this.frames[2];
		}
		else if (this.frameCounter % 2f < 1.9f)
		{
			this.rend.sprite = this.frames[1];
		}
		else
		{
			this.rend.sprite = this.frames[2];
		}
	}

	// Token: 0x04000553 RID: 1363
	public SpriteRenderer rend;

	// Token: 0x04000554 RID: 1364
	public Sprite[] frames;

	// Token: 0x04000555 RID: 1365
	public bool stopAnimating;

	// Token: 0x04000556 RID: 1366
	private float frameCounter;
}
