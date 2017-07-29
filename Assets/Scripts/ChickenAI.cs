using System;
using UnityEngine;

public class ChickenAI : MonoBehaviour {
    public event EventHandler destroyed;
    public event EventHandler feedSpoted;

    public float speed, feedSpeed;
    public Vector2 min, max;
    public SpriteRenderer sprite; // TODO: Change sprites while moving (?)
    public GameObject featherWhite, featherYellow;

    private Vector2 location;
    private float nextTimeMoving, currentTime, distance;
    private bool isFeed;
    private Pienso mFeed;

	// Use this for initialization
	void Start () {
        nextTimeMoving = currentTime = 0;
        location = transform.position;
        isFeed = false;
	}
	
	// Update is called once per frame
	void Update () {
        distance = Math.Abs(Vector2.Distance(location, transform.position));
        currentTime += Time.deltaTime;

        // Reached position: Calculate next movement
        if (distance < 0.1F && !isFeed)
        {
            Recalculate();
        }
        else if (isFeed && mFeed.RemainingTime <= 0) // Feed consumed
        {
            isFeed = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            Recalculate(true);
        }
        else if (isFeed)
        {
            // TODO: Play feeding animation (?) (use another trigger for range)
        }

        // Time reached: Start moving
        if (nextTimeMoving <= currentTime && distance >= 0.1F && !isFeed)
        {
            Vector2 vDistance = location - new Vector2(transform.position.x, transform.position.y);
            transform.Translate(vDistance.normalized * speed * Time.deltaTime);

            RefreshSprite(vDistance);
        }
	}

    private void FixedUpdate()
    {
        if (nextTimeMoving <= currentTime && distance >= 0.1F && isFeed)
        {
            Vector2 vDistance = location - new Vector2(transform.position.x, transform.position.y);
            Vector2 velocity = vDistance.normalized * feedSpeed;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            RefreshSprite(vDistance);
        }
    }

    private void Recalculate(bool wasFeed = false)
    {
        nextTimeMoving = (wasFeed ? UnityEngine.Random.Range(0, 1) : UnityEngine.Random.Range(0, 10)) + currentTime;
        location.x = UnityEngine.Random.Range(min.x, max.x);
        location.y = UnityEngine.Random.Range(min.y, max.y);

        GetComponent<Animator>().SetBool("Running", false);
    }

    private void RefreshSprite(Vector2 vDistance)
    {
        if (vDistance.x < 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (vDistance.x >= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localScale = Vector3.one;
            }
        }

        GetComponent<Animator>().SetBool("Running", true);
    }

    private void SpawnFeathers(GameObject original)
    {
        GameObject feathers = Instantiate(original, transform.position, Quaternion.identity);
        ParticleSystem ps = feathers.GetComponent<ParticleSystem>();
        float duration = ps.main.duration + ps.startLifetime;
        Destroy(feathers, duration);
    }

    /// <summary>
    /// Triggers the behaviour to reach the feed location
    /// </summary>
    /// <param name="feed">The feed in range</param>
    public void FeedSpoted(Pienso feed)
    {
        feedSpoted?.Invoke(gameObject, new EventArgs());

        mFeed = feed;
        isFeed = true;
        location = feed.Location;
        nextTimeMoving = 0;

        GetComponent<Rigidbody2D>().isKinematic = false;

        // TODO: Play animation and sound
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // TODO: Play animation and sound
            SpawnFeathers(featherWhite);
            SpawnFeathers(featherYellow);

            destroyed?.Invoke(gameObject, new EventArgs());

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Pienso feed = other.gameObject.GetComponent<Pienso>();
        if (feed != null) FeedSpoted(feed);
    }
}
