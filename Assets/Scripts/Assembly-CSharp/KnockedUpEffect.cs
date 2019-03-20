using System;
using UnityEngine;
public class KnockedUpEffect : MonoBehaviour
{
	// Token: 0x06000562 RID: 1378 RVA: 0x0002E024 File Offset: 0x0002C424
	private void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.rotSpeed *= UnityEngine.Random.Range(-2f, 2f);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0002E050 File Offset: 0x0002C450
	private void Update()
	{
		this.fallDelay -= Time.deltaTime;
		if (this.fallDelay > 0f)
		{
			return;
		}
		if (!this.haveStartedSound)
		{
			this.haveStartedSound = true;
			base.GetComponent<AudioSource>().Play();
		}
		Vector3 position = base.transform.position;
		position.y -= this.fallSpeed * Time.deltaTime;
		base.transform.position = position;
		base.transform.Rotate(Vector3.forward, this.rotSpeed * Time.deltaTime);
		if (base.transform.position.y < 0f && !this.spriteRenderer.isVisible)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x04000547 RID: 1351
	public float rotSpeed;

	// Token: 0x04000548 RID: 1352
	public float fallSpeed;

	// Token: 0x04000549 RID: 1353
	public SpriteRenderer spriteRenderer;

	// Token: 0x0400054A RID: 1354
	public float fallDelay;

	// Token: 0x0400054B RID: 1355
	private bool haveStartedSound;
}
