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

        target = Globals.GetEnemy();

        effectiveness = 1.0f;
        stacks = inStacks;

        showTooltip = true;

        operation = modifierOperation.OTHER;
        type = modifierType.DEBUFF;
    }

    public override void Modify()
    {
        base.Modify();

        C_FightEnemy enemy = (C_FightEnemy)target;
        enemy.backup.PushForce = enemy.pushForce;
        enemy.pushForce += 1 * stacks;
    }

    public override string GetDescription()
    {
        return description + stacks.ToString();
    }
}
