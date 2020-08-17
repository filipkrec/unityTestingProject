using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_Push : C_Spell
{
    public Sp_Push() : base()
    {
        spellName = "Push!";
        description = "Push for " + effectiveness * 10;
        icon = null;

        manaCost = 10.0f;
        cooldown = 5.0f;
        effectiveness = 1.0f;
    }

    public override void Cast()
    {
        if (canUse())
        {
            useResources();

            C_Clash thisTarget = Globals.GetClash();

            thisTarget.clash += 10 * effectiveness;
        }
    }
}
