using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_DivineForce : C_InstantSpell
{
    public Sp_DivineForce() : base()
    {
        spellName = "Divine force 10MP";
        description = "Push Force increased by 2, mana pool reduced by 3 for 10s"; // duration.ToString(); TODO
        icon = null;

        cooldown = 20.0f;
        effectiveness = 1.0f;
        manaCost = 10;
    }

    public override void OnCast()
    {
        Globals.Player.AddModifier(new Mod_DivineForce(10.0f + durationModifier, effectiveness));
    }
}
