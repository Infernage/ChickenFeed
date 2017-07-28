using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Text timerText;
    private float timer;

    void Awake()
    {
        timerText = this.GetComponent<Text>();
    }



    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = (timer % 60).ToString("00");
        timerText.text = minutes + " : " + seconds;
    }
}
