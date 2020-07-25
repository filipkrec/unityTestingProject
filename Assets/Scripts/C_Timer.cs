using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class C_Timer : MonoBehaviour
{
    public Action toExecute = delegate { };
    private float timeBeforeExecute; //-1 = manual stop
    private float timeBetweenRepeat;
    private bool executed;
    private int repeats; // -1 = inf
    private int repeatCounter;
    private float currentTimerValue;
    bool running = false;

    public C_Timer(Action inAction, float inTimeBeforeExecute = -1, int inRepeats = 0, float inTimeBetweenRepeat = -1)
    {
        toExecute = inAction;
        if (inTimeBeforeExecute == -1)
        {
            running = true;
        }
        else
        {
            timeBeforeExecute = inTimeBeforeExecute;
            timeBetweenRepeat = inTimeBetweenRepeat;
            repeats = inRepeats;
            currentTimerValue = 0.0f;
            running = true;
            executed = false;
        }
    }

    public void initiateTimer(Action inAction, float inTimeBeforeExecute = -1, int inRepeats = 0, float inTimeBetweenRepeat = -1)
    {
        toExecute = inAction;
        if (inTimeBeforeExecute == -1)
        {
            running = true;
        }
        else
        {
            timeBeforeExecute = inTimeBeforeExecute;
            timeBetweenRepeat = inTimeBetweenRepeat;
            repeats = inRepeats;
            currentTimerValue = 0.0f;
            running = true;
            executed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (running == true)
        {
            currentTimerValue += Time.deltaTime;
            if (timeBeforeExecute != -1)
            {
                if (timeBeforeExecute <= currentTimerValue && !executed)
                {
                    if (timeBeforeExecute == 0)
                    {
                        currentTimerValue = 0.0f;
                    }
                    toExecute();
                    executed = true;
                }
                else if (executed && (repeats > 0 || repeats == -1)
                    && (repeatCounter <= repeats || repeats == -1)
                    && currentTimerValue >= (timeBeforeExecute + (repeatCounter + 1) * timeBetweenRepeat))
                {
                    repeatCounter++;
                    toExecute();
                }

                if (executed && repeatCounter == repeats && repeats != -1)
                    running = false;
            }
        }
    }

    public void StopTimer()
    {
        running = false;
        ResetTimer();
    }

    public void ResetTimer()
    {
        executed = false;
        repeatCounter = 0;
        currentTimerValue = 0.0f;
    }

    public void TogglePause()
    {
        running = !running;
    }

    public void Execute()
    {
        toExecute();
    }

    public float GetCurrentTime()
    {
        return currentTimerValue;
    }

    public int GetRepeatCount()
    {
        return repeatCounter;
    }
}