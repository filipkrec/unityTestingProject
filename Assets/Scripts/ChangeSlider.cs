using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeSlider : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
        if(slider != null)
        slider.maxValue = 100;
        slider.value = 100;
    }

    public void ChangeFill(int percentage)
    {
        if (slider != null)
            slider.value = percentage;
    }
}
