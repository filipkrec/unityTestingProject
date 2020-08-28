using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_Push : C_InstantSpell
{
    public Sp_Push() : base()
    {
        spellName = "Push!";
        description = "Push for 10";
        icon = null;

        manaCost = 10.0f;
        cooldown = 5.0f;
    }

    public override void OnCast()
    {
        Globals.Clash.Clash += 10 * effectiveness;
    }

    public override void SetDescription()
    {
        description = "Push for " + effectiveness * 10;
    }
}
