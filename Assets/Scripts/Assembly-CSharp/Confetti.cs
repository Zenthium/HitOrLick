using System;
using UnityEngine;
public class Confetti : MonoBehaviour
{
	// Token: 0x06000557 RID: 1367 RVA: 0x0002DB76 File Offset: 0x0002BF76
	private void Start()
	{
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0002DB78 File Offset: 0x0002BF78
	private void Update()
	{
		base.transform.position += this.velocity * Time.deltaTime;
		this.life += Time.deltaTime;
		this.velocity += this.accel * Time.deltaTime;
		if (this.life > 10f && !base.GetComponent<SpriteRenderer>().isVisible)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000529 RID: 1321
	public Vector3 velocity;

	// Token: 0x0400052A RID: 1322
	public Vector3 accel;

	// Token: 0x0400052B RID: 1323
	private float life;
}
