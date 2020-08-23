using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class C_Clash : C_ModifiableMono 
{
    public float rate;

    public float RateModifier { get => rateModifier; set { if (!backup.RateModifierModified) backup.RateModifier = rateModifier; rateModifier = value; } }
    private float rateModifier = 1.0f;
    private const float rateMax = 20f;

    public bool IsRateConstant { get => isRateConstant; set { if (!backup.IsRateConstantModified) backup.IsRateConstant = isRateConstant; isRateConstant = value; } }
    private bool isRateConstant = false;

    public float rateConstant;

    public float Clash { 
        get => clash; 
        set => addClash(value - clash); 
    }
    private float clash = 50;
    private float addedClash = 0;
    private float currentAddedClash = 0;

    C_Timer fightTimer;
    public float sliderValue;
    public Slider slider;
    public float originalScale;
         
    public C_FightCalculationBackup backup;

    private void Start()
    {
        clash = 50;
        addedClash = 0;
        currentAddedClash = 0;

        backup = new C_FightCalculationBackup();

        tooltipStartingPosition = new Vector2(0f, 175f);
        nextTooltipPositionDifference = new Vector2(20f, 0f);
    }

    private void Update()
    {
        if (Globals.paused) return;
        
        float rate = (Globals.Player.PushForce - Globals.Enemy.PushForce) * Time.smoothDeltaTime * (isRateConstant ? rateConstant : rateModifier);

        if (Math.Abs(rate) > rateMax * Time.smoothDeltaTime)
            rate = Math.Sign(rate) * rateMax * Time.smoothDeltaTime;

        clash += rate + processAddedClash();
        clash = clash > 100 ? 100 : clash < 0 ? 0 : clash;
        
        slider.value = clash;
    }

    void addClash(float val)
    {
        if (addedClash == 0)
            addedClash += val;
        else
        {
            addedClash = Math.Sign(addedClash) * Math.Abs(Math.Abs(addedClash) - Math.Abs(currentAddedClash)) + val;
            currentAddedClash = 0; 
        }
    }

    private float processAddedClash()
    {
        float addedRate;
        if (Math.Sign(addedClash) != Math.Sign(addedClash) || Math.Abs(currentAddedClash) < Math.Abs(addedClash))
        {
            addedRate = Math.Sign(addedClash) * rateMax * Time.smoothDeltaTime - Math.Sign(addedClash) * rate;
            currentAddedClash += addedRate;
        }
        else
        {
            addedRate = 0;
            addedClash = 0;
            currentAddedClash = 0;
        }

        return addedRate;
    }

    public override void UnmodifyValues()
    {
        if(backup.RateModifierModified)
        rateModifier = backup.RateModifier;

        if(backup.IsRateConstantModified)
        isRateConstant = backup.IsRateConstant;

        backup.Reset();
    }
}

public class C_FightCalculationBackup
{
    private bool rateModifierModified = false;
    private float rateModifier;

    private bool isRateConstantModified = false;
    private bool isRateConstant;

    public bool RateModifierModified { get => rateModifierModified; }
    public float RateModifier { get { return rateModifier; } set { if (!rateModifierModified) rateModifier = value; rateModifierModified = true; } }

    public bool IsRateConstantModified { get => isRateConstantModified; }
    public bool IsRateConstant { get { return isRateConstant; } set { if (!isRateConstantModified) isRateConstant = value; isRateConstantModified = true; } }

    public void Reset()
    {
        rateModifierModified = false;
        isRateConstant = false;
    }
}