using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStat 
{
    private int? baseStat = null;
    private int finalStat;
    private List<int> modifiers = new List<int>(); 

    public void InitiateStat(int baseValue)
    {
        baseStat = baseStat == null ? baseValue : baseStat;
    }

    public int GetStat()
    {
        return finalStat;
    }

    public void AddModifier(int modifier)
    {
        modifiers.Add(modifier);
        RecalculateStat();
    }

    public void RemoveModifier(int modifier)
    {
            modifiers.Remove(modifier);
            RecalculateStat();
    }

    private void RecalculateStat()
    {
        finalStat = baseStat == null ? 0 : baseStat.Value + modifiers.Sum();
    }
}
