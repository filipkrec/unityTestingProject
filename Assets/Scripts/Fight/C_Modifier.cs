using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public abstract class C_Modifier
{
    public string modifierName;
    public string description;
    public Sprite icon;

    protected bool showTooltip;

    public float duration; //-1 inf
    public float effectiveness;

    public int stacks;

    protected IModifiable target;
    public modifierOperation operation;
    public modifierType type;
    public int ordinal;


    protected TextMeshProUGUI descriptionText;
    protected float lastDescriptionUpdateTime;

    protected GameObject modifierIcon;
    public C_Timer timer;
    public C_Timer iconTimer;

    public void Update()
    {
        if (descriptionText != null && iconTimer != null && iconTimer.GetCurrentTime() - lastDescriptionUpdateTime > 1f/60)
        {
            descriptionText.text = GetDescription();
            lastDescriptionUpdateTime = iconTimer.GetCurrentTime();
        }
    }

    public virtual void Modify() 
    {
        if (showTooltip && modifierIcon == null)
        {
            GameObject prefab = Globals.GetPrefab(0); //modifierIcon prefab
            if (prefab != null)
                modifierIcon = MonoBehaviour.Instantiate(prefab, Globals.GetCanvas().transform);

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
                        {
                            txt.SetText(GetDescription());
                            descriptionText = txt;
                        }
                }
            }

            iconTimer = new C_Timer(Update, 0f, -1);
        }
    }

    public void Remove() 
    {
        iconTimer.delete = true;

        target.removeModifier(this);

        if (modifierIcon != null)
            MonoBehaviour.Destroy(modifierIcon);
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

/*
 * MOD CONSTRUCTOR EXAMPLE 
        Mod_Modifier(...)
{
        modifierName = "Name";
        description = "Desc";
        icon = Icon;

        duration = inDuration;
        effectiveness = inEffectiveness;
        target = Globals.GetPlayer();

        timer = new C_Timer();
        timer.Instantiate(delegate { RemoveSelf(); timer.deleteTimer(); }, duration);

        timer.setPrecision(1);
        lastDescriptionUpdateTime = 0.0f;

        showTooltip = true;

        operation = modifierOperation.ADDITION;
        type = modifierType.DEBUFF;
}
 */
