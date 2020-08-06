using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Modifiable : MonoBehaviour
{
    private List<C_Modifier> modifiers = new List<C_Modifier>();
    private int currentOrdinal = 0;

    public void addModifier(C_Modifier modifier)
    {
        modifiers.Add(modifier);
        modifier.ordinal = currentOrdinal;
        currentOrdinal++;
        unmodifyValues();
    }

    public void removeModifier(C_Modifier modifier)
    {
        modifiers.Remove(modifier);
        unmodifyValues();
        modifyValues();
    }
    private void modifyValues()
    {
        modifiers.Sort((mod1, mod2) => mod1.sortMod(mod2) ? -1 : 1);
        foreach (C_Modifier modifier in modifiers)
            modifier.Modify();
    }

    protected virtual void unmodifyValues()
    {
        
    }
}

