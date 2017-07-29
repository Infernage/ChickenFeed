using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour {

    public float FeedLifeTime=10.0f;
    public float RemainingTime;
    public Vector2 Location=new Vector2(0,0);

    public Sprite FeedSprite;   

	// Use this for initialization
	void Start () {
        RemainingTime = FeedLifeTime;
        transform.position = Location;
	}


	
	// Update is called once per frame
	void Update () {
        RemainingTime = RemainingTime - Time.deltaTime;		
	}

    public void KillFeed()
    {
        //GC logic
     
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        ChickenAI ai = other.GetComponentInParent<ChickenAI>();
        ai.FeedSpoted(this);
    }
}
