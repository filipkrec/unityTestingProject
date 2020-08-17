using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightEnemy : C_Modifiable, IModifiable
{
    protected List<KeyValuePair<C_Spell, float>> spellSequence;
    protected C_Timer timer;

    public int pushForce = 5;
    public int pushAttack;
    public int pushDefence;
    List<C_Modifier> spellModifiers;

    public C_FightEnemyBackup backup;

    public virtual void Start()
    {
        tooltipStartingPosition = new Vector2(200f, 0f);
        nextTooltipPositionDifference = new Vector2(0f, 20f);

        backup = new C_FightEnemyBackup();
    }

    public virtual void Update()
    {

    }
    public override void unmodifyValues()
    {
        if(backup.PushForceModified)
        pushForce = backup.PushForce;
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
}

public class C_FightEnemyBackup
{
    bool pushForceModified = false;
    int pushForce;

    bool pushAttackModified = false;
    int pushAttack;

    bool pushDefenceModified = false;
    int pushDefence;

    public bool PushForceModified { get => pushForceModified; }
    public int PushForce { get => pushForce; set { pushForce = value; pushForceModified = true; } }
    public bool PushAttackModified { get => pushAttackModified; }
    public int PushAttack { get => pushAttack; set { pushAttack = value; pushAttackModified = true; } }
    public bool PushDefenceModified { get => pushDefenceModified; }
    public int PushDefence { get => pushDefence; set { pushDefence = value; pushDefenceModified = true; } }
}

/*  ENEMY 1
    BUFF -> GIVES + 1 PUSH POWER PER STACK    

    EVERY 5 SEC 
                {
                IF PUSH POWER > PLAYER PUSH POWER
                    PUSH FOR PUSH FORCE x 4 
                    STACKS = 0 
                ELSE IF PUSH POWER < PLAYER PUSH POWER
                    FREEZE TIME FOR 1s x STACKS
                    STACKS += 1
                }
   
    ONCE ON 65/80/95% 
                {
                    GAIN 1/2/3 STACKS
                }
 */

/*  ENEMY 2
    EVERY 5 SEC 
                {
                    PUSH POWER = PLAYER PUSH POWER + 1
                }

    EVERY 10 SEC 
                {
                    IF PUSH POWER <= 5 
                       RATE = 0.2 * RATE NEXT 10 SECONDS
                    ELSE IF PUSH POWER > 5
                       RATE = 3 * RATE NEXT 10 SECONDS
                }

    EVERY TIME ON 60% 
                {
                    PUSH POWER = 2 x PUSH POWER
                }


    AFTER 60 SEC
                {
                    PUSH TO 99% 
                    PUSH POWER = 0 FOR NEXT 5 SECONDS; 
                    (REGARDLESS OF 5 SEC SPELL)
                }
*/
