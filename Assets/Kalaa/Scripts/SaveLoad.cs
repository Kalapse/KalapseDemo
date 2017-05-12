using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using UnityEngine.Networking;

public class SaveLoad {
	public static long lastTicks = 0;

	public static void Save(Transform parent) {
		List<SerializableObject> objects = new List<SerializableObject>();

		int i = 0;
		lastTicks = System.DateTime.Now.Ticks;

		foreach (Transform child in parent) {
			PrintTime(""+i);
			i++;

			SerializableObject so = new SerializableObject();
			so.name = child.name;
			so.position = child.transform.position;
			so.scale = child.transform.localScale;
			so.rotation = child.transform.localRotation;

			objects.Add(so);
		}

		PrintTime("bf");
		BinaryFormatter bf = new BinaryFormatter ();
		PrintTime("file");
		FileStream file = File.Create (Application.persistentDataPath + "/drawing.dat");
		PrintTime("serialize");
		bf.Serialize(file, objects);
		PrintTime("close");
		file.Close();
	}

	public static void PrintTime(string s) {
		Debug.Log(s);
		Debug.Log(System.DateTime.Now.Ticks - lastTicks);
		lastTicks = System.DateTime.Now.Ticks;
	}

	public static void Load(Transform parent) {
		if(File.Exists(Application.persistentDataPath + "/drawing.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/drawing.dat", FileMode.Open);
			List<SerializableObject> objects = (List<SerializableObject>)bf.Deserialize(file);
			file.Close();

			PopulateObjects(parent, objects);
		}
	}

	public static void LoadFromWeb(Transform parent, MonoBehaviour mb) {
		string fileName = Application.persistentDataPath + "/drawingFromWeb.dat";

		// WebClient client = new WebClient();
		// client.DownloadFile(fileUrl, fileName);

		// BinaryFormatter bf = new BinaryFormatter();
		// FileStream file = File.Open(fileName, FileMode.Open);
		// List<SerializableObject> objects = (List<SerializableObject>)bf.Deserialize(file);

		// PopulateObjects(parent, objects);

		mb.StartCoroutine(WaitForRequest(parent));
	}

	public static void PopulateObjects(Transform parent, List<SerializableObject> objects) {
		foreach (SerializableObject so in objects) {
			GameObject go;
			if (so.name == "Sphere") {
				go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			} else {
				go = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
			}

			go.transform.parent = parent;
			go.transform.localScale = so.scale;
			go.transform.position = so.position;
			go.transform.rotation = so.rotation;
		}
	}

	static IEnumerator WaitForRequest(Transform parent)
	{
		string fileUrl = "https://dl.dropboxusercontent.com/s/mw9k59stiukowqe/drawingFromWeb?dl=1";

		using (UnityWebRequest request = UnityWebRequest.Get(fileUrl))
		{
			yield return request.Send();

			if (request.isError) // Error
			{
				Debug.Log(request.error);
			}
			else // Success
			{
				BinaryFormatter bf = new BinaryFormatter();
				MemoryStream stream = new MemoryStream(request.downloadHandler.data);
				List<SerializableObject> objects = (List<SerializableObject>)bf.Deserialize(stream);

				PopulateObjects(parent, objects);
			}
		}
		// yield return www;

		// // check for errors
		// if (www.error == null) {
		// 	Debug.Log("WWW Ok!");

		// 	BinaryFormatter bf = new BinaryFormatter();
		// 	MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(www.data));
		// 	List<SerializableObject> objects = (List<SerializableObject>)bf.Deserialize(stream);

		// 	PopulateObjects(parent, objects);
		// } else {
		// 	Debug.Log("WWW Error: "+ www.error);
		// }
	}
}