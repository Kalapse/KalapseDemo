using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {
	static MenuController instance;

	GameObject currentMenu;

	// Use this for initialization
	void Start () {
		instance = this;
		currentMenu = transform.Find("StandardMenu").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void SetMenu(string menuName) {
		GameObject newMenu = instance.transform.Find(menuName).gameObject;
		if (newMenu != null) {
			instance.currentMenu.SetActive(false);
			newMenu.SetActive(true);
			instance.currentMenu = newMenu;
		} else {
			print("Menu '" + menuName + "' not found!");
		}
	}
}
