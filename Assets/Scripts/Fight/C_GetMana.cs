using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class C_GetMana : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI txtMana;

    [SerializeField]
    public TextMeshProUGUI txtPlayerForce;

    [SerializeField]
    public TextMeshProUGUI txtEnemyForce;

    private void Start()
    {
    }
    void Update()
    {
        txtMana.SetText(Globals.Player.mana.ToString());
        txtPlayerForce.SetText(Math.Round(Globals.Player.PushForce, 2).ToString());
        txtEnemyForce.SetText(Math.Round(Globals.Enemy.PushForce, 2).ToString());
    }
}
