using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class C_Spell : C_Modifiable, IModifiable
{
    public string spellName;
    public string description;
    public Sprite icon;

    public float cooldown;
    public C_Timer cooldownTimer;

    public float effectiveness;
    public float durationModifier;
    public float channelDuration; // -1 = inf
    public float manaCost; //if -1 manacost/s

    public int numberOfUses; // 0 = inf
    protected int currentUses;

    public bool passive;
    public bool exclusiveChannel;

    public IModifiable target;

    protected C_SpellBackup backup;
    protected bool castInterupted;

    public virtual void Cast(){}

    public bool canUse()
    {
        return checkMana() && checkUses() && checkCooldown();
    }
    protected void useResources()
    {
        Globals.GetPlayer().mana -= (int)manaCost;

        if(numberOfUses != 0)
            currentUses--;

        if (cooldown > 0)
        {
            if (cooldownTimer == null)
            {
                cooldownTimer = new C_Timer();
                cooldownTimer.Instantiate(cooldownTimer.StopTimer, cooldown);
                cooldownTimer.Play();
            }
            else
            {
                cooldownTimer.Play();
            }
        }
    }

    protected bool checkCooldown()
    {
        if (cooldown > 0 && cooldownTimer != null && cooldownTimer.IsRunning())
            return false;

        return true;
    }

    protected bool checkMana()
    {
        if (manaCost > Globals.GetPlayer().mana)
            return false;

        return true;
    }

    protected bool checkUses()
    {
        if (numberOfUses == 0)
            return true;

        if (currentUses == 0)
            return false;

        return true;
    }

    public C_Spell()
    {
        currentUses = numberOfUses;
    }

    public float getCooldownPercentage()
    {
        if (cooldownTimer != null && cooldownTimer.IsRunning())
            return cooldownTimer.GetTimeLeftBeforeExecute() / cooldown;
        else
            return 0f;
    }
    
    public bool isOnCooldown()
    {
        if(cooldownTimer != null)
        return cooldownTimer.IsRunning();

        return false;
    }

    public override void unmodifyValues()
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
    private IModifiable target;

    public string SpellName { get => spellName; set { spellName = value; spellNameModified = true; } }
    public string Description { get => description; set { description = value; descriptionModified = true; } }
    public Sprite Icon { get => icon; set { icon = value; iconModified = true; } }
    public float Cooldown { get => cooldown; set { cooldown = value; cooldownModified = true; } }
    public float Effectiveness { get => effectiveness; set { effectiveness = value; effectivenessModified = true; } }
    public float ChannelDuration { get => channelDuration; set { channelDuration = value; channelDurationModified = true; } }
    public IModifiable Target { get => target; set { target = value; targetModified = true; } }

    public bool SpellNameModified { get => spellNameModified; }
    public bool DescriptionModified { get => descriptionModified;}
    public bool IconModified { get => iconModified; }
    public bool CooldownModified { get => cooldownModified; }
    public bool EffectivenessModified { get => effectivenessModified; }
    public bool ChannelDurationModified { get => channelDurationModified; }
    public bool TargetModified { get => targetModified; }
}