using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
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
        target = Globals.GetClash();
        timer = gameObject.AddComponent<C_Timer>();
        timer.initiateTimer(RemoveSelf, duration);
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
        thisTarget.backup.RateModifier = thisTarget.rateModifier;
        thisTarget.rateModifier = 0.0f;
    }

    public override string GetDescription()
    {
        return description + timer.GetTimeLeftBeforeExecute() + "s";
    }
}
