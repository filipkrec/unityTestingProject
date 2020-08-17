using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sp_FreezeTime : C_Spell
{
    public Sp_FreezeTime() : base()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for 5s"; 
        icon = null;

        manaCost = 5.0f;
        cooldown = 15.0f;
        effectiveness = 1.0f;
    }

    public override void Cast()
    {
        if (canUse())
        {
            useResources();

            C_Clash thisTarget = Globals.GetClash();

            Mod_FreezeTime mod = new Mod_FreezeTime(5.0f);

            thisTarget.addModifier(mod);
        }
    }
}
