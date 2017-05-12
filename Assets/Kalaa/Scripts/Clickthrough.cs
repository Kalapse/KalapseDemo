using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clickthrough : MonoBehaviour {
	public int numScenes = 4;

	void Start() {
		// UnityEngine.VR.VRSettings.showDeviceView = false;
	}

	// Update is called once per frame
	void Update () {
		if (OVRInput.GetDown(OVRInput.Button.One)) {
			int nextScene = SceneManager.GetActiveScene().buildIndex+1;
			if (nextScene == numScenes) {
				// UnityEditor.EditorApplication.isPlaying = false;
			} else {
				Application.LoadLevel(nextScene);
			}
		}
		if (OVRInput.GetUp(OVRInput.RawButton.LThumbstick)) {
			UnityEngine.VR.VRSettings.showDeviceView = !UnityEngine.VR.VRSettings.showDeviceView;
		}
	}
}
