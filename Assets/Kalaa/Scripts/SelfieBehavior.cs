using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SelfieBehavior : MonoBehaviour {
	public GameObject reviewSphere;
	public GameObject reviewSphereAnchor;
	// Material reviewMaterial;
	static bool cameraActive = false;
	bool updatedReviewImage = false;
	string reviewImagePath;
	static SelfieBehavior instance;

	// Use this for initialization
	void Start () {
		instance = this;
		CapturePanorama.CapturePanorama.callback = LoadCapturedImage;

		// reviewMaterial = Material.Instantiate(reviewSphere.GetComponent<Renderer>().sharedMaterial);
		// reviewSphere.GetComponent<Renderer>().sharedMaterial = reviewMaterial;
	}

	// Update is called once per frame
	void Update () {
		if (OVRInput.GetUp(OVRInput.RawButton.RThumbstick)) {
			SetSelfieStickActive(!cameraActive);
		}	
		if (cameraActive) {
			if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
		        reviewSphere.transform.parent.gameObject.SetActive(false);
				CapturePanorama.CapturePanorama.capture = true;
		        // StartCoroutine(ShowImage());
				SetSelfieStickActive(false);
			}
		}
		// if (OVRInput.GetDown(OVRInput.Button.One)) {
		// 	if (imagePopup.active) {
		// 		imagePopup.SetActive(false);
		// 		imagePopup2.SetActive(true);
		// 	} else if (imagePopup2.active) {
		// 		UnityEditor.EditorApplication.isPlaying = false;
		// 	}
		// }
		if (updatedReviewImage) {
			updatedReviewImage = false;
			// if (File.Exists(reviewImagePath)) {
			// 	byte[] fileData = File.ReadAllBytes(reviewImagePath);
			// 	Texture2D tex = new Texture2D(2,2);
			// 	tex.LoadImage(fileData);
			// 	reviewMaterial.mainTexture = tex;
			// }
	        reviewSphere.transform.parent.gameObject.SetActive(true);
			reviewSphere.GetComponent<ReviewSphereBehavior>().SetImage(reviewImagePath);
			reviewSphere.transform.parent.transform.position = reviewSphereAnchor.transform.position;
			reviewSphere.transform.parent.transform.rotation = Quaternion.Euler(
				0f,
				reviewSphereAnchor.transform.rotation.eulerAngles.y,
				0f
			);
		}
	}

	void LoadCapturedImage (string filePath) {
		reviewImagePath = filePath;		
		updatedReviewImage = true;
	}

	public static void Enable() {
		instance.SetSelfieStickActive(true);
	}

	void SetSelfieStickActive(bool b) {
		cameraActive = b;
		transform.GetChild(0).gameObject.SetActive(cameraActive);
		// if (b) {
		// 	SetPanoramaViewActive(false);
		// }
	}

	// void SetPanoramaViewActive(bool b) {
	// 	if (b && !reviewPanoSphere.active) {
	// 		SetSelfieStickActive(false);
	//         reviewPanoSphere.SetActive(true);
	// 		avatar.transform.position += 10*Vector3.up;
	// 		reviewPanoSphere.transform.rotation = Quaternion.Euler(
	// 			0f,
	// 			180f+reviewSphereAnchor.transform.rotation.eulerAngles.y,
	// 			0f
	// 		);
	// 	} else if (!b && reviewPanoSphere.active) {
	//         reviewPanoSphere.SetActive(false);
	// 		avatar.transform.position -= 10*Vector3.up;
	// 	}
	// }
}
