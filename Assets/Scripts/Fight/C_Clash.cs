using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_Clash : C_Modifiable 
{
    public float rate;
    public float rateModifier;

    public float currentClash;

    C_Timer fightTimer;
    public float sliderValue;
    public Slider slider;
         
    public C_FightCalculationBackup backup;

    private void Start()
    {
        currentClash = 50;
        sliderValue = 50;

        rateModifier = 1.0f;

        backup = new C_FightCalculationBackup();

        tooltipStartingPosition = new Vector2(0f, 175f);
        nextTooltipPositionDifference = new Vector2(20.0f, 0f);

        fightTimer = gameObject.AddComponent<C_Timer>();
        fightTimer.setPrecision(2);
        fightTimer.initiateTimer(calculateClash, 0, -1, 0.1f);
    }

    private void Update()
    {
        sliderValue += rate;
        slider.value = sliderValue;
    }

    private void calculateClash()
    {
        float oldValue = currentClash;
        currentClash += (float)(Globals.GetPlayer().pushForce - Globals.GetEnemy().pushForce) * 0.1f;
        rate = (currentClash - oldValue) / 10 * rateModifier;
    }

    protected override void unmodifyValues()
    {
        if(backup.RateModifierModified)
        rateModifier = backup.RateModifier;

        backup.Reset();
    }
}

public class C_FightCalculationBackup
{
    private bool rateModifierModified = false;
    private float rateModifier;

    public bool RateModifierModified { get => rateModifierModified; }
    public float RateModifier { get => rateModifier; set { if (!rateModifierModified) rateModifier = value; rateModifierModified = true; } }


    public void Reset()
    {
        rateModifierModified = false;
    }
}