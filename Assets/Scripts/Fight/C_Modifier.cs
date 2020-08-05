using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public abstract class C_Modifier : MonoBehaviour
{
    public string modifierName;
    public string description;
    public Sprite icon;

    public float duration; //-1 inf
    public float effectiveness;
    public int stacks;
    protected Component target;

    protected GameObject modifierIcon;
    public C_Timer timer;

    public virtual void Modify() 
    { 
        GameObject prefab = (GameObject)Resources.Load("ModifierIcon", typeof(GameObject));
        if (prefab != null) 
            modifierIcon = Instantiate(prefab, Globals.canvas.transform);

        if (modifierIcon != null)
        {
            modifierIcon.GetComponent<Image>().sprite = icon;
            foreach (Transform child in modifierIcon.GetComponentsInChildren<Transform>())
            {

                TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();

                if (txt != null)
                    if (txt.name == "Name")
                        txt.SetText(modifierName);
                    else if (txt.name == "Description")
                        txt.SetText(GetDescription());
            }
        }
    }

    protected void RemoveSelf() 
    { 
        if(target is C_Modifiable)
        {
            C_Modifiable currTarget = (C_Modifiable)target;
            currTarget.removeModifier(this);
        }

        if (modifierIcon != null)
            Destroy(modifierIcon);

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

public enum modifierType {
    BUFF,
    DEBUFF,
    INDESTRUCTIBLE
}