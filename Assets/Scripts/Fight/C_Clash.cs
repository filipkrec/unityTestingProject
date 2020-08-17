using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class C_Clash : C_ModifiableMono, IModifiable 
{
    public float rate;
    public float rateModifier;
    public bool isRateConstant;
    public float rateConstant;

    public float clash;
    public float currentClash;

    C_Timer fightTimer;
    public float sliderValue;
    public Slider slider;
         
    public C_FightCalculationBackup backup;

    private void Start()
    {
        clash = 50;
        currentClash = clash;

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
        currentClash += Math.Min(rate * 60 * Time.smoothDeltaTime,0.3f);
        slider.value = currentClash;
    }

    private void calculateClash()
    {
        clash += (float)(Globals.GetPlayer().pushForce - Globals.GetEnemy().pushForce) * (isRateConstant ? rateConstant : rateModifier) / 60;
        clash = clash > 100 ? 100 : clash < 0 ? 0 : clash;
        rate = clash - currentClash;
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