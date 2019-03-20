using System;
using UnityEngine;
using UnityEngine.UI;
public class SideScorePlum : MonoBehaviour
{
	// Token: 0x0600056E RID: 1390 RVA: 0x0002E556 File Offset: 0x0002C956
	public void SetText(string text, Color color)
	{
		this.text.text = text;
		this.text.color = color;
		this.color = color;
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x0002E577 File Offset: 0x0002C977
	public void SetPoints(int points)
	{
		this.points = points;
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x0002E580 File Offset: 0x0002C980
	private void Start()
	{
		this.canvasTransform.rotation = Quaternion.identity;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x0002E594 File Offset: 0x0002C994
	private void RunText()
	{
		if (this.lifeCounter <= this.animTime)
		{
			this.canvasTransform.localScale = Vector3.Lerp(this.canvasTransform.localScale, Vector3.one, Time.deltaTime * 10f);
			Vector3 vector = this.canvasTransform.localPosition;
			vector = Vector3.Lerp(vector, new Vector3(4f, 0f, 0f), Time.deltaTime * 5f);
			this.canvasTransform.localPosition = vector;
		}
		else
		{
			float num = this.lifeCounter - this.animTime;
			float num2 = num / this.textFadeTime;
			if (num2 < 0.5f)
			{
				this.canvasTransform.localScale = Vector3.Lerp(this.canvasTransform.localScale, Vector3.one, Time.deltaTime * 2f);
			}
			else
			{
				this.canvasTransform.localScale = Vector3.Lerp(this.canvasTransform.localScale, Vector3.one * 1.5f, Time.deltaTime);
			}
			Vector3 vector2 = this.canvasTransform.localPosition;
			vector2 = Vector3.Lerp(vector2, new Vector3(4f, 0f, 0f), Time.deltaTime * 3f);
			this.canvasTransform.localPosition = vector2;
		}
		Color a = this.text.color;
		float a2 = a.a;
		if (Time.time % 0.3f < 0.1f)
		{
			a = Color.Lerp(a, this.color, Time.deltaTime * 25f);
		}
		else if (Time.time % 0.3f < 0.2f)
		{
			a = Color.Lerp(a, Color.white, Time.deltaTime * 25f);
		}
		else
		{
			a = Color.Lerp(a, Color.black, Time.deltaTime * 25f);
		}
		if (this.lifeCounter > this.animTime)
		{
			float num3 = this.lifeCounter - this.animTime;
			float num4 = num3 / this.textFadeTime;
			if (num4 > 0.5f)
			{
				a2 = Mathf.Lerp(1f, 0f, (num4 - 0.5f) * 2f);
			}
			a.a = a2;
		}
		this.text.color = a;
		if (this.lifeCounter > this.animTime + this.textFadeTime)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0002E804 File Offset: 0x0002CC04
	private void Update()
	{
		this.lifeCounter += Time.deltaTime;
		this.frameCounter += Time.deltaTime;
		this.RunText();
		if (this.lifeCounter > this.animTime && this.looping)
		{
			this.looping = false;
			this.frame = -1;
			this.frameCounter = this.frameDelay;
		}
		if (this.frameCounter >= this.frameDelay)
		{
			this.frameCounter -= this.frameDelay;
			this.frame++;
			if (this.looping)
			{
				this.SprayParticles();
				if (this.frame >= this.loop.Length)
				{
					this.frame = 0;
				}
				base.GetComponent<SpriteRenderer>().sprite = this.loop[this.frame];
			}
			else if (this.frame >= this.deathAnim.Length)
			{
				base.GetComponent<SpriteRenderer>().enabled = false;
			}
			else
			{
				this.SprayParticles();
				base.GetComponent<SpriteRenderer>().sprite = this.deathAnim[this.frame];
			}
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0002E930 File Offset: 0x0002CD30
	private void SprayParticles()
	{
		if (this.points > 0)
		{
			EffectsController.SprayParticles(base.transform.position, base.transform.right, 15f + 5f * (float)this.points, this.points, 0.22f, 15f);
		}
	}

	// Token: 0x0400055E RID: 1374
	public float animTime;

	// Token: 0x0400055F RID: 1375
	public float textFadeTime;

	// Token: 0x04000560 RID: 1376
	public Sprite[] loop;

	// Token: 0x04000561 RID: 1377
	public Sprite[] deathAnim;

	// Token: 0x04000562 RID: 1378
	private float lifeCounter;

	// Token: 0x04000563 RID: 1379
	private float frameCounter;

	// Token: 0x04000564 RID: 1380
	public float frameDelay;

	// Token: 0x04000565 RID: 1381
	private int frame = -1;

	// Token: 0x04000566 RID: 1382
	private bool looping = true;

	// Token: 0x04000567 RID: 1383
	public Transform canvasTransform;

	// Token: 0x04000568 RID: 1384
	public Text text;

	// Token: 0x04000569 RID: 1385
	private Color color;

	// Token: 0x0400056A RID: 1386
	private int points;
}
