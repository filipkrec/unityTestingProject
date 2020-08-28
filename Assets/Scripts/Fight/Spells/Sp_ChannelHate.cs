using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_ChannelHate : C_ChannelSpell
{
    public Sp_ChannelHate() : base()
    {
        spellName = "Channel hate";
        description = "";
        channelDuration = -1;
        channelManaCost = 1;
        channelFrequency = 1.5f;
        icon = null;

        manaCost = 0f;
        cooldown = 2f;
    }

    public override void OnChannelCast()
    {
        if (!channelManaCostBackedUp)
        {
            backupChannelManaCost = channelManaCost;
            channelManaCostBackedUp = true;
        }
        channelManaCost += 1;

        Mod_Hatred stacksMod = (Mod_Hatred)Globals.Player.GetModifier<Mod_Hatred>();

        if (stacksMod != null)
        {
            stacksMod.stacks += 1;
            Globals.Player.RefreshModifiers();
        }
        else
            Globals.Player.AddModifier(new Mod_Hatred(effectiveness));
    }

    public override void OnStopChannelCast()
    {
        Mod_Hatred stacksMod = (Mod_Hatred)Globals.Player.GetModifier<Mod_Hatred>();

        if (stacksMod != null)
            stacksMod.Remove();

        UnmodifyValues();
    }

    public override void SetDescription()
    {
        description = "While channeling: \n+"
            + effectiveness + " push power every 1.5s.\n" +
            "+" + (1f - bonus.manaCostReductionPercentage) + " mana cost every 1.5s.\n " +
            "Cleared on channeling end";
    }
}
