using System;
using UnityEngine;
public class SimpleAnim : MonoBehaviour
{
	public void StartAnimating() {
		this.paused = false;
	}

	public void StopAnimating() {
		this.paused = true;
		base.GetComponent<SpriteRenderer> ().sprite = this.staticSprite;
	}

	private void Start() {
		this.staticSprite = base.GetComponent<SpriteRenderer> ().sprite;
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x0002E9A0 File Offset: 0x0002CDA0
	private void Update()
	{
		if (!this.paused) {
			this.counter += Time.deltaTime;
			if (this.counter > this.animSpeed) {
				this.counter -= this.animSpeed;
				this.frame++;
				base.GetComponent<SpriteRenderer> ().sprite = this.frames [this.frame % this.frames.Length];
				if (this.playOnce && this.frame >= this.frames.Length) {
					UnityEngine.Object.Destroy (base.gameObject);
				}
			}
		}
	}

	// Token: 0x0400056B RID: 1387
	public float animSpeed;

	// Token: 0x0400056C RID: 1388
	public Sprite[] frames;

	// Token: 0x0400056D RID: 1389
	public bool playOnce;

	public bool paused;

	private Sprite staticSprite;

	// Token: 0x0400056E RID: 1390
	private int frame = -1;

	// Token: 0x0400056F RID: 1391
	private float counter;
}
