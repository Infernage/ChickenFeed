using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaceFeedWithMouse : MonoBehaviour
{

    public float surfaceOffset = 1.5f;

    public PiensoManager feedManager;
    public GameObject[] feedsUI;

    Texture2D HandClosed;
    Texture2D HandOpen;

    public AudioClip AudioDropFeed;
    AudioSource audioSource;
    bool AudioFlag;


    // Use this for initialization
    void Start()
    {

        feedManager = gameObject.AddComponent<PiensoManager>();
        feedManager.FeedKilled += FeedManager_FeedKilled;

        HandClosed = Resources.Load("Sprites/Hand_Closed") as Texture2D;
        HandOpen = Resources.Load("Sprites/Hand_Open") as Texture2D;

        AudioDropFeed = Resources.Load("Audio/Click soltar") as AudioClip;
        audioSource = GetComponent<AudioSource>();

        AudioFlag = false;

    }

    private void FeedManager_FeedKilled(object sender, FeedKillEventArgs e)
    {
        feedsUI[e.FeedAmount].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.GameFinished)
        {
            foreach (var item in feedsUI)
            {
                item.SetActive(false);
            }
        }
        else {

            if (!Input.GetMouseButton(0))
            {
                Cursor.SetCursor(HandClosed, Vector2.zero, CursorMode.Auto);
                AudioFlag = false;
                return;
            }
            else
            {
                Cursor.SetCursor(HandOpen, Vector2.zero, CursorMode.Auto);

                if (!AudioFlag)
                {
                    audioSource.PlayOneShot(AudioDropFeed, 0.7F);

                    AudioFlag = true;
                }

            }

            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == null)
            {
                return;
            }

            if (feedManager.AddPienso(hit.point + hit.normal) != null)
            {
                feedsUI[feedManager.FeedAmount - 1].SetActive(false);
            }
        }
    }
}
