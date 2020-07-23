using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TileObjects", menuName = "CustomTiles/TileObjectPalette", order = 2)]
public class C_TileObjectPalette : ScriptableObject
{
    public List<C_TileObject> tileObjects;
}
