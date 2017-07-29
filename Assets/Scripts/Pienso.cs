using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed : MonoBehaviour {

    public float FeedLifeTime=10.0f;
    public float RemainingTime;
    Vector2 Location=new Vector2(0,0);

    public Sprite FeedSprite;   

    public Feed(Vector2 InitialLocation)
    {
        Location = InitialLocation;
        this.transform.position = Location;
        //FeedSprite = something.something;

    }

	// Use this for initialization
	void Start () {
        RemainingTime = FeedLifeTime;

	}


	
	// Update is called once per frame
	void Update () {
        RemainingTime = RemainingTime - Time.deltaTime;		
	}

    public void KillFeed()
    {
        //GC logic
     
    }
}
