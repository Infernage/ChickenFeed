using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Text timerText;
    private float timer;
    private bool GameFinished;

    void Awake()
    {
        timerText = this.GetComponent<Text>();
        GameFinished = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (!GameFinished)
        {
            timer += Time.deltaTime;
            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = (timer % 60).ToString("00");
            timerText.text = minutes + " : " + seconds;
        }
    }

    public void StopTimer()
    {
        GameFinished = true;
    }

    public string GetTimerString()
    {
        return this.timerText.text;
    }

    public float GetTimer()
    {
        return this.timer;
    }
}
