using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sp_FreezeTime : C_Spell
{
    float freezeDuration = 5.0f;

    Sp_FreezeTime()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for" + (freezeDuration * durationModifier).ToString();
        icon = null;

        cooldown = 15.0f;
        effectiveness = 1.0f;
        durationModifier = 1.0f;
        target = null;

        backupCooldown = 15.0f;
        backupEffectiveness = 1.0f;
    }

    public void Instantiate()
    {
        spellName = "Freeze Time";
        description = "Time is frozen for" + (freezeDuration * durationModifier).ToString();
        icon = null;

        cooldown = 15.0f;
        effectiveness = 1.0f;
        durationModifier = 1.0f;
        target = null;

        backupCooldown = 15.0f;
        backupEffectiveness = 1.0f;
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
            mod.Instantiate(durationModifier);
            mod.SetTarget(target);

            thisTarget.addModifier(mod);
        }
    }
}
