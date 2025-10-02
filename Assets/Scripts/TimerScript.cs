using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    /// <summary>
    ///                              timerText - where is the text for the timer?
    ///                              dayNightText - where is the text for day/night?
    ///                              cycleTime - how much time you start with.
    ///                              timeRemaining - how much time left.
    ///                              isDay - is it day?
    ///                              onDaySwitch - what script or method will run upon day being struck?
    ///                              onNightSwitch - what script or method will run upon night being struck?
    /// </summary>
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI dayNightText;

    public float cycleTime = 60f;
    private float timeRemaining;
    private bool isDay = false;

    public UnityEvent onDaySwitch;
    public UnityEvent onNightSwitch;


    void Start()
    {
        timeRemaining = cycleTime;
        UpdateText();
    }

    // checks if day/night, then counts up/down respectively, then displays text. when switching, run program.
    void Update()
    {
        if (isDay)
        {
            timeRemaining += Time.deltaTime;

            if (timeRemaining >= cycleTime)
            {
                isDay = false;
                timeRemaining = cycleTime;
                UpdateText();
                onNightSwitch.Invoke();
            }
        }
        else
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                isDay = true;
                timeRemaining = 0;
                UpdateText();
            }

            DisplayTime(timeRemaining);
        }
    }

    // converts time to minute, and second format then displays it.
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("Time Left: {0:00}:{1:00}", minutes, seconds);
    }

    // updates the day/night text.
    void UpdateText()
    {
        dayNightText.text = isDay ? "Day" : "Night";
    }
}
