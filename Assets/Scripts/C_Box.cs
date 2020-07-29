using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Box : MonoBehaviour
{
    public Vector3 originalScale;
    public Vector3 originalPosition;

    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScale()
    {
        gameObject.transform.localScale = originalScale;
    }

    public void ResetPosition()
    {
        gameObject.transform.position = originalPosition;
    }

}
