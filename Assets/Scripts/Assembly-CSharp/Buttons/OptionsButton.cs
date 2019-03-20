using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : Button {

	public IntroAnimController animController;
	public ButtonController titleScreenButtonController;
	public ButtonController optionsScreenButtonController;

	public override void OnClick() {
		this.animController.panFrom = Camera.main.transform.position;
		this.animController.panTo = new Vector3 (11, 6, -10);
		this.animController.panTime = 2f;
		this.animController.cameraProgress = 0f;
		this.animController.StartCameraPan ();

		this.titleScreenButtonController.active = false;
		this.optionsScreenButtonController.active = true;
	}
}
