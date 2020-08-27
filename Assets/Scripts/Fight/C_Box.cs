using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Box : MonoBehaviour
{
    public Vector3 originalScale;
    public Vector3 originalPosition;

    public C_Spell spell;
    public int numberOfSockets;
    public List<C_Essence> essences = new List<C_Essence>();

    public C_BoxTooltip tooltip;
    bool dragging = false;

    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalScale = gameObject.transform.localScale;

        Debug.Assert(spell != null);

        foreach (C_Essence essence in essences)
        {
            essence.modify(spell);
        }

        tooltip = new C_BoxTooltip();
        tooltip.Instantiate(this);

        Debug.Assert(tooltip != null);
    }

    private void Update()
    {
    }

    public void AddEssence(C_Essence essence)
    {
        if(essences.Count <= numberOfSockets)
        {
            essences.Add(essence);
        }
    }

    public void ResetScale()
    {
        gameObject.transform.localScale = originalScale;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = originalPosition;
        tooltip.SetPosition(this);
    }

    public void onStartDrag()
    {
        tooltip.DeactivateText();
        dragging = true;
    }

    public void onStopDrag()
    {
        tooltip.SetPosition(this);
        dragging = false;
    }

    public void MouseIn()
    {
        if(!dragging)
        tooltip.ActivateText();
    }

    public void MouseOut()
    {
        tooltip.DeactivateText();
    }
}
