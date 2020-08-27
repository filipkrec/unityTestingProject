using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_ShowTooltipTextBox : MonoBehaviour
{
    private void Start()
    {
        Deactivate();
    }

    public void Activate()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            child.gameObject.SetActive(true);
        }
    }

    public void Deactivate()
    {
        foreach (Transform child in gameObject.GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject == gameObject)
                continue;

            child.gameObject.SetActive(false);
        }
    }

    public void Reposition(Transform other)
    {
        gameObject.transform.position = other.position;
    }
}
