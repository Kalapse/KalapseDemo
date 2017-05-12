using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class AvatarCreationClickthrough : VRUIBehavior {
	public int numImages;
	public GameObject avatar;

	Material material;
	Material avatarMaterial;
	int currentImage = 8;
	byte[] fileData;
	Texture2D tex = null;

	void Start() {
		base.Start();
		// UnityEngine.VR.VRSettings.showDeviceView = false;

		material = Material.Instantiate(GetComponent<Renderer>().sharedMaterial);
		SetUITexture();
		GetComponent<Renderer>().sharedMaterial = material;
		
		avatarMaterial = Material.Instantiate(avatar.GetComponent<Renderer>().sharedMaterial);
		SetAvatarTexture();
		avatar.GetComponent<Renderer>().sharedMaterial = avatarMaterial;
	}

	public override void OnButtonDown (RaycastHit hit) {
		currentImage++;
		SetUITexture();
		SetAvatarTexture();
		// if (currentImage > numImages) {
		// 	// Application.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
		// } else {
		// }
	}

	// void Update () {
	// 	if (OVRInput.GetDown(OVRInput.Button.One)) {
	// 		currentImage++;
	// 		if (currentImage > numImages) {
	// 			Application.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
	// 		} else {
	// 			SetUITexture(currentImage);
	// 			SetAvatarTexture(currentImage);
	// 		}
	// 	}
	// }

	void SetUITexture() {
		string filePath = string.Format("Assets/Kalaa/UI/Clickthrough/Screen{0}.png", currentImage);
		while (!File.Exists(filePath)) {
			currentImage++;
			// if (currentImage > numImages) {
			// 	Application.LoadLevel(SceneManager.GetActiveScene().buildIndex+1);
			// }
			filePath = string.Format("Assets/Kalaa/UI/Clickthrough/Screen{0}.png", currentImage);
		}
		fileData = File.ReadAllBytes(filePath);
		tex = new Texture2D(2,2);
		tex.LoadImage(fileData);
		material.mainTexture = tex;
	}

	void SetAvatarTexture() {
		string filePath = string.Format("Assets/Kalaa/UI/Clickthrough/Skin{0}.png", currentImage);
		if (File.Exists(filePath)) {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2,2);
			tex.LoadImage(fileData);
			avatarMaterial.mainTexture = tex;
		}
	}
}
