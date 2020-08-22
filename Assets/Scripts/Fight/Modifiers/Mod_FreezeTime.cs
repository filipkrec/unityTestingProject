using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Mod_FreezeTime : C_Modifier
{
    public Mod_FreezeTime(float inDuration) : base()
    {
        modifierName = "Time Freeze";
        description = "Time frozen for ";
        icon = null;
            
        duration = inDuration;

        effectiveness = 1.0f;

        showTooltip = true;

        operation = modifierOperation.OTHER;
        type = modifierType.DEBUFF;

        Initialise();
    }

    public override void Modify()
    {
        Globals.Clash.rateConstant = 0.0f;
        Globals.Clash.IsRateConstant = true;
    }

    public override string GetDescription()
    {
        return description + timer.GetTimeLeftBeforeExecute() + "s";
    }
}
