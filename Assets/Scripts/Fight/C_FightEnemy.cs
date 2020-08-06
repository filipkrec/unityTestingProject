using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightEnemy : MonoBehaviour
{
    private static C_Enemy enemy;
    public C_Enemy Enemy { get { return enemy; } }

    public static void setEnemy(C_Enemy inEnemy)
    {
        enemy = inEnemy;
    }

    private void Start()
    {
        C_Enemy enemyComponent = gameObject.AddComponent<C_Enemy>();
        enemyComponent.pushForce = 5;
        enemy = enemyComponent;
    }
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
