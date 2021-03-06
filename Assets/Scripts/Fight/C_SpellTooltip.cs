﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class C_SpellTooltip : MonoBehaviour
{
    GameObject tooltipObject;

    public C_SpellTooltip(string name, string description, int manaCost, Button button)
    {
        tooltipObject = Instantiate(Globals.GetPrefab(1), button.transform);

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
                else if (txt.name == "ManaCostText")
                {
                    if (manaCost > 0)
                        txt.SetText(manaCost.ToString());
                    else
                        txt.SetText("");
                }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipObject.GetComponent<RectTransform>());
        Physics2D.SyncTransforms();
    }

    public void activate()
    {
        tooltipObject.SetActive(true);
    }

    public void deactivate()
    {
        tooltipObject.SetActive(false);
    }
}
