﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_GlobalTimers : MonoBehaviour
{
    private List<C_Timer> timers;
    private List<C_Timer> newTimers;
    private List<C_Timer> deleteTimers;

    public void AddTimer(C_Timer timer)
    {
        newTimers.Add(timer);
    }

    public void RemoveTimer(C_Timer timer)
    {
        timers.Remove(timer);
    }

    private void Awake()
    {
        timers = new List<C_Timer>();
        newTimers = new List<C_Timer>();
        deleteTimers = new List<C_Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.paused) return;

        foreach(C_Timer timer in newTimers)
        {
            timers.Add(timer);
        }

        newTimers.Clear();

        foreach(C_Timer timer in timers)
        {
            timer.Update(Time.smoothDeltaTime);
            if (timer.delete)
                deleteTimers.Add(timer);
        }

        foreach(C_Timer timer in deleteTimers)
            timers.Remove(timer);

        if (deleteTimers.Count != 0)
            deleteTimers.Clear();
    }
}
