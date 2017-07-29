using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pienso : MonoBehaviour {

    public float FeedLifeTime;
    public float RemainingTime;
    public Vector2 Location=new Vector2(0,0);

    public SpriteRenderer FeedSpriteRender;   

	// Use this for initialization
	public void Start () {
        FeedLifeTime = 5.0f;
        RemainingTime = FeedLifeTime;
        transform.position = Location;

        Debug.Log("Feed start event");
    }


	
	// Update is called once per frame
	void Update () {
        RemainingTime = RemainingTime - Time.deltaTime;
	}

    public void KillFeed()
    {
        //GC logic
        FeedSpriteRender.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        ChickenAI ai = other.GetComponentInParent<ChickenAI>();
        ai.FeedSpoted(this);
    }
}
