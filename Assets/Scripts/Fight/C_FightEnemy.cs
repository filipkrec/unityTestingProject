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
