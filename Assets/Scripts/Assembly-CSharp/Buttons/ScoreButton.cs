using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreButton : Button {

	public Text scoreText;

	public void Start() {
		base.Start ();
		this.scoreText.text = GameController.scoreToWin.ToString ();
	}

	public override void OnClick() { }
	public override void OnMouseEnter() { }
	public override void OnMouseExit() { }

	public override void OnRight() {
		GameController.scoreToWin++;
		if (GameController.scoreToWin > 50) {
			GameController.scoreToWin = 50;
		}
		this.scoreText.text = GameController.scoreToWin.ToString ();
	}

	public override void OnLeft() {
		GameController.scoreToWin--;
		if (GameController.scoreToWin < 1) {
			GameController.scoreToWin = 1;
		}
		this.scoreText.text = GameController.scoreToWin.ToString ();
	}
}
