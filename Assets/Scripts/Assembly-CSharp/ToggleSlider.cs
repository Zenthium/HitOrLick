using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSlider : MonoBehaviour {

	public Sprite offSprite;
	public Sprite onSprite;

	public bool state;

	private SpriteRenderer renderer;

	public void SetState(bool state) {
		this.state = state;
		this.renderer.sprite = this.state ? this.onSprite : this.offSprite;
	}

	public void ToggleState() {
		this.SetState (!this.state);
	}

	// Use this for initialization
	void Start () {
		this.renderer = base.GetComponent<SpriteRenderer> ();
		this.SetState (this.state);
	}
}
