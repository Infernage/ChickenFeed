using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenGenerator : MonoBehaviour
{
    public int numberOfChickens = 0;
    public float x, y;
    public GameObject ChickenPrefab;

    public GameObject LosePanel;
    private GameObject rankingPanel, inputPanel;

    AudioSource audioSourceDie, audioSourceRush, audioSourceIDLE;
    AudioClip AudioRushing, AudioDying, AudioIDLE;
    List<AudioClip> ListAudiosRushing, ListAudiosDying;

    private List<GameObject> chickens;

    public void Awake()
    {
        rankingPanel = GameObject.Find("Canvas/RankingPanel");
        inputPanel = GameObject.Find("Canvas/PanelInputName");
        rankingPanel.SetActive(false);
        inputPanel.SetActive(false);
    }


    // Use this for initialization
    void Start()
    {
        if (numberOfChickens == 0) numberOfChickens = 20;

        chickens = new List<GameObject>();

        audioSourceDie = GetComponents<AudioSource>()[0];
        audioSourceRush = GetComponents<AudioSource>()[1];
        audioSourceIDLE = GetComponents<AudioSource>()[2];

        ListAudiosRushing = new List<AudioClip>();
        ListAudiosDying = new List<AudioClip>();

        ListAudiosRushing.Add(Resources.Load("Audio/Gallina rushing 1") as AudioClip);
        ListAudiosRushing.Add(Resources.Load("Audio/Gallina rushing 2") as AudioClip);
        ListAudiosRushing.Add(Resources.Load("Audio/Gallina rushing") as AudioClip);
        ListAudiosDying.Add(Resources.Load("Audio/Gallina dying 1") as AudioClip);
        ListAudiosDying.Add(Resources.Load("Audio/Gallina dying 2") as AudioClip);

        AudioIDLE = Resources.Load("Audio/Gallina IDLE") as AudioClip;

        for (int i = 0; i < numberOfChickens; i++)
        {
            GameObject chicken = Instantiate(ChickenPrefab, new Vector3(Random.Range(-x, x), Random.Range(-y, y)), Quaternion.identity);
            ChickenAI ai = chicken.GetComponent<ChickenAI>();
            ai.destroyed += Ai_destroyed;
            ai.feedSpoted += Ai_feedSpoted;
            chickens.Add(chicken);
        }
    }

    private void Ai_feedSpoted(object sender, System.EventArgs e)
    {
        if (!audioSourceRush.isPlaying)
        {
            Debug.Log("PlayRush");
            audioSourceRush.PlayOneShot(ListAudiosRushing[Random.Range(0, 2)], 0.5f);
        }
    }

    private void Ai_destroyed(object sender, System.EventArgs e)
    {
        chickens.Remove(sender as GameObject);

        if (!audioSourceDie.isPlaying)
        {
            Debug.Log("PlayDie");
            audioSourceDie.PlayOneShot(ListAudiosDying[Random.Range(0, 1)], 0.5f);
        }

        if (chickens.Count == 0)
        {
            GameController.GameFinished = true;
            GameObject.Find("Canvas/GameTimer").GetComponent<GameTimer>().StopTimer();
            inputPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSourceIDLE.isPlaying && chickens.Count > 0)
        {
            audioSourceIDLE.PlayOneShot(AudioIDLE, 0.2f);
        }
    }
}
