using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class C_Enemy : C_Modifiable
{

    protected List<KeyValuePair<C_Spell, float>> spellSequence;
    C_Timer timer;

    public int pushForce;
    public int pushAttack;
    public int pushDefence;
    List<C_Modifier> spellModifiers;

    C_EnemyBackup backup;

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
}

public class C_EnemyBackup
{
    bool pushForceModified = false;
    int pushForce;

    bool pushAttackModified = false;
    int pushAttack;

    bool pushDefenceModified = false;
    int pushDefence;

    public bool PushForceModified { get => pushForceModified;}
    public int PushForce { get => pushForce; set { pushForce = value; pushForceModified = true; } }
    public bool PushAttackModified { get => pushAttackModified; }
    public int PushAttack { get => pushAttack; set { pushAttack = value; pushAttackModified = true; } }
    public bool PushDefenceModified { get => pushDefenceModified;}
    public int PushDefence { get => pushDefence; set { pushDefence = value; pushDefenceModified = true; } }
}