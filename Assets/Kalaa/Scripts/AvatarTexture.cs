using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AvatarTexture : MonoBehaviour {
	static Color beltColor;
	static Color hairColor;
	static Color irisColor;
	static Color pantsColor;
	static Color shirtColor;
	static Color shoeColor;
	static Color skinColor;

	static Texture2D belt;
	static Texture2D hair;
	static Texture2D iris;
	static Texture2D pants;
	static Texture2D shirt;
	static Texture2D shoe;
	static Texture2D skin;

	static Texture2D avatarTexture;

	// Use this for initialization
	void Start () {
		// avatarMaterial = Material.Instantiate(avatar.GetComponent<Renderer>().sharedMaterial);
		// avatar.GetComponent<Renderer>().sharedMaterial = avatarMaterial;

		avatarTexture = (Texture2D)(GetComponent<Renderer>().sharedMaterial.mainTexture);

		// avatarTexture = Texture2D.Instantiate(GetComponent<Renderer>().sharedMaterial.mainTexture);
		// GetComponent<Renderer>().sharedMaterial.mainTexture = avatarTexture;

		belt = LoadTexture("Assets/Kalaa/Avatar/belt_mask.png");
		hair = LoadTexture("Assets/Kalaa/Avatar/hair_mask.png");
		iris = LoadTexture("Assets/Kalaa/Avatar/iris_mask.png");
		pants = LoadTexture("Assets/Kalaa/Avatar/pants_mask.png");
		shirt = LoadTexture("Assets/Kalaa/Avatar/shirt_mask.png");
		shoe = LoadTexture("Assets/Kalaa/Avatar/shoe_mask.png");
		skin = LoadTexture("Assets/Kalaa/Avatar/skin_mask.png");

		SetBeltColor(new Color(1f, 0f, 0f, 1f));
		SetHairColor(new Color(0f, 1f, 0f, 1f));
		SetIrisColor(new Color(0f, 0f, 1f, 1f));
		SetPantsColor(new Color(1f, 1f, 0f, 1f));
		SetShirtColor(new Color(0f, 1f, 1f, 1f));
		SetShoeColor(new Color(1f, 0f, 1f, 1f));
		SetSkinColor(new Color(1f, 1f, 1f, 1f));

		SaveTexture();
	}

	// Update is called once per frame
	void Update () {

	}

	public static void SetColorByName(string name, Color color) {
		if (name == "belt") {
			SetBeltColor(color);
		} else if (name == "hair") {
			SetHairColor(color);
		} else if (name == "iris") {
			SetIrisColor(color);
		} else if (name == "pants") {
			SetPantsColor(color);
		} else if (name == "Shirt") {
			SetShirtColor(color);
		} else if (name == "shoe") {
			SetShoeColor(color);
		} else if (name == "skin") {
			SetSkinColor(color);
		}
	}

	public static void SetBeltColor (Color color) {
		beltColor = color;
		ApplyTexture(belt, beltColor);
	}

	public static void SetIrisColor (Color color) {
		irisColor = color;
		ApplyTexture(iris, irisColor);
	}

	public static void SetHairColor (Color color) {
		hairColor = color;
		ApplyTexture(hair, hairColor);
	}

	public static void SetPantsColor (Color color) {
		pantsColor = color;
		ApplyTexture(pants, pantsColor);
	}

	public static void SetShirtColor (Color color) {
		shirtColor = color;
		ApplyTexture(shirt, shirtColor);
	}

	public static void SetShoeColor (Color color) {
		shoeColor = color;
		ApplyTexture(shoe, shoeColor);
	}

	public static void SetSkinColor (Color color) {
		skinColor = color;
		ApplyTexture(skin, skinColor);
	}

	static void ApplyTexture (Texture2D texture, Color color) {
		for (int i = 0; i < 1024; i++) {
			for (int j = 0; j < 1024; j++) {
				Color pixel = texture.GetPixel(i, j);
				if (pixel != Color.black) {
					avatarTexture.SetPixel(i, j, pixel*color);
				}
			}
		}
		avatarTexture.Apply();
	}


	static void SaveTexture () {
		File.WriteAllBytes("Assets/Kalaa/Avatar/output.png", avatarTexture.EncodeToPNG());
	}

	static Texture2D LoadTexture (string filePath) {
		byte[] fileData = File.ReadAllBytes(filePath);
		Texture2D tex = new Texture2D(2, 2);
		tex.LoadImage(fileData);
		return tex;
	}
}
