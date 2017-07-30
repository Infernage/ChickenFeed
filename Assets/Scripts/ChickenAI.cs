using System;
using UnityEngine;

public class ChickenAI : MonoBehaviour {
    public event EventHandler destroyed;
    public event EventHandler feedSpoted;

    public float speed, feedSpeed;
    public Vector2 min, max;
    public GameObject featherWhite, featherYellow;

    private Vector2 location;
    private float nextTimeMoving, currentTime, distance;
    private bool isFeed, feedReached;
    private Pienso mFeed;
    private Animator animator;
    private float initialXScale;
    private int nFeedsNear;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nFeedsNear = 0;
    }

    // Use this for initialization
    void Start () {
        initialXScale = transform.localScale.x;
        nextTimeMoving = currentTime = 0;
        location = transform.position;
        isFeed = feedReached = false;
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
            isFeed = feedReached = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            Recalculate(true);
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
        // Feed spotted: Run for your food!
        if (nextTimeMoving <= currentTime && distance >= 0.1F && isFeed && !feedReached)
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
        
        animator.SetBool("Running", false);
        animator.SetBool("Eat", false);
    }

    private void RefreshSprite(Vector2 vDistance)
    {
        transform.localScale =
            new Vector3(
                (vDistance.x < 0 ? -initialXScale : initialXScale),
                initialXScale,
                initialXScale);

        animator.SetBool("Running", true);
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
    private void FeedSpoted(Pienso feed)
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
            // TODO: Play sound
            SpawnFeathers(featherWhite);
            SpawnFeathers(featherYellow);

            destroyed?.Invoke(gameObject, new EventArgs());

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Feed"))
        {
            feedReached = true;
            animator.SetBool("Eat", true);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            nFeedsNear++;
        }
        else
        {
            Pienso feed = other.gameObject.GetComponent<Pienso>();
            if (feed != null) FeedSpoted(feed);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Feed"))
        {
            nFeedsNear--;
            if (nFeedsNear == 0)
            {
                feedReached = false;
                animator.SetBool("Eat", false);
            }
        }
    }
}

