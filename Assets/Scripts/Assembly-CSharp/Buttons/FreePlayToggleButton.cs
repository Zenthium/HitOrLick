using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreePlayToggleButton : Button {

	public ToggleSlider toggleButton;
	public WinsButton winsButton;

	public void Start() {
		base.Start ();
		this.toggleButton.SetState (GameController.isFreePlay);
		if (GameController.isFreePlay) {
			this.winsButton.Enable ();
		} else {
			this.winsButton.Disable ();
		}
	}

	public override void OnClick() {
		GameController.isFreePlay = !GameController.isFreePlay;
		this.toggleButton.ToggleState ();
		if (GameController.isFreePlay) {
			this.winsButton.Enable ();
		} else {
			this.winsButton.Disable ();
		}
	}
}
