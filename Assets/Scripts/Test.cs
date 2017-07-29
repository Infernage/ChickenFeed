using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ChickenAI[] chickens;
    private Pienso[] feeds;

    // Use this for initialization
    void Start()
    {
        chickens = new ChickenAI[20];
        for (int i = 0; i < 20; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.Translate(new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f)));
            Rigidbody body = obj.AddComponent<Rigidbody>();
            body.useGravity = false;
            //body.isKinematic = true;
            chickens[i] = obj.AddComponent<ChickenAI>();
            chickens[i].speed = 1;
            chickens[i].feedSpeed = 5;
            chickens[i].min = new Vector2(-5, -5);
            chickens[i].max = new Vector2(5, 5);
        }
        feeds = new Pienso[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
            obj.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            Rigidbody body = obj.AddComponent<Rigidbody>();
            body.useGravity = false;
            body.isKinematic = true;
            SphereCollider col = obj.GetComponent<SphereCollider>();
            col.radius = 5;
            col.isTrigger = true;
            feeds[i] = obj.AddComponent<Pienso>();
            feeds[i].Location = obj.transform.position;
            feeds[i].FeedLifeTime = 200;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
