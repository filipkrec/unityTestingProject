using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class C_GenerateTiles : MonoBehaviour
{
    [Range(0,100)]
    public int mountainousness;

    private const int birthLimit = 4;

    private const int deathLimit = 4;

    private const int repeat = 8;

    private bool[,] terrainMap;
    private bool[,] CheckedIslandTiles;

    public Tilemap mapGround;
    public Tilemap mapObstacles;

    public int width;
    public int height;

    public C_TilePalette palette;
    public C_TileObjectPalette objectPalette;

    private const int minIslandSize = 6;
    List<List<Vector2Int>> islands = new List<List<Vector2Int>>();
    bool islandsGenerated = false;

    private void Update()
    {
        if(!islandsGenerated)
        if (Input.GetMouseButtonDown(0) )
        {
            ConnectIslands();
            mapGround.SetTile(new Vector3Int(-1 + width / 2, 0 + height / 2, 0), palette.tiles[8].tile);
            islandsGenerated = true;
        }
    }

    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        ClearMap();

        if(terrainMap == null)
        {
            terrainMap = new bool[width, height];
            initPos();
        }

        for (int i = 0; i < repeat;  ++i)
        {
            GenerateTilePositions(terrainMap);
        }

        SetTiles();
    }

    void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y <width;y++)
            {
                terrainMap[x, y] = Random.Range(0, 100) < (60 - 15.0f * (float)mountainousness / 100) ? true : false;
            }
        }
    }

    public void ClearMap()
    {
        mapGround.ClearAllTiles();
        mapObstacles.ClearAllTiles();
    }


    void GenerateTilePositions(bool[,] terrainMap)
    {
        int neighbour;
        bool[,] newMap = new bool[width,height];
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbour = 0;
                foreach(Vector3Int b in bounds.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighbour += terrainMap[x + b.x, y + b.y] ? 1 : 0;
                    }
                    else
                        neighbour++;
                }

                if (terrainMap[x, y])
                {
                    if (8 - neighbour > deathLimit)
                        newMap[x, y] = false;
                    else
                        newMap[x, y] = true;
                }
                else 
                {
                    if (neighbour > birthLimit)
                        newMap[x, y] = true;
                    else
                        newMap[x, y] = false;
                }

            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                terrainMap[x, y] = newMap[x, y];
            }
        }
    }

    void SetTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y])
                    mapGround.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), palette.tiles[0].tile);
                else
                    mapGround.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), palette.tiles[3].tile);
            }
        }
    }

    void ConnectIslands()
    {
        List<Vector2Int> island;
        int neighbour;
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);
        CheckedIslandTiles = new bool[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                CheckedIslandTiles[x, y] = false;
            }
        }
        bool skipTile;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                //check if island marked
                skipTile = false;
                for (int i = 0; i < islands.Count; ++i)
                {
                    if (CheckedIslandTiles[x,y])
                    {
                        skipTile = true;
                        break;
                    }
                }
                if (skipTile) continue;

                //check if big enough for island
                neighbour = 0;
                foreach (Vector3Int b in bounds.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighbour += terrainMap[x + b.x, y + b.y] ? 1 : 0;
                    }
                }

                //add island
                if(neighbour + 1 >= minIslandSize)
                {
                    island = MarkIsland(x,y);
                    islands.Add(island);
                }

            }
        }

        foreach (List<Vector2Int> currentIsland in islands)
        {
            foreach (Vector2Int tile in currentIsland)
            mapGround.SetTile(new Vector3Int(-tile.x + width / 2, -tile.y + height / 2, 0), palette.tiles[5].tile);
        }
    }

    List<Vector2Int> MarkIsland(int x, int y)
    {
        List<Vector3Int> tiles = new List<Vector3Int>();
        Vector3Int currentTile = new Vector3Int(x, y,0);
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);
        Vector3Int neighbourTile;
        tiles.Add(currentTile);
        CheckedIslandTiles[x, y] = true;
        bool process;

        do
        {
            process = false;
            //add neighbours of connected tiles
            for (int i = 0; i < tiles.Count; ++i)
            {
                if (tiles[i].z == 1) continue; //skip processed 

                process = true;

                foreach (Vector3Int b in bounds.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (tiles[i].x + b.x >= 0 && tiles[i].x + b.x < width && tiles[i].y + b.y >= 0 && tiles[i].y + b.y < height)
                    {
                        neighbourTile = new Vector3Int(tiles[i].x + b.x, tiles[i].y + b.y, 0);

                        if (terrainMap[neighbourTile.x, neighbourTile.y] && !tiles.Any(tile => tile.x == neighbourTile.x && tile.y == neighbourTile.y))
                        //neighbour TRUE and not in tile list
                        {
                            tiles.Add(neighbourTile);
                            CheckedIslandTiles[neighbourTile.x, neighbourTile.y] = true;
                        }
                    }
                }
                //mark tile as processed
                tiles[i] = new Vector3Int(tiles[i].x, tiles[i].y, 1);
            }
        }
        while (process);

        List<Vector2Int> toReturn = new List<Vector2Int>();
        foreach (Vector3Int tile in tiles)
            toReturn.Add(new Vector2Int(tile.x, tile.y));

        return toReturn;
    }
}
