using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class C_Enemy : MonoBehaviour
{

    protected List<KeyValuePair<C_Spell, float>> spellSequence;
    C_Timer timer;

    public int pushForce;
    public int pushAttack;
    public int pushDefence;
    List<C_Modifier> spellModifiers;

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
