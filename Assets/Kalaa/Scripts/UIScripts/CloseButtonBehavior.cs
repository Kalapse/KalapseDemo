using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonBehavior : UIButtonBehavior {
	public void Start () {
		base.Start();
	}
	
	void Update () {

	}

	public override void OnEnter (RaycastHit hit) {
		base.OnEnter(hit);
	}

	public override void OnMove (RaycastHit hit) {
		base.OnMove(hit);
	}

	public override void OnExit (GameObject newTarget) {
		base.OnExit(newTarget);
	}

	public override void OnButtonDown (RaycastHit hit) {
		base.OnButtonDown(hit);
	}

	public override void OnDrag (RaycastHit hit) {
		base.OnDrag(hit);
	}

	public override void OnButtonUp (RaycastHit hit) {
		base.OnButtonUp(hit);
		ReviewSphereBehavior.openSphere.CloseView();
		print("MenuController.SetMenu(StandardMenu);");
		MenuController.SetMenu("StandardMenu");
		StickerTray.Reset();
		ShareModalBehavior.Reset();
	}
}