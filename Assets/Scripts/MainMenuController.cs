using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private GameObject Panel, MainPanel, RankingPanel, CreditsPanel;

    void Awake()
    {
        MainPanel = GameObject.Find("MainPanel");
        RankingPanel = GameObject.Find("RankingPanel");
        CreditsPanel = GameObject.Find("CreditsPanel");
        Panel = GameObject.Find("Panel");
        CreditsPanel.SetActive(false);
        RankingPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowCredits()
    {
        Panel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
        Panel.SetActive(true);
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
