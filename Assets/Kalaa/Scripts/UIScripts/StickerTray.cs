using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerTray : VRUIBehavior {
	static StickerTray instance;

	Transform originalParent;
	Vector3 originalPosition;

	public void Start () {
		base.Start();
		vibrate = false;
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
}