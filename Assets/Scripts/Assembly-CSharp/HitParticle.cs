using System;
using UnityEngine;
public class HitParticle : MonoBehaviour
{
	// Token: 0x0600055E RID: 1374 RVA: 0x0002DE82 File Offset: 0x0002C282
	public void SetColors(Color[] colors)
	{
		this.colors = colors;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0002DE8B File Offset: 0x0002C28B
	private void Start()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.colorCounter = UnityEngine.Random.Range(0f, this.colorChangeDelay);
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0002DEB0 File Offset: 0x0002C2B0
	private void Update()
	{
		float deltaTime = Time.deltaTime;
		this.counter += deltaTime;
		this.colorCounter += deltaTime;
		float t = this.counter / this.life;
		base.transform.localScale = Vector3.one * Mathf.Lerp(this.startSize, this.endSize, t);
		if (this.colorCounter >= this.colorChangeDelay)
		{
			this.colorCounter -= this.colorChangeDelay;
			this.colorIndex++;
			this.spriteRenderer.color = this.colors[this.colorIndex % this.colors.Length];
		}
		if (this.alphaOut && this.counter > this.life * 0.75f)
		{
			float num = (this.counter - this.life * 0.75f) / this.life * 0.25f;
			Color color = this.spriteRenderer.color;
			color.a = 1f - num;
			this.spriteRenderer.color = color;
		}
		base.transform.position += (Vector3) this.velocity * deltaTime;
		if (this.counter > this.life)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x0400053C RID: 1340
	public float life;

	// Token: 0x0400053D RID: 1341
	public float colorChangeDelay;

	// Token: 0x0400053E RID: 1342
	public float startSize;

	// Token: 0x0400053F RID: 1343
	public float endSize;

	// Token: 0x04000540 RID: 1344
	private float counter;

	// Token: 0x04000541 RID: 1345
	private float colorCounter;

	// Token: 0x04000542 RID: 1346
	public Vector2 velocity;

	// Token: 0x04000543 RID: 1347
	public bool alphaOut;

	// Token: 0x04000544 RID: 1348
	private SpriteRenderer spriteRenderer;

	// Token: 0x04000545 RID: 1349
	public Color[] colors;

	// Token: 0x04000546 RID: 1350
	private int colorIndex;
}
