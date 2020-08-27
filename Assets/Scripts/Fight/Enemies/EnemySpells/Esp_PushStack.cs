using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esp_PushStack : C_InstantSpell
{
    public Esp_PushStack() : base()
    {
        spellName = "Balanced strike";
        description = "Pushes forward if stronger, blocks if weaker";
        icon = null;

        manaCost = 0f;
        cooldown = 0f;
    }

    public override void OnCast()
    {
        EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)Globals.Enemy.GetModifier<EMod_GrowthStacks>();

        if (Globals.Enemy.PushForce <= Globals.Player.PushForce)
        {
            if (stacksMod != null)
            {
                Mod_FreezeTime freezeMod = new Mod_FreezeTime(1 * stacksMod.stacks);
                Globals.Clash.AddModifier(freezeMod);
                stacksMod.stacks++;
            }
            else
                Globals.Enemy.AddModifier(new EMod_GrowthStacks(1));
        }
        else
        {
            if (stacksMod != null)
            {
                Globals.Clash.Clash -= stacksMod.stacks * 4;
                stacksMod.stacks -= stacksMod.stacks / 2;
                Globals.Enemy.RefreshModifiers();

                if (stacksMod.stacks <= 1)
                    stacksMod.Remove();
            }

        }
    }
}
