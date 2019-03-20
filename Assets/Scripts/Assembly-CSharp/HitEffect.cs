using System;
using UnityEngine;
public class HitEffect : MonoBehaviour
{
	// Token: 0x0600055A RID: 1370 RVA: 0x0002DC18 File Offset: 0x0002C018
	public void SetColors(Color[] colors)
	{
		this.colors = colors;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x0002DC21 File Offset: 0x0002C021
	private void Start()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.rotationCounter = UnityEngine.Random.Range(0f, this.rotationDelay);
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x0002DC48 File Offset: 0x0002C048
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		this.counter += deltaTime;
		this.colorCounter += deltaTime;
		this.rotationCounter += deltaTime;
		if (this.counter < this.life)
		{
			if (this.flipColors && this.colorCounter >= 0.04f)
			{
				this.colorIndex++;
				this.colorCounter -= 0.04f;
				this.spriteRenderer.color = this.colors[this.colorIndex % this.colors.Length];
			}
			if (this.counter < this.growTime)
			{
				base.transform.localScale = this.counter / this.growTime * Vector3.one * this.scale * (1f + Mathf.Sin(Time.time * 40f + this.scaleCounter) * 0.15f);
			}
			else
			{
				base.transform.localScale = Vector3.one * this.scale * (1f + Mathf.Sin(Time.time * 40f + this.scaleCounter) * 0.15f);
			}
			if (this.rotationCounter >= this.rotationDelay)
			{
				this.rotationCounter -= this.rotationDelay;
				base.transform.rotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
			}
		}
		if (this.counter >= this.life)
		{
			this.deathFrameCounter -= deltaTime;
			if (this.deathFrameCounter < 0f)
			{
				this.deathFrameCounter = this.deathFrameRate;
				this.deathFrame++;
				if (this.deathFrame >= this.deathFrames.Length)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
				else
				{
					this.spriteRenderer.sprite = this.deathFrames[this.deathFrame];
				}
			}
		}
	}

	// Token: 0x0400052C RID: 1324
	public float life;

	// Token: 0x0400052D RID: 1325
	public float growTime;

	// Token: 0x0400052E RID: 1326
	public bool flipColors;

	// Token: 0x0400052F RID: 1327
	public float rotationDelay;

	// Token: 0x04000530 RID: 1328
	private float rotationCounter;

	// Token: 0x04000531 RID: 1329
	private float counter;

	// Token: 0x04000532 RID: 1330
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000533 RID: 1331
	public Sprite[] deathFrames;

	// Token: 0x04000534 RID: 1332
	public float deathFrameRate;

	// Token: 0x04000535 RID: 1333
	private int deathFrame = -1;

	// Token: 0x04000536 RID: 1334
	private float deathFrameCounter;

	// Token: 0x04000537 RID: 1335
	public Color[] colors;

	// Token: 0x04000538 RID: 1336
	public float scale;

	// Token: 0x04000539 RID: 1337
	private float colorCounter;

	// Token: 0x0400053A RID: 1338
	private int colorIndex;

	// Token: 0x0400053B RID: 1339
	public float scaleCounter;
}
