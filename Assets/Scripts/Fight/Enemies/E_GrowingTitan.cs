using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_GrowingTitan : C_FightEnemy
{
    bool triggered65;
    bool triggered80;
    bool triggered95;

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
        if (Globals.GetClash().currentClash > 65f && !triggered65)
        {
            EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)getModifier<EMod_GrowthStacks>();
            if (stacksMod == null)
                addModifier(new EMod_GrowthStacks(1));
            else
                stacksMod.stacks++;

            triggered65 = true;

            unmodifyValues();
            modifyValues();
        }
        else if (Globals.GetClash().currentClash > 80f && !triggered80)
        {
            EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)getModifier<EMod_GrowthStacks>();
            if (stacksMod == null)
                addModifier(new EMod_GrowthStacks(2));
            else stacksMod.stacks += 2;

            triggered80 = true;

            unmodifyValues();
            modifyValues();
        }
        else if (Globals.GetClash().currentClash > 95f && !triggered95)
        {
            EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)getModifier<EMod_GrowthStacks>();
            if (stacksMod == null)
                addModifier(new EMod_GrowthStacks(3));
            else stacksMod.stacks += 3;

            triggered95 = true;
            
            unmodifyValues();
            modifyValues();
        }
    }
}
