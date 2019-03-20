using System;
using UnityEngine;
public class LocalizedShake : MonoBehaviour
{
	// Token: 0x06000565 RID: 1381 RVA: 0x0002E12B File Offset: 0x0002C52B
	private void Start()
	{
		this.lifeLeft = this.life;
		this.material = base.GetComponent<SpriteRenderer>().material;
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x0002E14C File Offset: 0x0002C54C
	private void Update()
	{
		this.counter += Time.deltaTime;
		this.lifeLeft -= Time.deltaTime;
		float num = Mathf.PingPong(this.counter / this.life * 2f, 1f);
		if (this.lifeLeft <= 0f)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		float num2 = this.lifeLeft / this.life;
		this.offset = new Vector2(-num * this.Magnitude.x, num * this.Magnitude.y);
		this.material.SetFloat("_xOffset", this.offset.x);
		this.material.SetFloat("_yOffset", this.offset.y);
	}

	// Token: 0x0400054C RID: 1356
	public Material material;

	// Token: 0x0400054D RID: 1357
	public Vector2 Magnitude;

	// Token: 0x0400054E RID: 1358
	public float life;

	// Token: 0x0400054F RID: 1359
	private float lifeLeft;

	// Token: 0x04000550 RID: 1360
	public float velocity;

	// Token: 0x04000551 RID: 1361
	private Vector2 offset;

	// Token: 0x04000552 RID: 1362
	private float counter;
}
