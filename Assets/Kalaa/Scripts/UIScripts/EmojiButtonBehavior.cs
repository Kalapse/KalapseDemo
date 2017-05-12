using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiButtonBehavior : UIButtonBehavior {
	bool held;

	public void Start () {
		base.Start();
	}
	
	void Update () {
		if (held) {
			RaycastHit hit;
			bool didHit = VRUIHandBehavior.instance.ReverseRaycast(out hit);
			if (didHit) {
				print(hit.collider.gameObject.name);
				transform.position = hit.point-0.05f*hit.normal;
				transform.forward = hit.normal;
			}
			if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
				held = false;
				vibrate = true;
				transform.position += 0.01f*transform.forward;
			}
		}
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
		held = true;
		vibrate = false;
		transform.parent = ReviewSphereBehavior.openSphere.transform.parent;
	}

	public override void OnDrag (RaycastHit hit) {
		base.OnDrag(hit);

	}

	public override void OnButtonUp (RaycastHit hit) {
		base.OnButtonUp(hit);
		print("Emoji OnButtonUp");
		held = false;
		vibrate = true;
	}
}