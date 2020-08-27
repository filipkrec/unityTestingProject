using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;
using TMPro;

public class Globals : MonoBehaviour
{
    private static C_FightPlayer player;

    public static Mod_Essence_Player essencesModPlayer;
    public static Mod_Essence_Clash essencesModClash;
    //private static Mod_Essence_Enemy essencesModPlayer;

    private static C_FightEnemy enemy;
    public Image enemyCooldown;
    public TextMeshProUGUI enemyName;
    public TextMeshProUGUI enemyDescription;
    public TextMeshProUGUI PauseText;
    public static TextMeshProUGUI pauseText;

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

    public static bool paused = false;

    public TextMeshProUGUI FPS;
    private static TextMeshProUGUI fps;
    private static int fpsCounter = 0;
    private static float fpsTime;

    private void Awake()
    {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;

        fps = FPS;

        canvas = CanvasIn;

        player = new C_FightPlayer();

        enemy = new E_GrowingTitan();

        clash = ClashIn;

        buttons = ButtonsIn;

        timers = TimersIn;

        prefabs = PrefabsIn;

        pauseText = PauseText;
    }

    private void Start()
    {
        paused = false;
        player.Start();
        essencesModPlayer = new Mod_Essence_Player();
        player.AddModifier(essencesModPlayer);

        if (enemy is E_GrowingTitan)
        {
            E_GrowingTitan tempEnemy = (E_GrowingTitan)enemy;
            tempEnemy.cooldown = enemyCooldown;
            enemy.tmpName = enemyName;
            enemy.tmpDescription = enemyDescription;
        }
        enemy.Start();
    }

    private void Update()
    {
        if (paused) return;

        enemy.Update();
        OnUpdate();

        fpsCounter++;
        fpsTime += Time.smoothDeltaTime;

        if (fpsTime >= 1.0f)
        {
            fps.text = fpsCounter.ToString();
            fpsCounter = 0;
            fpsTime = 0f;
        }
    }

    public static void Pause()
    {
        paused = !paused;
        buttons.Pause();
        pauseText.enabled = !pauseText.enabled;
    }

    public static C_SpellButtons Buttons { get { return buttons; } }
    public static GameObject Canvas { get { return canvas; } }
    public static C_FightPlayer Player  { get { return player;} }
    public static C_FightEnemy Enemy { get { return enemy; } }
    public static C_Clash Clash { get { return clash; } }
    public static C_GlobalTimers Timers { get { return timers; } }
    public static GameObject GetPrefab(int i) { return prefabs[i]; }


}
