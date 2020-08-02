using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sp_FreezeTime : C_Spell
{

    public Sp_FreezeTime()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for" + (duration).ToString();
        icon = null;

        cooldown = 15.0f;
        effectiveness = 1.0f;
        duration = 5.0f;
        target = null;

        backupCooldown = 15.0f;
        backupEffectiveness = 1.0f;
    }

    public override void Instantiate()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for" + duration.ToString();
        icon = null;

        cooldown = 15.0f;
        effectiveness = 1.0f;
        duration = 5.0f;
        target = null; //set in awake

        backupCooldown = 15.0f;
        backupEffectiveness = 1.0f;
        backupDuration = 5.0f;
    }

    private void Awake()
    {
        GameObject targetGO = GameObject.FindWithTag("FightActors");
        if (targetGO != null)
        {
            target = targetGO.GetComponent<C_FightCalculations>();
        }
    }

    public override void Cast()
    {
        if(target is C_FightCalculations)
        {
            C_FightCalculations thisTarget = (C_FightCalculations)target;

            Mod_FreezeTime mod = thisTarget.gameObject.AddComponent<Mod_FreezeTime>();
            mod.Instantiate(duration);
            mod.SetTarget(target);

            thisTarget.addModifier(mod);
        }
    }
}
