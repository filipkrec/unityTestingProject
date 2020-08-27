using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Mod_Essence_Player : C_Modifier
{
    float bonusPushForce = 0;
    public Mod_Essence_Player() : base()
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
        Globals.Player.PushForce += bonusPushForce;
    }

    public void Refresh(C_Box boxNew, C_Box boxOld)
    {
        if (boxOld != null)
           bonusPushForce -= boxOld.spell.pushBonus;

        if (boxNew != null)
           bonusPushForce += boxNew.spell.pushBonus;

        Globals.Player.RefreshModifiers();
    }

    public override string GetDescription()
    {
        return "";
    }
}
