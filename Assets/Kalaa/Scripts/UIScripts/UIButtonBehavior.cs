using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonBehavior : VRUIBehavior {
	public Texture initialTexture;
	public Texture hoverTexture;

	protected bool focused = false;
	protected Material material;
	protected MenuButtonBehavior parentMenu;

	public void Start () {
		base.Start();
		material = Material.Instantiate(GetComponent<Renderer>().sharedMaterial);
		GetComponent<Renderer>().sharedMaterial = material;
		material.mainTexture = initialTexture;

		parentMenu = transform.parent.gameObject.GetComponent<MenuButtonBehavior>();
	}
	
	void Update () {

	}

	public override void OnEnter (RaycastHit hit) {
		base.OnEnter(hit);
		focused = true;
		material.mainTexture = hoverTexture;
	}

	public override void OnMove (RaycastHit hit) {
		base.OnMove(hit);
	}

	public override void OnExit (GameObject newTarget) {
		base.OnExit(newTarget);
		focused = false;
		material.mainTexture = initialTexture;

		if (newTarget == null && parentMenu != null) {
			parentMenu.HideMenu();
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