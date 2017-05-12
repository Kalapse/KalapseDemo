using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBehavior : MonoBehaviour {
	public GameObject cameraModel;

	private Vector3 lastLeftPosition;
	private Vector3 lastRightPosition;

	public const float lineRadius = 0.01f;
	public const float minimumLength = 0.02f;

	bool cameraActive = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.GetUp(OVRInput.RawButton.RThumbstick)) {
			cameraActive = !cameraActive;
			cameraModel.SetActive(cameraActive);
		}
		if (cameraActive) {
			if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
				CapturePanorama.CapturePanorama.capture = true;
			}
			return;
		}

		if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)) {
			lastRightPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.parent = this.transform;
				sphere.transform.localScale = new Vector3(lineRadius, lineRadius, lineRadius);
				sphere.transform.position = lastRightPosition;

		} else if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) {
			Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);

			float distance = Vector3.Distance(controllerPosition, lastRightPosition);

			if (distance > minimumLength) {
				GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				cylinder.transform.parent = this.transform;
				cylinder.transform.localScale = new Vector3(lineRadius, distance/2f, lineRadius);
				cylinder.transform.position = (lastRightPosition+controllerPosition)/2f;
				cylinder.transform.up = controllerPosition - lastRightPosition;

				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.parent = this.transform;
				sphere.transform.localScale = new Vector3(lineRadius, lineRadius, lineRadius);
				sphere.transform.position = controllerPosition;

				lastRightPosition = controllerPosition;
			}
		}

		if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger)) {
			lastLeftPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.parent = this.transform;
				sphere.transform.localScale = new Vector3(lineRadius, lineRadius, lineRadius);
				sphere.transform.position = lastLeftPosition;

		} else if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger)) {
			Vector3 controllerPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);

			float distance = Vector3.Distance(controllerPosition, lastLeftPosition);

			if (distance > minimumLength) {
				GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
				cylinder.transform.parent = this.transform;
				cylinder.transform.localScale = new Vector3(lineRadius, distance/2f, lineRadius);
				cylinder.transform.position = (lastLeftPosition+controllerPosition)/2f;
				cylinder.transform.up = controllerPosition - lastLeftPosition;

				GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.transform.parent = this.transform;
				sphere.transform.localScale = new Vector3(lineRadius, lineRadius, lineRadius);
				sphere.transform.position = controllerPosition;

				lastLeftPosition = controllerPosition;
			}
		}
	}
}
