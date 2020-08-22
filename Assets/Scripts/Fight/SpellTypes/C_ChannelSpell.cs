using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ChannelSpell : C_Spell
{
    public float channelDuration = -1f; // -1 = inf
    public float channelManaCost = 0f;
    public float channelFrequency = 1f;
    public bool exclusiveChannel = false;

    public C_Timer channelTimer;
    public C_Timer shutdownTimer;

    public bool casting = false;

    public bool channelManaCostBackedUp;
    public float backupChannelManaCost;

    public override void Cast()
    {
        if (!canUse()) return;

        if (!casting)
        {
            OnCast();
            ChannelStartCast();
        }
        else
        {
            ChannelStopCast();
        }
    }

    public virtual void OnChannelCast() {}

    public virtual void OnStopChannelCast() { }

    private void ChannelCast()
    {
        if (Globals.Player.mana > channelManaCost)
        {
            Globals.Player.mana -= (int)channelManaCost;
        }
        else
        {
            ChannelStopCast();
            return;
        }

        OnChannelCast();
    }

    public bool hasChannelCost()
    {
        return Globals.Player.mana > channelManaCost;
    }

    private void ChannelStartCast()
    {
        if (channelTimer == null)
            channelTimer = new C_Timer(ChannelCast, channelFrequency, -1, channelFrequency);

        channelTimer.Play();

        if (channelDuration > 0)
        {
            if (shutdownTimer == null)
            {
                C_Timer shutdownTimer = new C_Timer(channelTimer.StopTimer, channelDuration);
            }
        }

        casting = true;
    }

    private void ChannelStopCast()
    {
        channelTimer.StopTimer();
        setCooldown();
        OnStopChannelCast();

        if (shutdownTimer != null && shutdownTimer.IsRunning())
            shutdownTimer.StopTimer();

        casting = false;
    }

    public override void UnmodifyValues()
    {
        base.UnmodifyValues();

        if (channelManaCostBackedUp)
            channelManaCost = backupChannelManaCost;
    }
}
