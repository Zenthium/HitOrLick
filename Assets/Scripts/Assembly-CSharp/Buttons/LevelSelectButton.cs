using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : Button {

	public Sprite unselectedTexture;
	public Sprite selectedTexture;

	public string scene;

	public void Enable() {
		this.active = true;
	}

	public void Disable() {
		this.active = false;
		this.StopAnimating ();
		this.OnMouseExit ();
	}

	public override void StartAnimating() {
		base.GetComponent<SpriteRenderer> ().sprite = this.selectedTexture;
	}

	public override void StopAnimating() {
		base.GetComponent<SpriteRenderer> ().sprite = this.unselectedTexture;
	}

	public override void OnClick() {
		SceneManager.LoadScene (this.scene);
	}

	public override void OnMouseEnter () {
		this.StartAnimating ();
	}

	public override void OnMouseExit () {
		if (this.buttonController != null && this.buttonController.currentButton != this) {
			this.StopAnimating ();
		}
	}
}
