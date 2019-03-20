using System;
using FreeLives;
using UnityEngine;
public class RandomLightFlicker : MonoBehaviour
{
	// Token: 0x0600056B RID: 1387 RVA: 0x0002E319 File Offset: 0x0002C719
	private void Start()
	{
		this.sr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0002E328 File Offset: 0x0002C728
	private void Update()
	{
		this.explosionDelayLeft -= Time.deltaTime;
		if (this.explosionDelayLeft < 0f)
		{
			this.explosionDelayLeft = UnityEngine.Random.Range(this.delay.x, this.delay.y);
			this.intensity = UnityEngine.Random.Range(0.3f, 1.3f);
			Color color = this.sr.color;
			color.a = 1f + this.intensity;
			this.sr.color = color;
			EffectsController.ShakeCamera(UnityEngine.Random.insideUnitCircle, UnityEngine.Random.value * 0.2f + this.intensity);
			SoundController.PlaySoundEffect("BackgroundExplosion", 0.5f);
			float num = UnityEngine.Random.Range(-21f, -9f);
			if (UnityEngine.Random.value < 0.5f)
			{
				num = -num;
			}
			if (this.intensity > 1f)
			{
				ParticleSystem particleSystem = UnityEngine.Object.Instantiate<ParticleSystem>(this.dustParticlePrefab, new Vector3(num, 10.1f, 0f), this.dustParticlePrefab.transform.rotation);
				UnityEngine.Object.Destroy(particleSystem.gameObject, 15f);
			}
		}
		else
		{
			this.flickerDelayLeft -= Time.deltaTime;
			if (this.flickerDelayLeft < 0f)
			{
				this.flickerOn = !this.flickerOn;
				this.flickerDelayLeft = UnityEngine.Random.Range(0.01f, 0.03f);
			}
			this.intensity = Mathf.MoveTowards(this.intensity, 0.2f, Time.deltaTime);
			Color color2 = this.sr.color;
			if (this.flickerOn && this.intensity > 0.5f)
			{
				color2.a = this.intensity + 0.2f;
			}
			else if (this.intensity > 0.2f)
			{
				color2.a = UnityEngine.Random.Range(0.2f, 0.5f + this.intensity);
			}
			else
			{
				color2.a = 0.2f;
			}
			this.sr.color = color2;
		}
	}

	// Token: 0x04000557 RID: 1367
	public Vector2 delay;

	// Token: 0x04000558 RID: 1368
	private float explosionDelayLeft;

	// Token: 0x04000559 RID: 1369
	private float flickerDelayLeft;

	// Token: 0x0400055A RID: 1370
	private bool flickerOn;

	// Token: 0x0400055B RID: 1371
	public ParticleSystem dustParticlePrefab;

	// Token: 0x0400055C RID: 1372
	private SpriteRenderer sr;

	// Token: 0x0400055D RID: 1373
	private float intensity;
}
