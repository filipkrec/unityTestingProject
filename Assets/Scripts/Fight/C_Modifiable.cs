using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Modifiable : MonoBehaviour
{
    private List<C_Modifier> modifiers = new List<C_Modifier>();

    public void addModifier(C_Modifier modifier)
    {
        modifier.Modify();
        modifiers.Add(modifier);
    }

    public void removeModifier(C_Modifier modifier)
    {
        modifiers.Remove(modifier);
        unmodifyValues();
        modifyValues();
    }
    private void modifyValues()
    {
        foreach (C_Modifier modifier in modifiers)
            modifier.Modify();
    }

    protected virtual void unmodifyValues()
    {
        
    }
}
