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
    private int precision;
    bool running;
    bool started;

    public C_Timer(Action inAction, float inTimeBeforeExecute = -1, int inRepeats = 0, float inTimeBetweenRepeat = -1)
    {
        toExecute = inAction;
        timeBeforeExecute = inTimeBeforeExecute;
        timeBetweenRepeat = inTimeBetweenRepeat;
        repeats = inRepeats;
        currentTimerValue = 0.0f;
        precision = 6;
        running = true;
        executed = false;
        started = false;
    }

    public void initiateTimer(Action inAction, float inTimeBeforeExecute = -1, int inRepeats = 0, float inTimeBetweenRepeat = -1)
    {
        toExecute = inAction;
        timeBeforeExecute = inTimeBeforeExecute;
        timeBetweenRepeat = inTimeBetweenRepeat;
        repeats = inRepeats;
        currentTimerValue = 0.0f;
        repeatCounter = 0;
        precision = 6;
        running = true;
        executed = false;
        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            if (!started)
            {
                started = true;
                repeatCounter = 0;
            }

            currentTimerValue += Time.deltaTime;
            if ((int)timeBeforeExecute != -1)
            {
                if ((GetCurrentTime() >= timeBeforeExecute || timeBeforeExecute == 0)
                    && !executed)
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
                    && GetCurrentTime() >= (timeBeforeExecute + (repeatCounter + 1) * timeBetweenRepeat))
                {
                    repeatCounter++;
                    toExecute();
                }

                if (executed && running && repeatCounter == repeats && repeats != -1)
                    StopTimer();
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
        started = false;
        currentTimerValue = 0.0f;
    }

    public void Play()
    {
        running = true;
    }

    public void Pause()
    {
        running = false;
    }

    public void Execute()
    {
        toExecute();
    }

    public float GetCurrentTime()
    {
        return (float)Math.Round(currentTimerValue,precision);
    }

    public int GetRepeatCount()
    {
        return repeatCounter;
    }

    public void setPrecision(int inPrecision)
    {
        precision = inPrecision;
    }
}