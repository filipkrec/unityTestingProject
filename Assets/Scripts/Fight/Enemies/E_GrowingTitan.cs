using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class E_GrowingTitan : C_FightEnemy
{
    bool triggered65;
    bool triggered80;
    bool triggered95;
    public Image cooldown;

    public E_GrowingTitan()
    {
        triggered65 = false;
        triggered80 = false;
        triggered95 = false;
    }

    public override void Start()
    {
        base.Start();

        spellSequence = new List<KeyValuePair<C_Spell, float>>();
        spellSequence.Add(new KeyValuePair<C_Spell, float>(new Esp_PushStack(), 1f));
        timer = new C_Timer(spellSequence[0].Key.Cast, 5f, -1, 5.0f);
    }

    public override void Update()
    {
        cooldown.fillAmount = 1 - (timer.GetCurrentTime() % 5) / 5;

        if (Globals.Clash.Clash > 65f && !triggered65)
        {
            EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)GetModifier<EMod_GrowthStacks>();
            if (stacksMod == null)
                AddModifier(new EMod_GrowthStacks(1));
            else
                stacksMod.stacks++;

            triggered65 = true;

            UnmodifyValues();
            ModifyValues();
        }
        else if (Globals.Clash.Clash > 80f && !triggered80)
        {
            EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)GetModifier<EMod_GrowthStacks>();
            if (stacksMod == null)
                AddModifier(new EMod_GrowthStacks(2));
            else stacksMod.stacks += 2;

            triggered80 = true;

            UnmodifyValues();
            ModifyValues();
        }
        else if (Globals.Clash.Clash > 95f && !triggered95)
        {
            EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)GetModifier<EMod_GrowthStacks>();
            if (stacksMod == null)
                AddModifier(new EMod_GrowthStacks(3));
            else stacksMod.stacks += 3;

            triggered95 = true;
            
            UnmodifyValues();
            ModifyValues();
        }
    }
}
