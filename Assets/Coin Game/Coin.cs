using System;
using UnityEngine;

public class Coin{

    public GameObject coinObject;

    public Coin(int num, float x, float y)
    {
        coinObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        coinObject.GetComponent<Renderer>().material.color = new Color(219, 235, 0);
        coinObject.name = "Coin_" + num;
        coinObject.transform.position = new Vector3(x, y, 0);
        //coinObject.AddComponent<BoxCollider>();
        Rigidbody body = coinObject.AddComponent<Rigidbody>();
        body.useGravity = false;
		Debug.Log(coinObject.name + " created");
    }
}

