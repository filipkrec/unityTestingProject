using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_FightCalculations : C_Modifiable
{
    private int playerForce = 4;
    private int enemyForce = 5;
    public float rate;
    public float rateModifier;

    private int backupPlayerForce;
    private int backupEnemyForce;
    private float backupRateModifier;

    public float currentClash;

    C_Timer fightTimer;
    public Slider slider;

    private void Start()
    {
        currentClash = 50;
        slider.value = 50;

        rateModifier = 1.0f;
        backupRateModifier = 1.0f;

        backupPlayerForce = 4;
        backupEnemyForce = 5;

        fightTimer = gameObject.AddComponent<C_Timer>();
        fightTimer.setPrecision(2);
        fightTimer.initiateTimer(calculateClash, 0, -1, 0.1f);
    }

    private void Update()
    {
        slider.value += rate;
    }

    private void calculateClash()
    {
        float oldValue = currentClash;
        currentClash += (float)(playerForce - enemyForce) * 0.1f;
        rate = (currentClash - oldValue) / 10 * rateModifier;
    }

    protected override void unmodifyValues()
    {
        playerForce = backupPlayerForce;
        enemyForce = backupEnemyForce;
        rateModifier = backupRateModifier;
    }
}
