using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ModifiableMono : MonoBehaviour
{
    private List<C_Modifier> modifiers = new List<C_Modifier>();
    private int currentOrdinal = 0;

    protected int currentTooltipCount = 0;
    protected Vector2 tooltipStartingPosition;
    protected Vector2 nextTooltipPositionDifference;

    public void addModifier(C_Modifier modifier)
    {
        modifiers.Add(modifier);
        modifier.ordinal = currentOrdinal;

        currentOrdinal++;
        unmodifyValues();
        modifyValues();
    }

    public void removeModifier(C_Modifier modifier)
    {
        modifiers.Remove(modifier);
        unmodifyValues();
        modifyValues();
    }
    public void modifyValues()
    {
        modifiers.Sort((mod1, mod2) => mod1.sortMod(mod2) ? -1 : 1);

        currentTooltipCount = 0;

        foreach (C_Modifier modifier in modifiers)
        {
            modifier.Modify();

            if (modifier.ShowsTooltip())
            {
                //lineup tooltips
                modifier.repositionTooltipIcon(tooltipStartingPosition + nextTooltipPositionDifference * currentTooltipCount);
                currentTooltipCount++;
            }
        }

        foreach (C_Modifier modifier in modifiers)
        {
            if (modifier.ShowsTooltip())
            {
                //recenter tooltips
                modifier.moveTooltipIcon(-nextTooltipPositionDifference / 2 * currentTooltipCount);
            }
        }
    }

    public virtual void unmodifyValues()
    {
    }

    public C_Modifier getModifier<T>()
    {
        return modifiers.Find(x => x is T);
    }
}
