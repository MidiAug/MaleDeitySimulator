using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    public Tilemap groundTileMap;

    public int width;
    public int height;

    public int seed;
    public bool useRandomSeed;

    private float lacunarity;

    [Range(0, 1f)]
    public float waterProbability;

    public TileBase groundTile;
    public TileBase waterTile;

    private bool[,] mapData;//true:grount;false:water

   public void GenerateMap()
    {
        GenerateMapData();

        GenerateTileMap();
    }

   public void GenerateMapData()
    {
        //对于种子的应用
        if(!useRandomSeed)
            seed = Time.time.GetHashCode();
        UnityEngine.Random.InitState(seed);

        mapData = new bool[width, height];

        float randomOffset = UnityEngine.Random.Range(-10000, 10000);
        
        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                float noiseValue =Mathf.PerlinNoise(x*lacunarity + randomOffset, y*lacunarity + randomOffset);
                mapData[x, y] = noiseValue < waterProbability ? false : true;
            }
        }
    }

    private void GenerateTileMap()
    {
        CleanTileMap();

        for(int x=0;x<width;x++)
        {
            for(int y=0;y<height;y++)
            {
                TileBase tile = mapData[x, y] ? groundTile : waterTile;
                groundTileMap.SetTile(new Vector3Int(x, y), tile);
            }
        }
    }
    public void CleanTileMap()
    {
        groundTileMap.ClearAllTiles();
    }

}
