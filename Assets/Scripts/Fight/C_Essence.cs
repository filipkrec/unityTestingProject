using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Essence 
{
    //spell%
    public float effectiveness = 0;
    public float cooldownReduction = 0;
    public float durationModifier = 0;
    public int manaCostReduction = 0;
    public int numberOfUses = 0;

    //spell+
    public float cooldownReductionPercentage = 0;
    public float durationModifierPercentage = 0;
    public float manaCostReductionPercentage = 0;
    public float numberOfUsesPercentage = 0;

    //player
    public float pushForce = 0;
    public float pushForcePercentage = 0;

    //clash
    public float rate = 0;

    public void modify(C_Spell spell)
    {
        spell.bonus.effectiveness += effectiveness;

        spell.bonus.cooldownReduction += cooldownReduction;

        spell.bonus.durationModifier += durationModifier;

        spell.bonus.manaCostReduction +=  manaCostReduction;

        spell.bonus.numberOfUses += numberOfUses;

        spell.bonus.pushForce += pushForce;

        spell.bonus.rate += rate;

        //%

        spell.bonus.cooldownReductionPercentage += cooldownReductionPercentage;

        spell.bonus.durationModifierPercentage += durationModifierPercentage;

        spell.bonus.manaCostReductionPercentage += manaCostReductionPercentage;

        spell.bonus.numberOfUsesPercentage += numberOfUsesPercentage;

        spell.bonus.pushForcePercentage += pushForcePercentage;
    }
}
