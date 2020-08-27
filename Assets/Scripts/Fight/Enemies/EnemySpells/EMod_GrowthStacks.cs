using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EMod_GrowthStacks : C_Modifier
{
    public EMod_GrowthStacks(int inStacks = 1) : base()
    {
        modifierName = "Increases push power by 1 per stack";
        description = "STACKS: ";
        icon = null;

        target = Globals.Enemy;

        effectiveness = 1.0f;
        stacks = inStacks;

        showTooltip = true;

        operation = modifierOperation.ADDITION;
        type = modifierType.BUFF;

        Initialise();
    }

    public override void Modify()
    {
        base.Modify();
        Globals.Enemy.PushForce += stacks;
    }

    public override string GetDescription()
    {
        return description + stacks.ToString();
    }
}
