using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public struct C_Tile
{
    [SerializeField]
    public Tile tile;

    [SerializeField]
    public string name;

    [SerializeField]
    public bool collision;

    C_Tile(Tile _tile, string _name, bool _collision)
    {
        tile = _tile;
        name = _name;
        collision = _collision;
    }
}
