﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Source: https://www.youtube.com/watch?v=x-C95TuQtf0
public class GameTimer : MonoBehaviour
{
    public Text timeText;
    public int minutesMax;
    private float startTime;
    private float timeRemaining;
    public bool timeOver { get; set; }
    public float timeRemainingTotal { get; set; }
    void Start()
    {
        timeOver = false;
        startTime = Time.time + minutesMax * 60;
        timeRemainingTotal = 0;
    }

    void Update()
    {
        if (!timeOver) {
            timeRemaining = startTime - Time.time;
            int minutes = (int) timeRemaining  / 60;
            int seconds = (int) timeRemaining  % 60;
            timeText.text = minutes.ToString() + ":" + seconds.ToString("f0");

            if (timeRemaining < 0) {
                TimeOver();
            }
        }
    }

    public void ReStart() {
        startTime = Time.time + minutesMax * 60;
        timeRemainingTotal += timeRemaining;
    }

    void TimeOver() {
        timeOver = true;
        timeText.text = "00:00";
    }
}