using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static C_FightPlayer player;

    private static C_FightEnemy enemy;

    public C_Clash Clash;
    private static C_Clash clash;

    public GameObject Canvas;
    private static GameObject canvas;

    public C_SpellButtons Buttons;
    private static C_SpellButtons buttons;

    public C_GlobalTimers Timers;
    private static C_GlobalTimers timers;

    private void Awake()
    {
        canvas = Canvas;
        player = new C_FightPlayer();
        enemy = new C_FightEnemy();
        clash = Clash;
        buttons = Buttons;
        timers = Timers;
    }

    private void Start()
    {
        player.Start();
    }

    public static C_SpellButtons GetButtons() { return buttons; }
    public static GameObject GetCanvas() { return canvas; }
    public static C_FightPlayer GetPlayer() { return player; }
    public static C_FightEnemy GetEnemy() { return enemy; }
    public static C_Clash GetClash() { return clash; }
    public static C_GlobalTimers GetTimers() { return timers; }


}
