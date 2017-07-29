﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    public void BackToMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Replay()
    {
        SceneManager.LoadScene("Game");
    }
}