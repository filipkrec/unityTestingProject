using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_FightCalculations : C_Modifiable
{
    C_FightPlayer player;
    C_FightEnemy enemy;
    public float rate;
    public float rateModifier;

    private float backupRateModifier = 1.0f;

    public float currentClash;

    C_Timer fightTimer;
    public Slider slider;

    private void Start()
    {
        currentClash = 50;
        slider.value = 50;

        rateModifier = 1.0f;

        fightTimer = gameObject.AddComponent<C_Timer>();
        fightTimer.setPrecision(2);
        fightTimer.initiateTimer(calculateClash, 0, -1, 0.1f);

        player = GetComponent<C_FightPlayer>();
        enemy = GetComponent<C_FightEnemy>();
    }

    private void Update()
    {
        slider.value += rate;
    }

    private void calculateClash()
    {
        float oldValue = currentClash;
        currentClash += (float)(player.pushForce - enemy.Enemy.pushForce) * 0.1f;
        rate = (currentClash - oldValue) / 10 * rateModifier;
    }

    protected override void unmodifyValues()
    {
        rateModifier = backupRateModifier;
    }
}
