using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Modifiable : IModifiable
{
    private List<C_Modifier> modifiers = new List<C_Modifier>();
    private int currentOrdinal = 0;

    protected int currentTooltipCount = 0;
    protected Vector2 tooltipStartingPosition;
    protected Vector2 nextTooltipPositionDifference;

    public void AddModifier(C_Modifier modifier)
    {
        modifier.SetTarget(this);
        modifiers.Add(modifier);
        modifier.ordinal = currentOrdinal;

        currentOrdinal++;
        RefreshModifiers();
    }

    public void AddModifier<T>(C_Modifier modifier)
    {
        C_Modifier temp = GetModifier<T>();

        if (temp.ReplaceOnAdd(modifier))
        {
            RemoveModifier(temp);
        }
        else if (temp.stackable)
        {
            temp.stacks += modifier.stacks;
        }

        modifier.SetTarget(this);
        modifiers.Add(modifier);
        modifier.ordinal = currentOrdinal;

        currentOrdinal++;
        RefreshModifiers();
    }

    public void RemoveModifier(C_Modifier modifier)
    {
        modifiers.Remove(modifier);
        RefreshModifiers();
    }
    public void ModifyValues()
    {
        modifiers.Sort((mod1, mod2) => mod1.sortMod(mod2) ? -1 : 1);

        currentTooltipCount = 0;

        foreach (C_Modifier modifier in modifiers)
        {
            modifier.Modify();

            if (modifier.ShowsTooltip() && modifier.tooltip != null)
            {
                //lineup tooltips
                modifier.tooltip.setPosition(tooltipStartingPosition + nextTooltipPositionDifference * currentTooltipCount);
                currentTooltipCount++;
            }
        }

        foreach (C_Modifier modifier in modifiers)
        {
            if (modifier.ShowsTooltip() && modifier.tooltip != null)
            {
                //recenter tooltips
                modifier.tooltip.movePosition(-nextTooltipPositionDifference / 2 * currentTooltipCount);
            }
        }
    }

    public virtual void UnmodifyValues()
    {
    }

    public void RefreshModifiers()
    {
        UnmodifyValues();
        ModifyValues();
    }

    public C_Modifier GetModifier<T>()
    {
        return modifiers.Find(x => x is T);
    }
}

