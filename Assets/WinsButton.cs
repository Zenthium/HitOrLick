using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinsButton : Button {

	public Text winsText;

	public void Start() {
		base.Start ();
		this.winsText.text = GameController.winsNeeded.ToString ();
	}

	public override void OnClick() { }
	public override void OnMouseEnter() { }
	public override void OnMouseExit() { }

	public override void OnRight() {
		GameController.winsNeeded++;
		if (GameController.winsNeeded > 20) {
			GameController.winsNeeded = 20;
		}
		this.winsText.text = GameController.winsNeeded.ToString ();
	}

	public override void OnLeft() {
		GameController.winsNeeded--;
		if (GameController.winsNeeded < 1) {
			GameController.winsNeeded = 1;
		}
		this.winsText.text = GameController.winsNeeded.ToString ();
	}
}
