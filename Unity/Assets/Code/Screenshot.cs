using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {

	public float Scale = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate() {
		Camera Mcamera = Camera.main;
		if(Input.GetKeyDown(KeyCode.P)) {
			int rw = (int)Mathf.Round(Mcamera.pixelWidth * Scale);
			int rh = (int)Mathf.Round(Mcamera.pixelHeight * Scale);
			RenderTexture rt = new RenderTexture(rw, rh, 24);
			Mcamera.targetTexture = rt;
			Texture2D ss = new Texture2D(rw, rh, TextureFormat.RGB24, false);
			Mcamera.Render();
			RenderTexture.active = rt;
			ss.ReadPixels(new Rect(0, 0, rw, rh), 0, 0);
			Mcamera.targetTexture = null;
			RenderTexture.active = null;
			Destroy(rt);
			byte[] bytes = ss.EncodeToPNG();
			string filename = "screenshot.png";
			System.IO.File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
		}
	}
}
