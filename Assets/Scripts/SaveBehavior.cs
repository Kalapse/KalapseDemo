using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		System.Net.ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => { return true; };
		SaveLoad.LoadFromWeb(this.transform, this);
	}
	
	// Update is called once per frame
	void Update () {
		// if (OVRInput.GetUp(OVRInput.RawButton.A)) {
		// 	foreach (Transform child in this.transform) Destroy(child.gameObject);
		// 	SaveLoad.Load(this.transform);
		// }

		// if (OVRInput.GetUp(OVRInput.RawButton.B)) {
		// 	SaveLoad.Save(this.transform);
		// }

		// if (OVRInput.GetUp(OVRInput.RawButton.X)) {
		// 	foreach (Transform child in this.transform) Destroy(child.gameObject);
		// }
	}
}
