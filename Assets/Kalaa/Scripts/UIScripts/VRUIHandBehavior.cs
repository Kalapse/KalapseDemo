using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIHandBehavior : MonoBehaviour {
	public static VRUIHandBehavior instance;

	public GameObject laser;
	GameObject target;
	VRUIBehavior targetUIBehavior;
	public static OVRInput.RawButton button;
	float laserLength;

	// Use this for initialization
	void Start () {
		instance = this;
		button = OVRInput.RawButton.RIndexTrigger;
		laserLength = laser.transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		int VRUILayerMask = 1 << 8;

		bool didHitReverse;
		bool didHit = Physics.Raycast(
			transform.position,
			transform.rotation * Vector3.up,
			out hit,
			Mathf.Infinity,
			VRUILayerMask,
			QueryTriggerInteraction.Collide
		);
		if (!didHit) {
			didHitReverse = ReverseRaycast(out hit);

			if (didHitReverse) {
				// print("didHitReverse");
				laser.SetActive(true);
				laser.transform.position = hit.point;
				laser.transform.up = hit.point-transform.position;
				laser.transform.localScale = new Vector3(laser.transform.localScale.x, laserLength, laser.transform.localScale.z);
				ReviewSphereBehavior.Draw(hit);
			}

			if (target != null) {
				targetUIBehavior.OnExit(null);
				target = null;
				targetUIBehavior = null;
			}
			if (!didHitReverse) {
				laser.SetActive(false);
			}
			return;
		}

		// laser.transform.position = (transform.position + hit.point)/2f;
		laser.SetActive(true);
		laser.transform.position = hit.point;
		laser.transform.up = hit.point-transform.position;
		laser.transform.localScale = new Vector3(laser.transform.localScale.x, Mathf.Min(laserLength, hit.distance/2f), laser.transform.localScale.z);

		if (target != null && target != hit.transform.gameObject) {
			targetUIBehavior.OnExit(hit.transform.gameObject);
			target = null;
			targetUIBehavior = null;
		}

		if (target != null && OVRInput.GetDown(button)) {
			targetUIBehavior.OnButtonDown(hit);
		} else if (target != null && OVRInput.GetUp(button)) {
			print("OVRInput.GetUp(button)");
			targetUIBehavior.OnButtonUp(hit);
		} else if (target != null && OVRInput.Get(button)) {
			targetUIBehavior.OnDrag(hit);
		} else {
			if (target == null) {
				targetUIBehavior = hit.transform.gameObject.GetComponent(typeof(VRUIBehavior)) as VRUIBehavior;
				if (targetUIBehavior != null) {
					target = hit.transform.gameObject;
					targetUIBehavior.OnEnter(hit);
				}
			} else if (target == hit.transform.gameObject) {
				targetUIBehavior.OnMove(hit);
			}
		}
	}

	public bool ReverseRaycast(out RaycastHit hit) {
		int VRUILayerMask = 1 << 8;

		Vector3 rayDirection = transform.rotation * Vector3.up;
		Vector3 rayOrigin = transform.position + 10*rayDirection;

		return Physics.Raycast(
			rayOrigin,
			-rayDirection,
			out hit,
			Mathf.Infinity,
			VRUILayerMask,
			QueryTriggerInteraction.Collide
		);
	}
}
