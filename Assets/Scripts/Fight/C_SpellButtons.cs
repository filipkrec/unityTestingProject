using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_SpellButtons : MonoBehaviour
{
    public C_FightPlayer player;
    public Button[] spellButtons;
    public int buttonCount;

    C_SpellButtons()
    {
        spellButtons = new Button[buttonCount];
    }

    void updateButton(int i)
    {
        if(player.spells[i] != null)
        {
            C_Spell spell = player.spells[i];
            spellButtons[i].GetComponent<Image>().sprite = player.spells[i].icon;
            spellButtons[i].
        }
    }

}
