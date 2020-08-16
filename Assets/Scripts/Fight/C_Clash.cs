using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class C_Clash : C_ModifiableMono, IModifiable 
{
    public float rate;
    public float rateModifier;
    public bool isRateConstant;
    public float rateConstant;

    public float currentClash;

    C_Timer fightTimer;
    public float sliderValue;
    public Slider slider;
         
    public C_FightCalculationBackup backup;

    private void Start()
    {
        currentClash = 50;

        rateModifier = 1.0f;
        isRateConstant = false;

        backup = new C_FightCalculationBackup();

        tooltipStartingPosition = new Vector2(0f, 175f);
        nextTooltipPositionDifference = new Vector2(20f, 0f);

        fightTimer = new C_Timer(calculateClash, 0, -1, 1.0f / 60);
        fightTimer.setPrecision(2);
    }

    private void Update()
    {
        slider.value += rate * 60 * Time.smoothDeltaTime;
    }

    private void calculateClash()
    {
        float oldval = currentClash;
        currentClash += (float)(Globals.GetPlayer().pushForce - Globals.GetEnemy().pushForce) * (isRateConstant ? rateConstant : rateModifier) / 60;
        currentClash = currentClash > 100 ? 100 : currentClash < 0 ? 0 : currentClash;
        rate = currentClash - oldval;
    }

    public override void unmodifyValues()
    {
        if(backup.RateModifierModified)
        rateModifier = backup.RateModifier;

        isRateConstant = backup.RateConstant;
    }
}

public class C_FightCalculationBackup
{
    private bool rateModifierModified = false;
    private float rateModifier;

    private bool isRateConstantModified;
    private bool isRateConstant;

    public bool RateModifierModified { get => rateModifierModified; }
    public float RateModifier { get => rateModifier; set { if (!rateModifierModified) rateModifier = value; rateModifierModified = true; } }

    public bool RateConstant { get => isRateConstant;  set { if (!isRateConstantModified) isRateConstant = value; isRateConstantModified = true; } }

    public void Reset()
    {
        rateModifierModified = false;
        isRateConstant = false;
    }
}