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
    public float manaCost; //if inf manacost/s
    public float channelDuration; // -1 = inf
    public int numberOfUses; // -1 = inf

    public bool passive;
    public bool exclusiveChannel;

    public Component target;

    protected C_SpellBackup backup;

    public virtual void Cast() { }
    public virtual void Instantiate() { }
    protected override void unmodifyValues()
    {
        if (backup.SpellNameModified)
            spellName = backup.SpellName;
        if (backup.DescriptionModified)
            description = backup.Description;
        if (backup.IconModified)
            icon = backup.Icon;
        if (backup.CooldownModified)
            cooldown = backup.Cooldown;
        if (backup.EffectivenessModified)
            effectiveness = backup.Effectiveness;
        if (backup.ChannelDurationModified)
            channelDuration = backup.ChannelDuration;
        if (backup.TargetModified)
            target = backup.Target;
    }
}

public class C_SpellBackup
{
    bool spellNameModified = false;
    private string spellName;
    bool descriptionModified = false;
    private string description;
    bool iconModified = false;
    private Sprite icon;
    bool cooldownModified = false;
    private float cooldown;
    bool effectivenessModified = false;
    private float effectiveness;
    bool channelDurationModified = false;
    private float channelDuration;
    bool targetModified = false;
    private Component target;

    public string SpellName { get => spellName; set { spellName = value; spellNameModified = true; } }
    public string Description { get => description; set { description = value; descriptionModified = true; } }
    public Sprite Icon { get => icon; set { icon = value; iconModified = true; } }
    public float Cooldown { get => cooldown; set { cooldown = value; cooldownModified = true; } }
    public float Effectiveness { get => effectiveness; set { effectiveness = value; effectivenessModified = true; } }
    public float ChannelDuration { get => channelDuration; set { channelDuration = value; channelDurationModified = true; } }
    public Component Target { get => target; set { target = value; targetModified = true; } }

    public bool SpellNameModified { get => spellNameModified; }
    public bool DescriptionModified { get => descriptionModified;}
    public bool IconModified { get => iconModified; }
    public bool CooldownModified { get => cooldownModified; }
    public bool EffectivenessModified { get => effectivenessModified; }
    public bool ChannelDurationModified { get => channelDurationModified; }
    public bool TargetModified { get => targetModified; }
}