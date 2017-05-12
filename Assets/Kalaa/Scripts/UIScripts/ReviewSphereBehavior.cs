using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReviewSphereBehavior : VRUIBehavior {
	public static ReviewSphereBehavior openSphere;

	public const float lineRadius = 0.01f;
	public const float minimumLength = 0.02f;
	public Texture startImage;

	public GameObject reviewSphereAnchor;
	public GameObject eyeCamera;
	GameObject interactionHand;
	bool beingViewed;
	bool beingHeld;
	Vector3 position;
	int originalCullingMask;

	Material reviewMaterial;

	Vector3 lastDrawPosition;
	bool drawing;

	// Use this for initialization
	void Start () {
		base.Start();

		reviewMaterial = Material.Instantiate(gameObject.GetComponent<Renderer>().sharedMaterial);
		reviewMaterial.mainTexture = startImage;
		gameObject.GetComponent<Renderer>().sharedMaterial = reviewMaterial;

	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
			if (interactionHand != null && !beingViewed) {
				transform.parent.SetParent(interactionHand.transform);
				beingHeld = true;
			}
		}			
		if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
			drawing = false;
			if (beingHeld) {
				transform.parent.SetParent(null);
				beingHeld = false;
			}
		}
		// if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
		// 	if (beingViewed) {
		// 		CloseView();
		// 	} else if (hover) {
		// 		OpenView();
		// 	}
		// }
	}

	public void SetImage(string path) {
		if (File.Exists(path)) {
			byte[] fileData = File.ReadAllBytes(path);
			Texture2D tex = new Texture2D(2,2);
			tex.LoadImage(fileData);
			reviewMaterial.mainTexture = tex;
		}
	}

	public static void Draw(RaycastHit hit) {
		if (openSphere == null) {
			return;
		}
		Vector3 currentPosition = hit.point - 0.05f*hit.normal;
		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
			openSphere.drawing = true;
			print("drawing = true;");
			openSphere.lastDrawPosition = currentPosition;
		} else if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)) {
			openSphere.drawing = false;
			print("drawing = false");
		}

		if (openSphere.drawing) {
			float distance = Vector3.Distance(currentPosition, openSphere.lastDrawPosition);

			if (distance > minimumLength) {
				GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				cylinder.layer = 8;
				cylinder.transform.localScale = new Vector3(lineRadius, distance/2f, lineRadius);
				cylinder.transform.position = (openSphere.lastDrawPosition+currentPosition)/2f;
				cylinder.transform.up = currentPosition - openSphere.lastDrawPosition;
				cylinder.transform.parent = openSphere.transform;

				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.localScale = new Vector3(lineRadius, lineRadius, lineRadius);
				sphere.transform.position = currentPosition;
				sphere.transform.parent = openSphere.transform;
				sphere.layer = 8;

				openSphere.lastDrawPosition = currentPosition;
			}
		}
	}

	public void OpenView() {
		MenuController.SetMenu("ReviewMenu");
		openSphere = this;

		position = transform.parent.position;
		transform.parent.position = eyeCamera.transform.position;
		transform.parent.up = Vector3.up;
		beingViewed = true;
		transform.parent.SetParent(eyeCamera.transform);
		transform.localScale = new Vector3(3f, 3f, 3f);

		Camera cam = eyeCamera.GetComponent<Camera>();
		originalCullingMask = cam.cullingMask;
		cam.cullingMask = (1 << 8) + (1 << 9);
	}

	public void CloseView() {
		openSphere = null;

		Camera cam = eyeCamera.GetComponent<Camera>();
		cam.cullingMask = originalCullingMask;

		for(int i = transform.parent.GetChildCount()-1; i >= 0; i--) {
		   Transform child = transform.parent.GetChild(i).transform;
			if (child.GetComponent<EmojiButtonBehavior>() != null) {
				child.parent = transform;
			}
		}

		transform.parent.SetParent(null);
		beingViewed = false;
		transform.parent.position = position;
		transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
	}

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "InteractionHand") {
        	interactionHand = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "InteractionHand") {
        	interactionHand = null;
        }
    }

	public override void OnButtonUp (RaycastHit hit) {
		OpenView();
	}

	public override void OnEnter (RaycastHit hit) {
		base.OnEnter(hit);
		transform.Find("HoverSphere").gameObject.SetActive(true);
	}

	public override void OnMove (RaycastHit hit) {}

	public override void OnExit (GameObject newTarget) {
		base.OnExit(newTarget);
		transform.Find("HoverSphere").gameObject.SetActive(false);
	}
}

