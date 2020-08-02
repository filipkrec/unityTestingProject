using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod_FreezeTime : C_Modifier
{
    public void Instantiate(float inDuration)
    {
        modifierName = "Time Freeze";
        description = "Time frozen for ";
        icon = null;
            
        duration = inDuration;
        effectiveness = 1.0f;
        target = null;
        timer = gameObject.AddComponent<C_Timer>();
        timer.initiateTimer(RemoveSelf, duration);
    }

    public override void Modify()
    {
        if (target is C_FightCalculations)
        {
            C_FightCalculations thisTarget = (C_FightCalculations)target;
            thisTarget.rateModifier = 0.0f;
        }
    }

    public override string GetDescription()
    {
        return description + timer.GetTimeLeftBeforeExecute() + "s";
    }
}
