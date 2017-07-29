using System;
using UnityEngine;

public class ChickenAI : MonoBehaviour {
    public float speed, feedSpeed;
    public Vector2 min, max;
    public Sprite sprite; // TODO: Change sprites while moving (?)

    private Vector2 location;
    private float nextTimeMoving, currentTime;
    private bool isFeed;
    private Feed mFeed;

	// Use this for initialization
	void Start () {
        nextTimeMoving = currentTime = 0;
        location = transform.position;
        isFeed = false;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Math.Abs(Vector2.Distance(location, transform.position));
        currentTime += Time.deltaTime;

        // Reached position: Calculate next movement
        if (distance < 0.1F && !isFeed)
        {
            nextTimeMoving = UnityEngine.Random.Range(0, 10) + currentTime;
            location.x = UnityEngine.Random.Range(min.x, max.x);
            location.y = UnityEngine.Random.Range(min.y, max.y);
        }
        else if (isFeed && mFeed.RemainingTime <= 0) // Feed consumed
        {
            isFeed = false;
            nextTimeMoving = 0;
        }
        else if (isFeed)
        {
            // TODO: Play feeding animation (?)
        }

        // Time reached: Start moving
        if (nextTimeMoving <= currentTime && distance >= 0.1F)
        {
            transform.Translate((location - new Vector2(transform.position.x, transform.position.y)).normalized
                                * speed * Time.deltaTime);
        }
        //Debug.Log("Distance: " + distance + ", target: " + location + ", location: " + transform.position + ", to move: " + nextTimeMoving + ", current time:" + currentTime);
	}

    /// <summary>
    /// Triggers the behaviour to reach the feed location
    /// </summary>
    /// <param name="feed">The feed in range</param>
    public void FeedSpoted(Feed feed)
    {
        mFeed = feed;
        isFeed = true;
        location = feed.Location;
        nextTimeMoving = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("JSDHASJDHSA");
    }
}
