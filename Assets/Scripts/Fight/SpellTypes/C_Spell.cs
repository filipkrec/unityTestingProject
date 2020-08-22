using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class C_Spell : C_Modifiable, IModifiable
{
    public string spellName;
    public string description;
    public Sprite icon;
    public C_SpellTooltip tooltip;

    public float cooldown;
    public C_Timer cooldownTimer;

    public float effectiveness;
    public float durationModifier;

    public float manaCost; //if -1 manacost/s

    public int numberOfUses; // 0 = inf
    protected int currentUses;

    public bool passive;

    protected C_SpellBackup backup = new C_SpellBackup();

    public virtual void Cast() { }
    public virtual void OnCast() { }

    public virtual void OnSwitch(C_Box newBox = null) { }

    public virtual void OnInsert(C_Box oldBox = null) { }

    public bool canUse()
    {
        return checkMana() && checkUses() && checkCooldown();
    }

    protected virtual void useResources()
    {
        Globals.Player.mana -= (int)manaCost;

        if (numberOfUses != 0)
            currentUses--;
    }

    protected virtual void setCooldown()
    {
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
        if (manaCost > Globals.Player.mana)
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
        if (cooldownTimer != null)
            return cooldownTimer.IsRunning();

        return false;
    }
    public void setTooltip(Button button)
    {
        tooltip = new C_SpellTooltip(spellName, description, button);
    }

    public override void UnmodifyValues()
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

    public string SpellName { get => spellName; set { spellName = value; spellNameModified = true; } }
    public string Description { get => description; set { description = value; descriptionModified = true; } }
    public Sprite Icon { get => icon; set { icon = value; iconModified = true; } }
    public float Cooldown { get => cooldown; set { cooldown = value; cooldownModified = true; } }
    public float Effectiveness { get => effectiveness; set { effectiveness = value; effectivenessModified = true; } }
    public float ChannelDuration { get => channelDuration; set { channelDuration = value; channelDurationModified = true; } }

    public bool SpellNameModified { get => spellNameModified; }
    public bool DescriptionModified { get => descriptionModified;}
    public bool IconModified { get => iconModified; }
    public bool CooldownModified { get => cooldownModified; }
    public bool EffectivenessModified { get => effectivenessModified; }
    public bool ChannelDurationModified { get => channelDurationModified; }
}