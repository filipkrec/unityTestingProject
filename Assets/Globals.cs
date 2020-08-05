using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }

    public static GameObject getCanvas()
    {
        return canvas;
    }
}
