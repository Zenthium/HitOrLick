using System;
using UnityEngine;
public class TrailFader : MonoBehaviour
{
	// Token: 0x06000577 RID: 1399 RVA: 0x0002EA4E File Offset: 0x0002CE4E
	private void Start()
	{
		this.scaleM = this.lifeM;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0002EA5C File Offset: 0x0002CE5C
	private void LateUpdate()
	{
		if (this.copySpriteRenderer != null)
		{
			this.spriteRenderer.sprite = this.copySpriteRenderer.sprite;
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0002EA88 File Offset: 0x0002CE88
	private void Update()
	{
		this.counter += Time.deltaTime;
		Color color = this.spriteRenderer.color;
		color.a = 1f - this.counter / this.life;
		this.spriteRenderer.color = color;
		float num = this.scaleM * (1f - this.counter / (this.life * this.lifeM));
		if (this.grow)
		{
			num = this.scaleM * (1f + this.counter / (this.life * this.lifeM));
		}
		base.transform.localScale = Vector3.one * num;
		if (num < 1f && !this.grow)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y, base.transform.position.z + ((!this.grow) ? (-Time.deltaTime * 0.1f) : (Time.deltaTime * 0.1f)));
		if (this.counter > this.life * this.lifeM)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000570 RID: 1392
	public SpriteRenderer spriteRenderer;

	// Token: 0x04000571 RID: 1393
	public float life;

	// Token: 0x04000572 RID: 1394
	public float counter;

	// Token: 0x04000573 RID: 1395
	public float lifeM = 1f;

	// Token: 0x04000574 RID: 1396
	public SpriteRenderer copySpriteRenderer;

	// Token: 0x04000575 RID: 1397
	public bool grow;

	// Token: 0x04000576 RID: 1398
	private float scaleM;

	// Token: 0x04000577 RID: 1399
	private float scaleXSign;
}
