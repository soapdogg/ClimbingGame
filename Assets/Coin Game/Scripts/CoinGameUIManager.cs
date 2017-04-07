using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinGameUIManager: MonoBehaviour
{

	void Start ()
	{
		GameObject canvasGo = new GameObject ();
		canvasGo.transform.parent = transform;
		canvasGo.name = "canvasGo";
		RectTransform canvasRT = canvasGo.AddComponent<RectTransform> ();
		Canvas canvasCV = canvasGo.AddComponent<Canvas> ();
		canvasCV.renderMode = RenderMode.ScreenSpaceCamera;
//		Vector3 pos = Camera.main.transform.position;
//		pos += Camera.main.transform.forward * 10f;
		canvasCV.worldCamera = Camera.main;

		GameObject buttonGo = new GameObject ();
		RectTransform buttonRT = buttonGo.AddComponent<RectTransform> ();
		buttonRT.SetParent (canvasRT);
		buttonRT.sizeDelta = new Vector2 (200f, 100f);
		Button buttonBu = buttonGo.AddComponent<Button> ();
		buttonBu.gameObject.name = "buttonBu";

		buttonBu.onClick.AddListener (() => {
			Debug.Log ("button clicked");
		});
	}
}


