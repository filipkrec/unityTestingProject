using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightPlayer : C_Modifiable
{
    public int numberOfSlots; 
    public C_Box[] slots;
    public C_Spell[] spells;

    public int pushForce;
    public int pushAttack;
    public int pushDefence;
    List<C_Modifier> spellModifiers;

    C_FightPlayerBackup backup;

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
    void Start()
    {
        numberOfSlots = 5;
        pushForce = 4;
        slots = new C_Box[numberOfSlots];
        spells = new C_Spell[numberOfSlots];

        refreshAllButtons();
    }

    public C_Box getSlot(int i)
    {
        if (i < numberOfSlots)
        {
            removeSpell(i);
            return slots[i];
        }
        else
            return null;
    }

    public void setSlot(int i, C_Box box)
    {
        slots[i] = box;

        if(box != null)
        addSpell(i,box.spell);
    }

    private void addSpell(int slot, C_Spell spell)
    {
        C_SpellButtons buttons = gameObject.GetComponent<C_SpellButtons>();

        if(buttons != null)
        {     
            spells[slot] = spell;
            buttons.refreshButton(slot);
        }
    }

    private void removeSpell(int i)
    {
        C_SpellButtons buttons = gameObject.GetComponent<C_SpellButtons>();
        spells[i] = null;

        if (buttons != null)
        buttons.refreshButton(i);
    }

    public void refreshAllButtons()
    {
        C_SpellButtons buttons = gameObject.GetComponent<C_SpellButtons>();

        if(buttons != null)
        for (int i = 0; i < numberOfSlots; ++i)
        {
            buttons.refreshButton(i);   
        }
    }

    protected override void unmodifyValues()
    {
        if (backup.NumberOfSlotsModified)
            numberOfSlots = backup.NumberOfSlots;
        if (backup.PushForceModified)
            pushForce = backup.PushForce;
        if (backup.PushAttackModified)
            pushAttack = backup.PushAttack;
        if (backup.PushDefenceModified)
            pushDefence = backup.PushDefence;

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

    public bool NumberOfSlotsModified { get => numberOfSlotsModified; }
    public int NumberOfSlots { get => numberOfSlots; set { numberOfSlots = value; numberOfSlotsModified = true; } }
    public bool PushForceModified { get => pushForceModified; }
    public int PushForce { get => pushForce; set { pushForce = value; pushForceModified = true; } }
    public bool PushAttackModified { get => pushAttackModified; }
    public int PushAttack { get => pushAttack; set { pushAttack = value; pushAttackModified = true; } }
    public bool PushDefenceModified { get => pushDefenceModified; }
    public int PushDefence { get => pushDefence; set { pushDefence = value; pushDefenceModified = true; } }
    
    public void Reset()
    {
         numberOfSlotsModified = false;
        
         pushForceModified = false;
        
         pushAttackModified = false;
        
         pushDefenceModified = false;
    }
}
