using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haptic : MonoBehaviour {
	static Haptic instance;
	public AudioClip vibeClip;

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void ShortVibe() {
        OVRHaptics.RightChannel.Preempt(new OVRHapticsClip(instance.vibeClip));
	}
}
