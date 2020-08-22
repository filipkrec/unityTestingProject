using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightPlayer : C_Modifiable
{
    public int numberOfSlots = 5; 
    public C_Box[] slots;

    public int ManaMax { get => manaMax; set { if (!backup.ManaMaxModified) backup.ManaMax = manaMax; manaMax = value; } }
    private int manaMax = 100;

    public int mana = 100;
    public float manaRegen = 1;
    public float manaRegenPool = 0;
    public float PushForce { get => pushForce; set { if(!backup.PushForceModified) backup.PushForce = pushForce; pushForce = value; } }
    private float pushForce = 4;

    public float PushAttack { get => pushAttack; set { if (!backup.PushAttackModified) backup.PushAttack = pushAttack; PushAttack = value; } }
    private float pushAttack;

    public float PushDefence { get => pushDefence; set { if (!backup.PushDefenceModified) backup.PushDefence = pushDefence; pushDefence = value; } }
    private float pushDefence;
    List<C_Modifier> spellModifiers;

    public C_FightPlayerBackup backup;

    public void Start()
    {
        tooltipStartingPosition = new Vector2(-200f, 0f);
        nextTooltipPositionDifference = new Vector2(0f, 20f);

        backup = new C_FightPlayerBackup();
        slots = new C_Box[numberOfSlots];
        mana = manaMax;
        Globals.OnUpdate += RegenMana;
        refreshAllButtons();
    }

    private void RegenMana()
    {
        if (mana < manaMax)
        {
            manaRegenPool += manaRegen * Time.smoothDeltaTime;
            if (manaRegenPool > 1)
            {
                mana += Mathf.FloorToInt(manaRegenPool);
                manaRegenPool -= Mathf.FloorToInt(manaRegenPool);
            }
        }
        else
            manaRegenPool = 0;

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
        Globals.Buttons.refreshButton(i);
    }

    public void refreshAllButtons()
    {
        for (int i = 0; i < Globals.Buttons.spellButtons.Length; ++i)
        {
            Globals.Buttons.refreshButton(i);   
        }
    }

    public override void UnmodifyValues()
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
    float pushForce;

    bool pushAttackModified = false;
    float pushAttack;

    bool pushDefenceModified = false;
    float pushDefence;

    bool manaMaxModified = false;
    int manaMax;

    public bool NumberOfSlotsModified { get => numberOfSlotsModified; }
    public int NumberOfSlots { get { return numberOfSlots; } set { if (!numberOfSlotsModified) numberOfSlots = value; numberOfSlotsModified = true; } }
    public bool PushForceModified { get => pushForceModified; }
    public float PushForce { get { return pushForce; } set { if (!pushForceModified) pushForce = value; pushForceModified = true; } }
    public bool PushAttackModified { get => pushAttackModified; }
    public float PushAttack { get { return pushAttack; } set { if (!pushAttackModified) pushAttack = value; pushAttackModified = true; } }
    public bool PushDefenceModified { get => pushDefenceModified; }
    public float PushDefence { get { return pushDefence; } set { if (!pushDefenceModified) pushDefence = value; pushDefenceModified = true; } }
    public bool ManaMaxModified { get => manaMaxModified; }
    public int ManaMax { get { return manaMax; } set { if (!manaMaxModified) manaMax = value; manaMaxModified = true; } }

    public void Reset()
    {
        numberOfSlotsModified = false;
        pushForceModified = false;
        pushAttackModified = false;
        pushDefenceModified = false;
        manaMaxModified = false;
    }
}
