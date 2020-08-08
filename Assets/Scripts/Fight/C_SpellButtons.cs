using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_SpellButtons : MonoBehaviour
{
    public Button[] spellButtons;

    C_SpellButtons()
    {
        spellButtons = new Button[5];
    }

    public void refreshButton(int i)
    {
        if (Globals.GetPlayer().slots[i] != null)
        {
            C_Spell spell = Globals.GetPlayer().slots[i].spell;
            System.Type spellType = spell.GetType();
            spellButtons[i].GetComponent<Image>().sprite = spell.icon;
            spellButtons[i].onClick.AddListener(spell.Cast);
            spellButtons[i].gameObject.GetComponentInChildren<Text>().text = spell.spellName;
            spellButtons[i].gameObject.SetActive(true);
        }
        else spellButtons[i].gameObject.SetActive(false);
    }
}
