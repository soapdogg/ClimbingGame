using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CountDownScript : MonoBehaviour {

	public float timeRemaining = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
	}

	void OnGUI(){
		if (timeRemaining > 0) {
			GUI.Label (new Rect (100, 100, 200, 100), "Time: " + timeRemaining);

		} else {
			SceneManager.LoadScene("CoinGame");
		}



	}
}
