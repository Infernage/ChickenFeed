using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pienso : MonoBehaviour {

    public float FeedLifeTime;
    public float RemainingTime;
    public float TimePercToDissapear; // in decimal f i.e. 0.2f = 20%
    public Vector2 Location=new Vector2(0,0);

    float TimeShrinkStart;
    float TimeShrinkEnd;
    float ShrinkLength;

    Vector3 FinalScale, InitialScale;

    public SpriteRenderer FeedSpriteRender;   

	// Use this for initialization
	public void Start () {
        //FeedLifeTime = 5.0f;
        //TimePercToDissapear = 0.5f;
        RemainingTime = FeedLifeTime;
        transform.position = Location;

        TimeShrinkStart = FeedLifeTime * TimePercToDissapear;

        ShrinkLength = TimeShrinkStart - TimeShrinkEnd;

        FinalScale = new Vector3(0, 0, 0);
        InitialScale = transform.localScale;
    }


	
	// Update is called once per frame
	void Update () {
        RemainingTime = RemainingTime - Time.deltaTime;

        if(RemainingTime <= FeedLifeTime * TimePercToDissapear)
        {
            float ShrinkCovered = (TimeShrinkStart - RemainingTime);
            float ShrinkFraction = ShrinkCovered / ShrinkLength;
            transform.localScale = Vector3.Lerp(InitialScale, FinalScale, ShrinkFraction);
        }
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
