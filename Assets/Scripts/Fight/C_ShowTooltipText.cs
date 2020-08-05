using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class C_ShowTooltipText : MonoBehaviour
{
    private void Start()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            child.gameObject.SetActive(false);
        }
    }

    private void OnMouseEnter()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            child.gameObject.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            child.gameObject.SetActive(false);
        }
    }

}
