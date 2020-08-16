using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightPlayer : C_Modifiable, IModifiable
{
    public int numberOfSlots = 5; 
    public C_Box[] slots;

    public int manaMax = 100;
    public int mana = 100;
    public int pushForce = 4;
    public int pushAttack;
    public int pushDefence;
    List<C_Modifier> spellModifiers;

    public C_FightPlayerBackup backup;

    /*
     PRVO ZBRAJANJE => ODUZIMANJE => MNOZENJE => DJELJENJE
     
     vrj = 1;
     modifier 1 => vrj * 2;
     modifier 2 => vrj + 1;
     modifier 3 => vrj * 3;

     (1 + 1) * 2 * 3 
     1 * 2 * 3
     -----------------------------------
     */


    // Start is called before the first frame update
    public void Start()
    {
        tooltipStartingPosition = new Vector2(-200f, 0f);
        nextTooltipPositionDifference = new Vector2(0f, 20f);

        backup = new C_FightPlayerBackup();
        slots = new C_Box[numberOfSlots];
        mana = manaMax;
        refreshAllButtons();
    }

    public C_Box getSlot(int i)
    {
        if (i < numberOfSlots)
        {
            return slots[i];
        }
        else
            return null;
    }

    public void setSlot(int i, C_Box box)
    {
        slots[i] = box;
        Globals.GetButtons().refreshButton(i);
    }

    public void refreshAllButtons()
    {
        for (int i = 0; i < Globals.GetButtons().spellButtons.Length; ++i)
        {
            Globals.GetButtons().refreshButton(i);   
        }
    }

    public override void unmodifyValues()
    {
        if (backup.NumberOfSlotsModified)
            numberOfSlots = backup.NumberOfSlots;
        if (backup.PushForceModified)
            pushForce = backup.PushForce;
        if (backup.PushAttackModified)
            pushAttack = backup.PushAttack;
        if (backup.PushDefenceModified)
            pushDefence = backup.PushDefence;
        if (backup.ManaMaxModified)
            manaMax = backup.ManaMax;

        backup.Reset();
    }
}

public class C_FightPlayerBackup
{
    bool numberOfSlotsModified = false;
    int numberOfSlots;

    bool pushForceModified = false;
    int pushForce;

    bool pushAttackModified = false;
    int pushAttack;

    bool pushDefenceModified = false;
    int pushDefence;

    bool manaMaxModified = false;
    int manaMax;

    public bool NumberOfSlotsModified { get => numberOfSlotsModified; }
    public int NumberOfSlots { get => numberOfSlots; set { if (!numberOfSlotsModified) numberOfSlots = value; numberOfSlotsModified = true; } }
    public bool PushForceModified { get => pushForceModified; }
    public int PushForce { get => pushForce; set { if (!pushForceModified) pushForce = value; pushForceModified = true; } }
    public bool PushAttackModified { get => pushAttackModified; }
    public int PushAttack { get => pushAttack; set { if (!pushAttackModified) pushAttack = value; pushAttackModified = true; } }
    public bool PushDefenceModified { get => pushDefenceModified; }
    public int PushDefence { get => pushDefence; set { if (!pushDefenceModified) pushDefence = value; pushDefenceModified = true; } }
    public bool ManaMaxModified { get => manaMaxModified; }
    public int ManaMax { get => manaMax; set { if (!manaMaxModified) manaMax = value; manaMaxModified = true; } }


    public void Reset()
    {
         numberOfSlotsModified = false;
        
         pushForceModified = false;
        
         pushAttackModified = false;
        
         pushDefenceModified = false;
    }
}
