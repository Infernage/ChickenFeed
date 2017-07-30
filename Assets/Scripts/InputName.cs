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
        name = name.Substring(0, 10);
        string timerText = gameTimer.GetTimerString();
        float timer = gameTimer.GetTimer();
        rankingManager.GetComponent<RankingManager>().InsertNewScore(name, timerText, timer);
        rankingManager.SetActive(true);
        GameObject.Find("Canvas/PanelInputName").SetActive(false);
        LosePanel.SetActive(true);
    }
}
