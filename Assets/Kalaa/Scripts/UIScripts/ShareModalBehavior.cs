using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareModalBehavior : UIButtonBehavior {
	static ShareModalBehavior instance;

	Transform originalParent;
	Vector3 originalPosition;

	public void Start () {
		base.Start();
		instance = this;
		originalPosition = transform.localPosition;
		originalParent = transform.parent;
	}
	
	void Update () {

	}

	public static void Reset() {
		instance.gameObject.SetActive(false);
		instance.transform.parent = instance.originalParent;
		instance.transform.localPosition = instance.originalPosition;
	}

	public override void OnEnter (RaycastHit hit) {
		// base.OnEnter(hit);
	}

	public override void OnMove (RaycastHit hit) {
		base.OnMove(hit);
	}

	public override void OnExit (GameObject newTarget) {
		// base.OnExit(newTarget);
	}

	public override void OnButtonDown (RaycastHit hit) {
		base.OnEnter(hit);
		// base.OnButtonDown(hit);
	}

	public override void OnDrag (RaycastHit hit) {
		base.OnDrag(hit);
	}

	public override void OnButtonUp (RaycastHit hit) {
		base.OnButtonUp(hit);
	}
}