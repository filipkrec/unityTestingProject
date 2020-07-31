using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Tiles", menuName = "CustomTiles/TilePalette", order = 1)]
public class C_TilePalette : ScriptableObject
{
    public List<C_Tile> tiles;
}
