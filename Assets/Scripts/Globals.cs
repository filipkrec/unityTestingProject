using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public GameObject Canvas;
    private static GameObject canvas;

    public C_FightPlayer Player;
    private static C_FightPlayer player;

    public C_FightEnemy Enemy;
    private static C_FightEnemy enemy;

    public C_Clash Clash;
    private static C_Clash clash;

    public C_SpellButtons Buttons;
    private static C_SpellButtons buttons;

    private void Start()
    {
        canvas = Canvas;
        player = Player;
        enemy = Enemy;
        clash = Clash;
        buttons = Buttons;
    }

    public static C_SpellButtons GetButtons() { return buttons; }
    public static GameObject GetCanvas() { return canvas; }
    public static C_FightPlayer GetPlayer() { return player; }
    public static C_FightEnemy GetEnemy() { return enemy; }
    public static C_Clash GetClash() { return clash; }


}
