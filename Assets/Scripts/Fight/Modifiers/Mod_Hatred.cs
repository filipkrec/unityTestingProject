using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod_Hatred : C_Modifier
{
    public Mod_Hatred(int inStacks = 1) : base()
    {
        modifierName = "Increases push power by 1 per stack";
        description = "STACKS: ";
        icon = null;

        effectiveness = 1.0f;
        stacks = inStacks;

        showTooltip = true;

        operation = modifierOperation.OTHER;
        type = modifierType.DEBUFF;

        Initialise();
    }

    public override void Modify()
    {
        Globals.Player.PushForce += stacks;
    }

    public override string GetDescription()
    {
        return description + stacks.ToString();
    }
}
