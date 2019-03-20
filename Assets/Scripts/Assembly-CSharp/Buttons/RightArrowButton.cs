using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightArrowButton : Button {

	public Button scoreButton;

	//public override void OnMouseEnter() { }
	//public override void OnMouseExit() { }

	public override void OnClick() {
		this.scoreButton.OnRight ();
	}
}
