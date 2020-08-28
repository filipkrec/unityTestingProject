using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_DivineForce : C_InstantSpell
{
    public Sp_DivineForce() : base()
    {
        spellName = "Divine force";
        description = "Push Force increased by 2, mana pool reduced by 3 for 10s"; 
        icon = null;

        cooldown = 20.0f;
        manaCost = 10;
    }

    public override void OnCast()
    {
        Globals.Player.AddModifier(new Mod_DivineForce(durationModified(10f), effectiveness));
    }

    public override void SetDescription()
    {
        description = "Push Force increased by " + 2 * effectiveness + ", mana pool reduced by 3 for" + durationModified(10f) +"s";
    }
}
