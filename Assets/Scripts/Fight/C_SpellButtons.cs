using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_SpellButtons : MonoBehaviour
{
    public C_FightPlayer player;
    public Button[] spellButtons;

    C_SpellButtons()
    {
        spellButtons = new Button[5];
    }

    public void refreshButton(int i)
    {
        if (player.spells[i] != null)
        {
            C_Spell spell = player.spells[i];
            System.Type spellType = spell.GetType();
            spellButtons[i].GetComponent<Image>().sprite = player.spells[i].icon;
            spellButtons[i].onClick.AddListener(spell.Cast);
            spellButtons[i].gameObject.GetComponentInChildren<Text>().text = player.spells[i].spellName;
            spellButtons[i].gameObject.SetActive(true);
        }
        else spellButtons[i].gameObject.SetActive(false);
    }
}
