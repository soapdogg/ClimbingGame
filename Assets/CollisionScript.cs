using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object: " + this.name + " collided with: " + other.name);
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
