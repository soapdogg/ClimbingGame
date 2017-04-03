using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSoundScript : MonoBehaviour {

	void onCollisionStay(Collision col) {
			GetComponent<AudioSource>().Play();﻿
			Debug.Log ("Coin Sound Playing");
	}
}
