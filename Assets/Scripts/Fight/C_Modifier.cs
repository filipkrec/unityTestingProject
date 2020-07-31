using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public abstract class C_Modifier : MonoBehaviour
{
    public string modifierName;
    public string description;
    public Sprite icon;

    public float duration;
    public float effectiveness;
    protected Component target;

    public C_Timer timer;

    public virtual void Modify() { }
    protected void RemoveSelf() 
    { 
        if(target is C_Modifiable)
        {
            C_Modifiable currTarget = (C_Modifiable)target;
            currTarget.removeModifier(this);
        }

    }

    public void SetTarget(Component inTarget)
    {
        target = inTarget;
    }

    public virtual string GetDescription()
    {
        return description;
    }
}
