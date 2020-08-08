using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sp_FreezeTime : C_Spell
{
    public Sp_FreezeTime()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for"; // + (duration).ToString(); TODO
        icon = null;

        cooldown = 15.0f;
        effectiveness = 1.0f;
        channelDuration = 0.0f;
        target = Globals.GetClash();

        backup = new C_SpellBackup();
        
    }

    public override void Instantiate()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for"; // duration.ToString(); TODO
        icon = null;

        cooldown = 15.0f;
        effectiveness = 1.0f;
        target = Globals.GetClash();
    }

    public override void Cast()
    {
        if(target is C_Clash)
        {
            C_Clash thisTarget = (C_Clash) target;

            Mod_FreezeTime mod = thisTarget.gameObject.AddComponent<Mod_FreezeTime>();
            mod.Instantiate(5.0f);

            thisTarget.addModifier(mod);
        }
    }
}
