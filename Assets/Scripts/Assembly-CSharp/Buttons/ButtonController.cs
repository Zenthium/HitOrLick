using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FreeLives;

public class ButtonController : MonoBehaviour {

	public Button[] buttons;
	public Button currentButton;

	public bool horizontalEnabled = true;
	public bool verticalEnabled = true;

	public bool wrap = true;

	public bool active;
	private bool wasActive;

	private InputState input;

	// Use this for initialization
	void Start () {
		this.input = new InputState();
		if (this.currentButton == null) {
			this.currentButton = this.buttons[0];
		}
		this.currentButton.StartAnimating ();
	}

	public void OnUp() {
		Button[] sorted = new Button[this.buttons.Length];
		Array.Copy (this.buttons, sorted, this.buttons.Length);
		Array.Sort(sorted, (a, b) => {
			var pos = a.transform.position.y.CompareTo(b.transform.position.y);
			return pos != 0 ? pos : System.Math.Abs(a.transform.position.x
				- this.currentButton.transform.position.x)
					.CompareTo(System.Math.Abs(b.transform.position.x
						- this.currentButton.transform.position.x));
		});
		sorted = Array.FindAll (sorted, b => b.active);
		foreach (var button in sorted) {
			if (button.transform.position.y > this.currentButton.transform.position.y) {
				this.SelectButton (button);
				return;
			}
		}
		if (this.wrap) {
			this.SelectButton (sorted [0]);
		}
	}

	public void OnDown() {
		Button[] sorted = new Button[this.buttons.Length];
		Array.Copy (this.buttons, sorted, this.buttons.Length);
		Array.Sort(sorted, (a, b) => {
			var pos = b.transform.position.y.CompareTo(a.transform.position.y);
			return pos != 0 ? pos : System.Math.Abs(a.transform.position.x
				- this.currentButton.transform.position.x)
					.CompareTo(System.Math.Abs(b.transform.position.x
						- this.currentButton.transform.position.x));
		});
		sorted = Array.FindAll (sorted, b => b.active);
		foreach (var button in sorted) {
			if (button.transform.position.y < this.currentButton.transform.position.y) {
				this.SelectButton (button);
				return;
			}
		}
		if (this.wrap) {
			this.SelectButton (sorted [0]);
		}
	}

	public void OnRight() {
		Button[] sorted = new Button[this.buttons.Length];
		Array.Copy (this.buttons, sorted, this.buttons.Length);
		Array.Sort(sorted, (a, b) => {
			var pos = a.transform.position.x.CompareTo(b.transform.position.x);
			return pos != 0 ? pos : System.Math.Abs(a.transform.position.y
				- this.currentButton.transform.position.y)
					.CompareTo(System.Math.Abs(b.transform.position.y
						- this.currentButton.transform.position.y));
		});
		sorted = Array.FindAll (sorted, b => b.active);
		foreach (var button in sorted) {
			if (button.transform.position.x > this.currentButton.transform.position.x) {
				this.SelectButton (button);
				return;
			}
		}
		if (this.wrap) {
			this.SelectButton (sorted [0]);
		}
	}

	public void OnLeft() {
		Button[] sorted = new Button[this.buttons.Length];
		Array.Copy (this.buttons, sorted, this.buttons.Length);
		Array.Sort(sorted, (a, b) => {
			var pos = b.transform.position.x.CompareTo(a.transform.position.x);
			return pos != 0 ? pos : System.Math.Abs(a.transform.position.y
				- this.currentButton.transform.position.y)
					.CompareTo(System.Math.Abs(b.transform.position.y
						- this.currentButton.transform.position.y));
		});
		sorted = Array.FindAll (sorted, b => b.active);
		foreach (var button in sorted) {
			if (button.transform.position.x < this.currentButton.transform.position.x) {
				this.SelectButton (button);
				return;
			}
		}
		if (this.wrap) {
			this.SelectButton (sorted [0]);
		}
	}

	void SelectButton(Button button) {
		this.currentButton.StopAnimating ();
		this.currentButton = button;
		this.currentButton.StartAnimating ();
	}
	
	// Update is called once per frame
	void Update () {
		InputReader.GetInput (this.input);

		if (this.active && this.wasActive) {
			
			if (this.input.down && !this.input.wasDown) {
				if (this.verticalEnabled) {
					this.OnDown ();
				} else {
					this.currentButton.OnDown ();
				}
			} else if (this.input.up && !this.input.wasUp) {
				if (this.verticalEnabled) {
					this.OnUp ();
				} else {
					this.currentButton.OnUp ();
				}
			}

			if (this.input.right && !this.input.wasRight) {
				if (this.horizontalEnabled) {
					this.OnRight ();
				} else {
					this.currentButton.OnRight ();
				}
			} else if (this.input.left && !this.input.wasLeft) {
				if (this.horizontalEnabled) {
					this.OnLeft ();
				} else {
					this.currentButton.OnLeft ();
				}
			}

			if (this.input.start && !this.input.wasStart) {
				this.currentButton.OnClick ();
			}
		}

		this.wasActive = this.active;
	}
}
