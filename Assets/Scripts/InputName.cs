using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    private RankingManager rankingManager;
    private GameTimer gameTimer;

    void Awake()
    {
        rankingManager = GameObject.FindGameObjectWithTag("Ranking").GetComponent<RankingManager>();
        gameTimer = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<GameTimer>();
    }

    public void SaveName()
    {
        string name = GetComponentInChildren<Text>().text;
        string timerText = gameTimer.GetTimerString();
        float timer = gameTimer.GetTimer();
        rankingManager.InsertNewScore(name, timerText, timer);
    }
}
