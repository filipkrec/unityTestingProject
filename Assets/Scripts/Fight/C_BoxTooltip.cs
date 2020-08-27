using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class C_BoxTooltip
{
    GameObject tooltipObject;

    public C_BoxTooltip()
    {
        tooltipObject = null;
    }

    public void Instantiate(C_Box box)
    {
        tooltipObject = Object.Instantiate(Globals.GetPrefab(2), Globals.Canvas.transform);
        string name = box.spell.spellName;
        string description = "Essences: \n";

        if (box.spell.bonus.pushForce != 0)
            description += "<color=#249D00>Push force: </color>" + box.spell.bonus.pushForce + "\n";
        if (box.spell.bonus.rate != 0)
            description += "<color=#249D00>Rate: </color>" + ToPercentage(box.spell.bonus.rate) + "\n";
        if (box.spell.bonus.numberOfUses != 0)
            description += "<color=#249D00>Uses: </color>" + box.spell.bonus.numberOfUses + "\n";
        if (box.spell.bonus.effectiveness != 0)
            description += "<color=#249D00>Effectiveness: </color>" + ToPercentage(box.spell.bonus.effectiveness) + "\n";
        if (box.spell.bonus.manaCostReduction != 0)
            description += "<color=#249D00>ManaCostReduction: </color>" + ToPercentage(box.spell.bonus.manaCostReduction) + "\n";
        if (box.spell.bonus.cooldownReduction != 0)
            description += "<color=#249D00>CooldownReduction: </color>" + ToPercentage(box.spell.bonus.cooldownReduction) + "\n";
        if (box.spell.bonus.durationModifier != 0)
            description += "<color=#249D00>DurationModifier: </color>" + ToPercentage(box.spell.bonus.durationModifier) + "\n";

        description.Remove(description.Length - 1);

        foreach (Transform child in tooltipObject.GetComponentsInChildren<Transform>())
        {
            TextMeshProUGUI txt = child.GetComponentInChildren<TextMeshProUGUI>();

            if (txt != null)
                if (txt.name == "Name")
                {
                    txt.SetText(name);
                }
                else if (txt.name == "Description")
                {
                    txt.SetText(description);
                }
        }

        tooltipObject.transform.position = box.transform.position;
    }

    private string ToPercentage(float num)
    {
        return num * 100 + "%";
    }

    public void Activate(C_Box box)
    {
        tooltipObject.SetActive(true);
    }

    public void Deactivate()
    {
        tooltipObject.SetActive(false);
    }
    public void ActivateText()
    {
        tooltipObject.GetComponent<C_ShowTooltipTextBox>().Activate();
    }

    public void DeactivateText()
    {
        tooltipObject.GetComponent<C_ShowTooltipTextBox>().Deactivate();
    }

    public void SetPosition(C_Box other)
    {
        tooltipObject.GetComponent<C_ShowTooltipTextBox>().Reposition(other.transform);
    }
}
