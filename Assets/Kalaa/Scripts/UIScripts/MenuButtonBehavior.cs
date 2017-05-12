using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonBehavior : UIButtonBehavior {
	public void Start () {
		base.Start();
		HideMenu();
	}
	
	void Update () {

	}

	public void HideMenu() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}
	}

	public void ShowMenu() {
		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
	}

	public override void OnEnter (RaycastHit hit) {
		base.OnEnter(hit);
		ShowMenu();
	}

	public override void OnMove (RaycastHit hit) {
		base.OnMove(hit);
	}

	public override void OnExit (GameObject newTarget) {
		base.OnExit(newTarget);
		if (newTarget == null) {
			HideMenu();
		}
	}

	public override void OnButtonDown (RaycastHit hit) {
		base.OnButtonDown(hit);
	}

	public override void OnDrag (RaycastHit hit) {
		base.OnDrag(hit);
	}

	public override void OnButtonUp (RaycastHit hit) {
		base.OnButtonUp(hit);
	}
}