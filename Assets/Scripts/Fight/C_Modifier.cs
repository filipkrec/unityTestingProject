using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public abstract class C_Modifier
{
    protected bool showTooltip;
    public C_Tooltip tooltip;

    public string modifierName;
    public string description;
    public Sprite icon;

    public float duration; //<= 0 inf
    public float effectiveness;

    public bool stackable;
    public int stacks;

    protected IModifiable target;
    public modifierOperation operation;
    public modifierType type;
    public int ordinal;

    public C_Timer timer;

    public virtual void Modify() 
    {
    }

    public void Remove()
    { 
        target.RemoveModifier(this);
        if(tooltip != null)
            tooltip.Destroy();
    }

    public virtual string GetDescription()
    {
        return description;
    }

    public void Initialise()
    {
        if (duration > 0)
        {
            timer = new C_Timer();
            timer.Instantiate(delegate { Remove(); timer.delete = true; }, duration);
            timer.setPrecision(1);
        }

        if (showTooltip)
        {
            tooltip = new C_Tooltip(this, modifierName, description, icon);
        }
    }

    public bool sortMod(C_Modifier other) //true => prije, false => poslje
    {
        switch(operation)
        {
            case modifierOperation.ADDITION:
                if (other.operation == modifierOperation.MULTIPLICATION) return true;
                if (other.operation == modifierOperation.OTHER) return true;
                break;
            case modifierOperation.MULTIPLICATION:
                if (other.operation == modifierOperation.ADDITION) return false;
                if (other.operation == modifierOperation.OTHER) return true;
                break;
            case modifierOperation.OTHER:
                if (other.operation == modifierOperation.ADDITION) return false;
                if (other.operation == modifierOperation.MULTIPLICATION) return false;
                break;
        }

                return ordinal > other.ordinal;
    }

    public bool ShowsTooltip()
    {
        return showTooltip;
    }

    public void SetTarget(IModifiable mod)
    {
        target = mod;
    }

    public virtual bool ReplaceOnAdd(C_Modifier other) { return true; }
}

public enum modifierType {
    BUFF,
    DEBUFF,
    INDESTRUCTIBLE
}

public enum modifierOperation
{
    ADDITION,
    MULTIPLICATION,
    OTHER
}

/*
 * MOD CONSTRUCTOR EXAMPLE 
        Mod_Modifier(...)
{
        modifierName = "Name";
        description = "Desc";
        icon = Icon;

        duration = inDuration;
        effectiveness = inEffectiveness;
        target = Globals.Player;

        timer = new C_Timer();
        timer.Instantiate(delegate { RemoveSelf(); timer.deleteTimer(); }, duration);

        timer.setPrecision(1);
        lastDescriptionUpdateTime = 0.0f;

        showTooltip = true;

        operation = modifierOperation.ADDITION;
        type = modifierType.DEBUFF;
}
 */
