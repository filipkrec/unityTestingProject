using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class C_TimerMono : C_ModifiableMono
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

    bool sequenceTimer;
    int currentSequenceStep;
    List<KeyValuePair<Action, float>> actionSequence;
    float currentSequenceTime;

    C_TimerBackup backup;

    /// <summary>
    ///  -1 Time before execute = infinity ;
    ///  -1 repeats = infinite
    /// </summary>
    /// <param name="inAction"></param>
    /// <param name="inTimeBeforeExecute"></param>
    /// <param name="inRepeats"></param>
    /// <param name="inTimeBetweenRepeat"></param>
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
        sequenceTimer = false;
    }

    public void initiateTimer(List<KeyValuePair<Action, float>> inActionSequence)
    {
        actionSequence = inActionSequence;
        sequenceTimer = true;
        currentSequenceStep = 0;
    }

    public void Update()
    {
        if (running)
        {
            if (!started)
            {
                started = true;
                repeatCounter = 0;
            }

            currentTimerValue += Time.deltaTime;

            if (sequenceTimer)
                SequenceTick();
            else
                RegularTick();
        }
    }

    void RegularTick()
    {
        if ((int)timeBeforeExecute != -1)
        {
            if ((GetCurrentTime() >= timeBeforeExecute || timeBeforeExecute == 0)
                && !executed)
            {
                if (timeBeforeExecute == 0)
                {
                    currentTimerValue = 0.0f;
                }
                executed = true;
                toExecute();
            }
            else if (
                executed
                && (repeats > 0 || repeats == -1)
                && (repeatCounter <= repeats || repeats == -1)
                && (GetCurrentTime() >= (timeBeforeExecute + (repeatCounter + 1) * timeBetweenRepeat) || timeBetweenRepeat == 0)
                )
            {
                repeatCounter++;
                toExecute();
            }

            if (executed && running && repeatCounter == repeats && repeats != -1)
                StopTimer();
        }
    }

    void SequenceTick()
    {
        currentSequenceTime += Time.deltaTime;

        if (actionSequence[currentSequenceStep].Value <= currentSequenceTime)
        {
            actionSequence[currentSequenceStep].Key();
            currentSequenceStep++;
            if (currentSequenceStep + 1 == actionSequence.Count)
            {
                currentSequenceTime = 0f;
                repeatCounter++;
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
        currentTimerValue = 0f;
        currentSequenceTime = 0f;
        currentSequenceStep = 0;
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

    public void setPrecision(int inPrecision)
    {
        precision = inPrecision;
    }

    public float GetCurrentTime()
    {
        return (float)Math.Round(currentTimerValue, precision);
    }

    public float GetTimeLeftBeforeExecute()
    {
        if (timeBeforeExecute > 0 && timeBeforeExecute > currentTimerValue)
        {
            return (float)Math.Round(timeBeforeExecute - currentTimerValue, precision);
        }
        else return 0f;
    }

    public int GetRepeatCount()
    {
        return repeatCounter;
    }

    public bool IsRunning()
    {
        return running;
    }

}