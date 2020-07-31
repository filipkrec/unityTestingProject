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
    public float durationModifier;
    public Component target;

    public float backupCooldown;
    public float backupEffectiveness;
    public virtual void Cast() { }
    protected override void unmodifyValues()
    {
        cooldown = backupCooldown;
        effectiveness = backupEffectiveness;
    }
}
