using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class C_Spell : C_Modifiable
{
    public string spellName;
    public string description;
    public Sprite icon;

    public float cooldown;
    public float effectiveness;
    public float duration;
    public Component target;

    public float backupCooldown;
    public float backupEffectiveness;
    public float backupDuration;
    public virtual void Cast() { }
    public virtual void Instantiate() { }
    protected override void unmodifyValues()
    {
        cooldown = backupCooldown;
        effectiveness = backupEffectiveness;
        duration = backupDuration;
    }
}
