using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod_Hatred : C_Modifier
{
    public Mod_Hatred(float effectivenessIn = 0f) : base()
    {
        modifierName = "Increases push power by 1 per stack";
        description = "STACKS: ";
        icon = null;

        effectiveness = effectivenessIn;
        stacks = 1;

        showTooltip = true;

        operation = modifierOperation.ADDITION;
        type = modifierType.BUFF;

        Initialise();
    }

    public override void Modify()
    {
        Globals.Player.PushForce += stacks * effectiveness;
    }

    public override string GetDescription()
    {
        return description + stacks.ToString();
    }
}
