using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mod_DivineForce : C_Modifier
{
    public Mod_DivineForce(float inDuration, float inEffectiveness) : base()
    {
        modifierName = "Divine Force";
        description = "Push Force increased by 2, mana pool reduced by 3 for ";
        icon = null;

        duration = inDuration;
        effectiveness = inEffectiveness;
        target = Globals.GetPlayer();

        timer = new C_Timer();
        timer.Instantiate(delegate { RemoveSelf(); timer.delete = true; }, duration);

        timer.setPrecision(1);
        lastDescriptionUpdateTime = 0.0f;

        showTooltip = true;

        operation = modifierOperation.ADDITION;
        type = modifierType.DEBUFF;
    }

    public override void Modify()
    {
        base.Modify();

        if (modifierIcon != null)
            foreach (Transform child in modifierIcon.GetComponentsInChildren<Transform>(true))
            {
                TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();

                if (txt != null && txt.name == "Description")
                    descriptionText = txt;
            }

        Debug.Assert(target is C_FightPlayer);

        C_FightPlayer thisTarget = (C_FightPlayer)target;

        thisTarget.backup.ManaMax = thisTarget.manaMax;
        thisTarget.manaMax -= (int)(3 * effectiveness);

        thisTarget.backup.PushForce = thisTarget.pushForce;
        thisTarget.pushForce += (int)(5 * effectiveness);
    }

    public override string GetDescription()
    {
        return description + timer.GetTimeLeftBeforeExecute() + "s";
    }
}
