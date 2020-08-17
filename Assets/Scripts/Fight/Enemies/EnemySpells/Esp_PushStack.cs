using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esp_PushStack : C_Spell
{
        public Esp_PushStack() : base()
        {
            spellName = "Balanced strike";
            description = "Pushes forward if stronger, blocks if weaker";
            icon = null;

            manaCost = 0f;
            cooldown = 0f;
            effectiveness = 1.0f;
        }

    public override void Cast()
    {
        C_Clash clash = Globals.GetClash();

        C_FightEnemy enemy = Globals.GetEnemy();

        C_FightPlayer player = Globals.GetPlayer();

        EMod_GrowthStacks stacksMod = (EMod_GrowthStacks)enemy.getModifier<EMod_GrowthStacks>();

        if (enemy.pushForce < player.pushForce)
        {
            if (stacksMod != null)
            {
                Mod_FreezeTime freezeMod = new Mod_FreezeTime(1 * stacksMod.stacks);
                clash.addModifier(freezeMod);
                stacksMod.stacks++;
            }
            else
                enemy.addModifier(new EMod_GrowthStacks(1));
        }
        else
        {
            if (stacksMod != null)
            {
                clash.clash -= stacksMod.stacks * 4;
                stacksMod.stacks -= stacksMod.stacks / 2;

                if(stacksMod.stacks <= 1)
                stacksMod.Remove();
            }
        }
    }
}
