using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class C_FightEnemy : C_Modifiable
{
    protected List<KeyValuePair<C_Spell, float>> spellSequence;
    protected C_Timer timer;
    public TextMeshProUGUI tmpName;
    protected string name;
    public TextMeshProUGUI tmpDescription;
    protected string description;

    public float PushForce { get => pushForce; set { if (!backup.PushForceModified) backup.PushForce = pushForce; pushForce = value; } }
    private float pushForce = 5;

    public float PushAttack { get => pushAttack; set { if (!backup.PushAttackModified) backup.PushAttack = pushAttack; pushAttack = value; } }
    private float pushAttack = 5;

    public float PushDefence { get => pushDefence; set { if (!backup.PushDefenceModified) backup.PushDefence = pushDefence; pushAttack = value; } }
    private float pushDefence = 5;

    List<C_Modifier> spellModifiers;

    public C_FightEnemyBackup backup;

    public virtual void Start()
    {
        tooltipStartingPosition = new Vector2(200f, 0f);
        nextTooltipPositionDifference = new Vector2(0f, 20f);

        backup = new C_FightEnemyBackup();

        tmpName.text = name;
        tmpDescription.text = description;
    }

    public virtual void Update() { }

    public override void UnmodifyValues()
    {
        if(backup.PushForceModified)
        pushForce = backup.PushForce;

        backup.Reset();
    }
}

public class C_FightEnemyBackup
{
    bool pushForceModified = false;
    float pushForce;

    bool pushAttackModified = false;
    float pushAttack;

    bool pushDefenceModified = false;
    float pushDefence;

    public bool PushForceModified { get => pushForceModified; }
    public bool PushAttackModified { get => pushAttackModified; }
    public bool PushDefenceModified { get => pushDefenceModified; }

    public float PushForce { get { return pushForce; } set { if (!pushForceModified) pushForce = value; pushForceModified = true; } }
    public float PushAttack { get { return pushAttack; } set { if (!pushAttackModified) pushAttack = value; pushAttackModified = true; } }
    public float PushDefence { get { return pushDefence; } set { if (!pushDefenceModified) pushDefence = value; pushDefenceModified = true; } }

    public void Reset()
    {
        pushForceModified = false;
        pushAttackModified = false;
        pushDefenceModified = false;
    }
}



/*

SEQ 1 -> svakih 5 sec x 
         svakih 12 sec y
         svakih 31 sec z

SEQ 2 -> 15s 
         5s -> x
         10 s -> y
         15 s -> z
         repeat
 */
