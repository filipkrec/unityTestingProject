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

    public float currentClash;

    C_Timer fightTimer;
    public float sliderValue;
    public Slider slider;

    public int[] testArray = { -1, 1, 2, 3, 3, 2, 6, 5, -3, 2, 8, -9, 15, -3 };
    public List<int> testSort = new List<int>();
    
    
         
    C_FightCalculationBackup backup;

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

        for (int i = 0; i < testArray.Length; ++i)
        {
            testSort.Add(testArray[i]);
        }

        testSort.Sort((int1, int2) => int1 < int2 ? -1 : 1);
        string testString = "";
        for (int i = 0;i < testSort.Count; ++i)
            testString += testSort[i].ToString() + ",";

        Debug.Log(testString);
    }

    private void Update()
    {
        sliderValue += rate;
        slider.value = sliderValue;
    }

    private void calculateClash()
    {
        float oldValue = currentClash;
        currentClash += (float)(player.pushForce - enemy.Enemy.pushForce) * 0.1f;
        rate = (currentClash - oldValue) / 10 * rateModifier;
    }

    protected override void unmodifyValues()
    {
        if(backup.RateModifierModified)
        rateModifier = backup.RateModifier;

        backup.Reset();
    }
}

class C_FightCalculationBackup
{
    private bool rateModifierModified = false;
    private float rateModifier;

    public bool RateModifierModified { get => rateModifierModified; }
    public float RateModifier { get => rateModifier; set { rateModifier = value; rateModifierModified = true; } }


    public void Reset()
    {
        rateModifierModified = false;
    }
}