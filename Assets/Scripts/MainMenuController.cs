using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject MainPanel, RankingPanel;

    void Awake()
    {
        MainPanel = GameObject.Find("MainPanel");
        RankingPanel = GameObject.Find("RankingPanel");
        RankingPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenRanking()
    {
        MainPanel.SetActive(false);
        RankingPanel.SetActive(true);
    }

    public void CloseRanking()
    {
        RankingPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
}
