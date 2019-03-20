using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

	private Vector3 regularSize;
	private Vector3 hoverSize;

	public float scaleFactor = 1.2f;
	public bool active = true;

	private SpriteRenderer renderer;

	public ButtonController buttonController;

	public void Enable() {
		this.active = true;
		this.renderer.material.SetColor("_Color", Color.white);
	}

	public void Disable() {
		this.active = false;
		//this.StopAnimating ();
		this.renderer.material.SetColor("_Color", Color.gray);
		this.OnMouseExit ();
		if (this.buttonController.currentButton == this) {
			this.buttonController.OnUp ();
		}
	}

	public virtual void StartAnimating() {
		base.GetComponent<SimpleAnim> ().StartAnimating ();
	}

	public virtual void StopAnimating() {
		base.GetComponent<SimpleAnim> ().StopAnimating ();
	}

	// Use this for initialization
	public void Start () {
		this.regularSize = this.transform.localScale;
		this.hoverSize = this.regularSize * scaleFactor;
		this.renderer = base.GetComponent<SpriteRenderer> ();
	}

	public virtual void OnClick() { }

	void OnMouseUp () {
		if (this.active) {
			this.OnClick ();
		}
	}
	
	public virtual void OnRight () { }
	public virtual void OnLeft () { }
	public virtual void OnUp () { }
	public virtual void OnDown () { }

	public virtual void OnMouseEnter () {
		this.transform.localScale = this.hoverSize;
	}

	public virtual void OnMouseExit () {
		this.transform.localScale = this.regularSize;
	}
}
