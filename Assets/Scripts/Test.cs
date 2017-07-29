using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    private ChickenAI[] chickens;
    private Pienso[] feeds;

	// Use this for initialization
	void Start () {
        chickens = new ChickenAI[200];
        for (int i = 0; i < 200; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.GetComponent<BoxCollider>().enabled = false;
            obj.transform.Translate(Vector3.zero);
            Rigidbody body = obj.AddComponent<Rigidbody>();
            body.useGravity = false;
            body.isKinematic = true;
            chickens[i] = obj.AddComponent<ChickenAI>();
            chickens[i].speed = 1;
            chickens[i].min = new Vector2(-5, -5);
            chickens[i].max = new Vector2(5, 5);
        }
        feeds = new Pienso[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
            obj.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            SphereCollider col = obj.GetComponent<SphereCollider>();
            col.radius = 5;
            col.isTrigger = true;
            feeds[i] = obj.AddComponent<Pienso>();
            feeds[i].Location = obj.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
