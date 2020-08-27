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

    public float manaCost; //if -1 manacost/s
    public float effectiveness { get => 1f + bonus.effectiveness; }
    public float durationModified(float duration) { return duration + duration * bonus.durationModifier; }
    public float manaCostModified { get => manaCost - manaCost * bonus.manaCostReduction; }
    public float cooldownModified { get => cooldown - cooldown * bonus.cooldownReduction; }
    public float numberOfUsesModified { get => bonus.numberOfUses + numberOfUses; }

    public float pushBonus { get => bonus.pushForce; }
    public float rateBonus { get => bonus.rate; }


    public int numberOfUses; // 0 = inf
    protected int currentUses;

    public bool passive;

    protected C_SpellBackup backup = new C_SpellBackup();

    public C_SpellBonus bonus = new C_SpellBonus();

    public virtual void Cast() { }
    public virtual void OnCast() { }

    public virtual void OnSwitch(C_Box newBox = null) { }

    public virtual void OnInsert(C_Box oldBox = null) { }

    public C_Spell()
    {
        currentUses = numberOfUses;
    }

    public bool canUse()
    {
        return checkMana() && checkUses() && checkCooldown();
    }

    protected virtual void useResources()
    {
        Globals.Player.mana -= (int)manaCostModified;

        if (numberOfUsesModified != 0)
            currentUses--;
    }

    protected virtual void setCooldown()
    {
        if (cooldownModified > 0)
        {
            if (cooldownTimer == null)
            {
                cooldownTimer = new C_Timer();
                cooldownTimer.Instantiate(cooldownTimer.StopTimer, cooldownModified);
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
        if (cooldownModified > 0 && cooldownTimer != null && cooldownTimer.IsRunning())
            return false;

        return true;
    }

    protected bool checkMana()
    {
        if (manaCostModified > Globals.Player.mana)
            return false;

        return true;
    }

    protected bool checkUses()
    {
        if (numberOfUses == 0)
            return true;

        if (currentUses == -(bonus.numberOfUses))
            return false;

        return true;
    }

    public float getCooldownPercentage()
    {
        if (cooldownTimer != null && cooldownTimer.IsRunning())
            return cooldownTimer.GetTimeLeftBeforeExecute() / cooldownModified;
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
    }
}

public class C_SpellBackup
{
}

public class C_SpellBonus
{
    public float effectiveness = 0f;
    public float cooldownReduction = 0f;
    public float durationModifier = 0f;
    public float manaCostReduction = 0f;
    public float numberOfUses = 0f;

    public float pushForce = 0f;

    public float rate = 0f;

    public C_SpellBonus()
    {

    }
}