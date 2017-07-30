using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    public GameObject rankingManager, LosePanel;
    private GameTimer gameTimer;

    void Awake()
    {
        gameTimer = GameObject.FindGameObjectWithTag("GameTimer").GetComponent<GameTimer>();
    }

    public void SaveName()
    {
        string name = GetComponentInChildren<Text>().text;
        if (name.Length > 10)
        {
            name = name.Substring(0, 10);
        }
        string timerText = gameTimer.GetTimerString();
        float timer = gameTimer.GetTimer();
        rankingManager.GetComponent<RankingManager>().InsertNewScore(name, timerText, timer);
        GameObject.Find("Canvas/PanelInputName").SetActive(false);
        rankingManager.SetActive(true);
        LosePanel.SetActive(true);
        rankingManager.GetComponent<RankingManager>().LoadRankingUI();
    }
}
