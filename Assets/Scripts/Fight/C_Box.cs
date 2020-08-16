using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_Box : MonoBehaviour
{
    public Vector3 originalScale;
    public Vector3 originalPosition;

    public C_Spell spell;
    public int numberOfSockets;
    public List<C_Essence> essences = new List<C_Essence>();

    void Start()
    {
        originalPosition = gameObject.transform.position;
        originalScale = gameObject.transform.localScale;
        if (spell != null)
        {
            foreach (C_Essence essence in essences)
            {
                essence.modify(spell);
            }
        }
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
