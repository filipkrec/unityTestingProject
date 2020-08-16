using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class C_GetMana : MonoBehaviour
{
    TextMeshProUGUI txt;
    
    private void Start()
    {
            txt = gameObject.GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        txt.SetText(Globals.GetPlayer().mana.ToString());
    }
}
