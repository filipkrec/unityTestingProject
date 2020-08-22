using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sp_FreezeTime : C_InstantSpell
{
    public Sp_FreezeTime() : base()
    {
        spellName = "Freeze Time 5MP";
        description = "Time is frozen for 5s"; 
        icon = null;

        manaCost = 5.0f;
        cooldown = 15.0f;
        effectiveness = 1.0f;
    }

    public override void OnCast()
    {
        Globals.Clash.AddModifier(new Mod_FreezeTime(5.0f * effectiveness + durationModifier));
    }
}
