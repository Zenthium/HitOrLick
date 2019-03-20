using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToggleButton : Button {

	public ToggleSlider toggleButton;

	public void Start() {
		base.Start ();
		this.toggleButton.SetState (GameController.flyEnabled);
	}

	public override void OnClick() {
		GameController.flyEnabled = !GameController.flyEnabled;
		this.toggleButton.ToggleState ();
	}
}
