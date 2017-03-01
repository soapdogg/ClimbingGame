using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision of this: " + this.name + " other: " + other.name);
        CoinGameScript coinScript = CoinGameScript.GetCoinGameScript();
        coinScript.RemoveCoin(other.gameObject);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
