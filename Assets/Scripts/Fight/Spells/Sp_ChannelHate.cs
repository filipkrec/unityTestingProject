using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sp_ChannelHate : C_ChannelSpell
{
    public Sp_ChannelHate() : base()
    {
        spellName = "Channel hate";
        description = "While channeling +1 push power/mana cost every 2 seconds, cleared on channeling end";
        channelDuration = -1;
        channelManaCost = 1;
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
            Globals.Player.AddModifier(new Mod_Hatred());
    }

    public override void OnStopChannelCast()
    {
        Mod_Hatred stacksMod = (Mod_Hatred)Globals.Player.GetModifier<Mod_Hatred>();

        if (stacksMod != null)
            stacksMod.Remove();

        UnmodifyValues();
    }
}
