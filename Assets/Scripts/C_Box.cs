using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Box : MonoBehaviour
{
    public Vector3 originalScale;
    public Vector3 originalPosition;
    public bool slotted;

    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalScale = gameObject.transform.localScale;
        slotted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
