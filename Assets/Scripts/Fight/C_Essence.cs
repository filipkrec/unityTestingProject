﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Essence 
{
    //spell
    public float effectiveness;
    public int cooldown;
    public int duration;
    public int manaCost;
    public int numberOfUses;

    //player
    public int pushForce;

    //clash
    public int rate;

    public void modify(C_Spell spell)
    {
        spell.effectiveness += effectiveness;

        spell.cooldown += cooldown;

        spell.durationModifier += duration;
        spell.channelDuration += duration;

        spell.manaCost += manaCost;

        spell.numberOfUses += numberOfUses;

        Globals.GetPlayer().pushForce += pushForce;

        Globals.GetClash().rateModifier += rate;
    }
}