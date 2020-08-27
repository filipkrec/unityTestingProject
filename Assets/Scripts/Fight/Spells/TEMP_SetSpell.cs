using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_SetSpell : MonoBehaviour
{
    public List<C_Box> backpack;

    private void Awake()
    {
        backpack[0].spell = new Sp_DivineForce();
        backpack[1].spell = new Sp_FreezeTime();
        backpack[2].spell = new Sp_Push();
        backpack[3].spell = new Sp_ChannelHate();

        //1
        backpack[0].numberOfSockets = 1;

        C_Essence pushEssence = new C_Essence();
        pushEssence.pushForce = 1;
        backpack[0].essences.Add(pushEssence);

        //2
        backpack[1].numberOfSockets = 2;

        C_Essence durationEssence = new C_Essence();
        durationEssence.durationModifier = 0.3f;
        backpack[1].essences.Add(durationEssence);

        C_Essence cooldownEssence = new C_Essence();
        cooldownEssence.cooldownReduction = 0.3f;
        backpack[1].essences.Add(cooldownEssence);


        //3
        backpack[2].numberOfSockets = 3;

        C_Essence cooldownEssence2 = new C_Essence();
        cooldownEssence2.cooldownReduction = 0.2f;
        backpack[2].essences.Add(cooldownEssence2);

        C_Essence effectivenessEssence2 = new C_Essence();
        effectivenessEssence2.effectiveness = 0.2f;
        backpack[2].essences.Add(effectivenessEssence2);

        C_Essence effectivenessEssence3 = new C_Essence();
        effectivenessEssence3.effectiveness = 0.2f;
        backpack[2].essences.Add(effectivenessEssence3);

        //4
        backpack[3].numberOfSockets = 4;

        C_Essence cooldownEssence3 = new C_Essence();
        cooldownEssence3.cooldownReduction = 0.1f;
        backpack[3].essences.Add(cooldownEssence3);

        C_Essence effectivenessEssence4 = new C_Essence();
        effectivenessEssence4.effectiveness = 0.1f;
        backpack[3].essences.Add(effectivenessEssence4);

        C_Essence manaCostEssence = new C_Essence();
        manaCostEssence.manaCostReduction = 0.1f;
        backpack[3].essences.Add(manaCostEssence);

        C_Essence manaCostEssence2 = new C_Essence();
        manaCostEssence2.manaCostReduction = 0.1f;
        backpack[3].essences.Add(manaCostEssence2);
    }
}