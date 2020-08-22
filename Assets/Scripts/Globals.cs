using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static C_FightPlayer player;

    private static C_FightEnemy enemy;
    public Image enemyCooldown;

    public C_Clash ClashIn;
    private static C_Clash clash;

    public GameObject CanvasIn;
    private static GameObject canvas;

    public C_SpellButtons ButtonsIn;
    private static C_SpellButtons buttons;

    public C_GlobalTimers TimersIn;
    private static C_GlobalTimers timers;

    [SerializeField]
    public List<GameObject> PrefabsIn = new List<GameObject>();
    private static List<GameObject> prefabs;

    public static Action OnUpdate = delegate{ } ;

    private void Awake()
    {
        canvas = CanvasIn;
        player = new C_FightPlayer();
        enemy = new E_GrowingTitan();
        clash = ClashIn;
        buttons = ButtonsIn;
        timers = TimersIn;
        prefabs = PrefabsIn;
    }

    private void Start()
    {
        player.Start();
        enemy.Start();
        if(enemy is E_GrowingTitan)
        {
            E_GrowingTitan tempEnemy = (E_GrowingTitan)enemy;
            tempEnemy.cooldown = enemyCooldown;
        }
    }

    private void Update()
    {
        enemy.Update();
        OnUpdate();
    }

    public static C_SpellButtons Buttons { get { return buttons; } }
    public static GameObject Canvas { get { return canvas; } }
    public static C_FightPlayer Player  { get { return player;} }
    public static C_FightEnemy Enemy { get { return enemy; } }
    public static C_Clash Clash { get { return clash; } }
    public static C_GlobalTimers Timers { get { return timers; } }
    public static GameObject GetPrefab(int i) { return prefabs[i]; }


}
