using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Mod_Essence_Clash : C_Modifier
{
    float bonusRate = 0;
    public Mod_Essence_Clash() : base()
    {
        modifierName = "Ess";
        description = "";
        icon = null;

        duration = -1;

        showTooltip = false;

        operation = modifierOperation.ADDITION;
        type = modifierType.INDESTRUCTIBLE;

        Initialise();
    }

    public override void Modify()
    {
        Globals.Clash.rate += bonusRate;
    }

    public void Refresh(C_Box boxNew, C_Box boxOld)
    {
        if (boxOld != null)
            bonusRate -= boxOld.spell.rateBonus;

        if (boxNew != null)
            bonusRate += boxNew.spell.rateBonus;

        Globals.Clash.RefreshModifiers();
    }

    public override string GetDescription()
    {
        return "";
    }
}
