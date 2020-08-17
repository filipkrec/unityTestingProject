using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_DivineForce : C_Spell
{
    public Sp_DivineForce() : base()
    {
        spellName = "Divine force";
        description = "Stuff"; // duration.ToString(); TODO
        icon = null;

        cooldown = 20.0f;
        effectiveness = 1.0f;
        manaCost = 5;
    }

    public override void Cast()
    {
        if (canUse())
        {
            useResources();

            C_FightPlayer thisTarget = Globals.GetPlayer();

            Mod_DivineForce mod = new Mod_DivineForce(10.0f + durationModifier, effectiveness);

            thisTarget.addModifier(mod);
        }
    }
}
