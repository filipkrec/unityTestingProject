using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class C_SpellButtons : MonoBehaviour
{
    public Button[] spellButtons;
    public Image[] cooldownImages;
    public int imgCount = 0;

    C_SpellButtons()
    {
        spellButtons = new Button[5];
        cooldownImages = new Image[5];
    }

    public void refreshButton(int i)
    {
        if (Globals.Player.slots[i] != null)
        {
            C_Spell spell = Globals.Player.slots[i].spell;
            Debug.Assert(spell != null);

            spellButtons[i].GetComponent<Image>().sprite = spell.icon;
            spellButtons[i].onClick.AddListener(spell.Cast);
            spellButtons[i].gameObject.GetComponentInChildren<Text>().text = spell.spellName;
            spellButtons[i].gameObject.SetActive(true);

            spell.setTooltip(spellButtons[i]);

            if (spell.cooldown > 0 || spell is C_ChannelSpell)
                setCooldown(i);
        }
        else
        {
            spellButtons[i].gameObject.SetActive(false);
            if(cooldownImages[i] != null)
                cooldownImages[i].fillAmount = 0f;
            cooldownImages[i] = null;
            if (imgCount > 0) imgCount--;
        }

    }

    public void setCooldown(int i)
    {
        if(Globals.Player.slots[i] != null)
        foreach (Image image in spellButtons[i].GetComponentsInChildren<Image>())
        {
            if (image.type == Image.Type.Filled)
            {
                    cooldownImages[i] = image;
                    imgCount++;
            }
        }
    }

    public void Update()
    {
        if(imgCount > 0)
        for(int i = 0;i < 5; ++i)
        {
            if(cooldownImages[i] != null)
            {
                if (Globals.Player.slots[i].spell is C_ChannelSpell)
                {
                    C_ChannelSpell channelspell = (C_ChannelSpell)Globals.Player.slots[i].spell;
                    if (channelspell.casting)
                    {
                        cooldownImages[i].fillAmount = 1f;
                        cooldownImages[i].color = Color.red;
                        continue;
                    }
                }

                if(cooldownImages[i].color == Color.red)
                cooldownImages[i].color = new Color(183, 183, 183);

                cooldownImages[i].fillAmount = Globals.Player.slots[i].spell.getCooldownPercentage();
            }
        }
    }
}
