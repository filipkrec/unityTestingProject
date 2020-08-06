using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class C_Timer : C_Modifiable
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
                toExecute();
                executed = true;
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
            if(currentSequenceStep + 1 == actionSequence.Count)
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
        return (float)Math.Round(currentTimerValue,precision);
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

}


class C_TimerBackup
{
    private bool toExecuteModified = false;
    private Action toExecute;

    private bool timeBeforeExecuteModified = false;
    private float timeBeforeExecute; //-1 = manual stop

    private bool timeBetweenRepeatModified = false;
    private float timeBetweenRepeat;

    private bool executedModified = false;
    private bool executed;

    private bool repeatsModified = false;
    private int repeats; // -1 = inf

    private bool repeatCounterModified = false;
    private int repeatCounter;

    private bool currentTimerValueModified = false;
    private float currentTimerValue;

    private bool precisionModified = false;
    private int precision;

    private bool runningModified = false;
    bool running;

    private bool startedModified = false;
    bool started;

    bool actionSequenceModified = false;
    List<KeyValuePair<Action, float>> actionSequence;

    bool currentSequenceTimeModified = false;
    float currentSequenceTime;

    public bool ToExecuteModified { get => toExecuteModified;}
    public Action ToExecute { get => toExecute; set { toExecute = value; toExecuteModified = true; } }
    public bool TimeBeforeExecuteModified { get => timeBeforeExecuteModified;}
    public float TimeBeforeExecute { get => timeBeforeExecute; set { timeBeforeExecute = value; timeBeforeExecuteModified = true; } }
    public bool TimeBetweenRepeatModified { get => timeBetweenRepeatModified;}
    public float TimeBetweenRepeat { get => timeBetweenRepeat; set { timeBetweenRepeat = value; timeBetweenRepeatModified = true; } }
    public bool ExecutedModified { get => executedModified;}
    public bool Executed { get => executed; set { executed = value; executedModified = true; } }
    public bool RepeatsModified { get => repeatsModified;}
    public int Repeats { get => repeats; set { repeats = value; repeatsModified = true; } }
    public bool RepeatCounterModified { get => repeatCounterModified;}
    public int RepeatCounter { get => repeatCounter; set { repeatCounter = value; repeatCounterModified = true; } }
    public bool CurrentTimerValueModified { get => currentTimerValueModified;}
    public float CurrentTimerValue { get => currentTimerValue; set { currentTimerValue = value; currentTimerValueModified = true; } }
    public bool PrecisionModified { get => precisionModified;}
    public int Precision { get => precision; set { precision = value; precisionModified = true; } }
    public bool RunningModified { get => runningModified;}
    public bool Running { get => running; set { running = value; runningModified = true; } }
    public bool StartedModified { get => startedModified;}
    public bool Started { get => started; set { started = value; startedModified = true; } }
    public bool ActionSequenceModified { get => actionSequenceModified; }
    public List<KeyValuePair<Action, float>> ActionSequence { get => actionSequence; set { actionSequence = value; actionSequenceModified = true; } }
    public bool CurrentSequenceTimeModified { get => currentSequenceTimeModified; }
    public float CurrentSequenceTime { get => currentSequenceTime; set { currentSequenceTime = value; currentSequenceTimeModified = true; } }
}