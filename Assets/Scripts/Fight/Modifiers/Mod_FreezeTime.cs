using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Mod_FreezeTime : C_Modifier
{
    private TextMeshProUGUI descriptionText;
    float lastTime;

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
        timer.setPrecision(1);
        lastTime = 0.0f;

        operation = modifierOperation.OTHER;
        type = modifierType.DEBUFF;
    }

    public void Update()
    {
        if(descriptionText != null && timer.GetCurrentTime() - lastTime > 0.1f)
        {
            descriptionText.text = GetDescription();
            lastTime = timer.GetCurrentTime();
        }
    }

    public override void Modify()
    {
        base.Modify();

        foreach (Transform child in modifierIcon.GetComponentsInChildren<Transform>(true))
        {
            TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();

            if (txt != null && txt.name == "Description")
                descriptionText = txt;
        }

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
