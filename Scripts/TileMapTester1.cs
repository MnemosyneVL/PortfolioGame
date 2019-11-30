using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapTester1 : MonoBehaviour
{
    public Tilemap wallTiles;
    public Tilemap groundTiles;
    public Tile[] grounds;
    public Tile[] walls;
    public Vector3 centerPos;

    private void Start()
    {
        wallTiles.SetTile(new Vector3Int(0, 0, 0), walls[0]);
        groundTiles.SetTile(new Vector3Int(0, 0, 0), grounds[0]);
        centerPos = wallTiles.GetCellCenterWorld(new Vector3Int(0,0,0));

    }
}
