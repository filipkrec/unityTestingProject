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
        target = Globals.GetClash();
        timer = new C_Timer();
        timer.Instantiate(delegate { RemoveSelf(); timer.delete = true; }, duration);
        timer.setPrecision(1);
        lastDescriptionUpdateTime = 0.0f;

        showTooltip = true;

        operation = modifierOperation.OTHER;
        type = modifierType.DEBUFF;
    }

    public override void Modify()
    {
        base.Modify();

        if(modifierIcon != null)
        foreach (Transform child in modifierIcon.GetComponentsInChildren<Transform>(true))
        {
            TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();

            if (txt != null && txt.name == "Description")
                descriptionText = txt;
        }

        C_Clash thisTarget = (C_Clash)target;
        thisTarget.rateConstant = 0.0f;
        thisTarget.backup.RateConstant = thisTarget.isRateConstant;
        thisTarget.isRateConstant = true;
    }

    public override string GetDescription()
    {
        return description + timer.GetTimeLeftBeforeExecute() + "s";
    }
}
