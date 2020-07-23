using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public struct C_TileObject 
{
    [SerializeField]
    public string name;

    [SerializeField]
    public List<C_TileRow> rows;
}

[System.Serializable]
public struct C_TileRow
{
    [SerializeField]
    public List<Tile> tiles;
}
