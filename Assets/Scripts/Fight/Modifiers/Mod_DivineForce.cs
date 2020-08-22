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

        showTooltip = true;

        operation = modifierOperation.ADDITION;
        type = modifierType.DEBUFF;

        duration = inDuration;
        effectiveness = inEffectiveness;

        Initialise();
    }

    public override void Modify()
    {
        Globals.Player.ManaMax -= 3;
        Globals.Player.PushForce += 5 * effectiveness;
    }

    public override string GetDescription()
    {
        return description + timer.GetTimeLeftBeforeExecute() + "s";
    }
}
