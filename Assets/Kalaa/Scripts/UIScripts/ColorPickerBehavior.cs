using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerBehavior : UIButtonBehavior {
	public string targetName;

	Texture2D tex;

	public void Start () {
		base.Start();
		tex = (Texture2D)(GetComponent<Renderer>().sharedMaterial.mainTexture);
		targetName = "shirt";
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
		Vector2 uv = hit.textureCoord;
		Color color = tex.GetPixelBilinear(uv.x, uv.y);
		AvatarTexture.SetColorByName(targetName, color);
	}

	public override void OnDrag (RaycastHit hit) {
		base.OnDrag(hit);
	}

	public override void OnButtonUp (RaycastHit hit) {
		base.OnButtonUp(hit);
	}
}