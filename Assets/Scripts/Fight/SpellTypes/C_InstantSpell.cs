using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_InstantSpell : C_Spell
{
    public override void Cast()
    {
        if (!canUse()) return;

        useResources();
        OnCast();
        setCooldown();
    }
}
