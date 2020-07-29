using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_FightCalculations : MonoBehaviour
{
    private int playerForce = 4;
    private int enemyForce = 5;
    public float currentClash;
    public float rate;
    private int FPS = 0;

    C_Timer timer;
    ChangeSlider slider;

    private void Start()
    {
        timer = gameObject.AddComponent<C_Timer>();
        timer.setPrecision(2);
        slider = (ChangeSlider)gameObject.GetComponent(typeof(ChangeSlider));
        currentClash = 50;
        slider.slider.value = 50;
        timer.initiateTimer(calculateClash, 0, -1, 0.1f);
        //timer.initiateTimer(fps, 0, -1, 1.0f);
    }

    private void Update()
    {
        slider.slider.value += rate;    
        FPS++;
    }

    private void calculateClash()
    {
        float oldValue = currentClash;
        currentClash += (float)(playerForce - enemyForce) * 0.1f;
        rate = (currentClash - oldValue) / 10;
    }

    private void fps()
    {
        Debug.Log(FPS);
        FPS = 0;
    }
}
