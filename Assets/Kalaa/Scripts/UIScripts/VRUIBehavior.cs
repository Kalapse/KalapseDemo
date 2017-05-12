using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRUIBehavior : MonoBehaviour {
	public bool hover;
	public bool vibrate = true;

	// Use this for initialization
	public void Start () {
		gameObject.layer = 8;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public virtual void OnEnter (RaycastHit hit) {
		hover = true;
		if (vibrate) {
			Haptic.ShortVibe();
		}
	}

	public virtual void OnMove (RaycastHit hit) {}

	public virtual void OnExit (GameObject newTarget) {
		hover = false;
	}

	public virtual void OnButtonDown (RaycastHit hit) {}

	public virtual void OnDrag (RaycastHit hit) {}

	public virtual void OnButtonUp (RaycastHit hit) {}
}