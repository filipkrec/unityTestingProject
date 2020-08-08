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

    protected bool showTooltip;

    public float duration; //-1 inf
    public float effectiveness;
    public int stacks;

    protected Component target;
    public modifierOperation operation;
    public modifierType type;
    public int ordinal;


    protected TextMeshProUGUI descriptionText;
    protected float lastDescriptionUpdateTime;

    protected GameObject modifierIcon;
    public C_Timer timer;

    public void Update()
    {
        if (descriptionText != null && timer.GetCurrentTime() - lastDescriptionUpdateTime > 1f/60)
        {
            descriptionText.text = GetDescription();
            lastDescriptionUpdateTime = timer.GetCurrentTime();
        }
    }

    public virtual void Modify() 
    {
        if (showTooltip && modifierIcon == null)
        {
            GameObject prefab = (GameObject)Resources.Load("ModifierIcon", typeof(GameObject));
            if (prefab != null)
                modifierIcon = Instantiate(prefab, Globals.GetCanvas().transform);

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

                return this.ordinal > other.ordinal;
    }

    public bool ShowsTooltip()
    {
        return showTooltip;
    }

    public void repositionTooltipIcon(Vector2 newPosition)
    {
        if (modifierIcon != null)
            modifierIcon.transform.localPosition = newPosition;
    }

    public void moveTooltipIcon(Vector2 moveFor)
    {
        if (modifierIcon != null)
            modifierIcon.transform.localPosition += new Vector3(moveFor.x,moveFor.y);
    }
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