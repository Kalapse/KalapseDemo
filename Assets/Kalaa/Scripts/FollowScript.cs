using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript : MonoBehaviour {
	public GameObject target;

	public bool followX = true;
	public bool followY = true;
	public bool followZ = true;

	public bool followRoll = true;
	public bool followPitch = true;
	public bool followYaw = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(
			followX ? target.transform.position.x : transform.position.x,
			followY ? target.transform.position.y : transform.position.y,
			followZ ? target.transform.position.z : transform.position.z
		);

		transform.rotation = Quaternion.Euler(
			followPitch ? target.transform.rotation.eulerAngles.x : transform.rotation.eulerAngles.x,
			followYaw ? target.transform.rotation.eulerAngles.y : transform.rotation.eulerAngles.y,
			followRoll ? target.transform.rotation.eulerAngles.z : transform.rotation.eulerAngles.z
		);
	}
}
