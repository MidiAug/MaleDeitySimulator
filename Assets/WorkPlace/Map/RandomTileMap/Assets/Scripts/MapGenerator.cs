﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


[Serializable]

public class ItemSpawnData
{
    public GameObject Prefab;
    public int weight;
}

public class MapGenerator : MonoBehaviour
{
    public Tilemap groundTileMap;
    public Tilemap itemTileMap;

    public int width;
    public int height;

    public int seed;
    public bool useRandomSeed;

    public float lacunarity;

    [Range(0, 1f)]
    public float waterProbability;

    public List<ItemSpawnData> itemSpawnDatas;

    // 移除孤岛Tile的次数
    public int removeSeparateTileNumberOfTimes;

    public TileBase groundTile;
    public TileBase waterTile;

    private float[,] mapData; // Ture:ground，Flase:water

    public GameObject randomProps;

    public void ClearGeneratedObjects()
    {
        // 在 randomProps 下找到所有生成的预制体并销毁它们
        foreach (Transform child in randomProps.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void Start()
    {
        // 调用生成地图的方法
        GenerateMap();
    }

    // 在游戏结束时调用清除方法
    void GameOver()
    {
        // 调用清除方法
        ClearGeneratedObjects();
    }
    public void GenerateMap()
    {
        itemSpawnDatas.Sort((data1, data2) =>
        {
            return data1.weight.CompareTo(data2.weight);
        });
        GenerateMapData();
        // 地图处理
        for (int i = 0; i < removeSeparateTileNumberOfTimes; i++)
        {
            if (!RemoveSeparateTile()) // 如果本次操作什么都没有处理，则不进行循环
            {
                break;
            }

        }

        GenerateTileMap();
    }

    private void GenerateMapData()
    {
        // 对于种子的应用
        if (!useRandomSeed) seed = Time.time.GetHashCode();
        UnityEngine.Random.InitState(seed);

        mapData = new float[width, height];

        float randomOffset = UnityEngine.Random.Range(-10000, 10000);

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float noiseValue = Mathf.PerlinNoise(x * lacunarity + randomOffset, y * lacunarity + randomOffset);
                mapData[x, y] = noiseValue;
                if (noiseValue < minValue) minValue = noiseValue;
                if (noiseValue > maxValue) maxValue = noiseValue;
            }
        }

        // 平滑到0~1
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                mapData[x, y] = Mathf.InverseLerp(minValue, maxValue, mapData[x, y]);
            }
        }
    }

    private bool RemoveSeparateTile()
    {
        bool res = false; // 是否是有效的操作
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // 是地面且只有一个邻居也是地面
                if (IsGround(x, y) && GetFourNeighborsGroundCount(x, y) <= 1)
                {
                    mapData[x, y] = 0; // 设置为水
                    res = true;
                }
            }
        }
        return res;
    }

    private int GetFourNeighborsGroundCount(int x, int y)
    {
        int count = 0;
        // top
        if (IsInMapRange(x, y + 1) && IsGround(x, y + 1)) count += 1;
        // bottom
        if (IsInMapRange(x, y - 1) && IsGround(x, y - 1)) count += 1;
        // left
        if (IsInMapRange(x - 1, y) && IsGround(x - 1, y)) count += 1;
        // right
        if (IsInMapRange(x + 1, y) && IsGround(x + 1, y)) count += 1;
        return count;
    }

    private int GetEigthNeighborsGroundCount(int x, int y)
    {
        int count = 0;
        // top
        if (IsInMapRange(x, y + 1) && IsGround(x, y + 1)) count += 1;
        // bottom
        if (IsInMapRange(x, y - 1) && IsGround(x, y - 1)) count += 1;
        // left
        if (IsInMapRange(x - 1, y) && IsGround(x - 1, y)) count += 1;
        // right
        if (IsInMapRange(x + 1, y) && IsGround(x + 1, y)) count += 1;

        // left top
        if (IsInMapRange(x - 1, y + 1) && IsGround(x - 1, y + 1)) count += 1;
        // right top
        if (IsInMapRange(x + 1, y + 1) && IsGround(x + 1, y + 1)) count += 1;
        // left bottom
        if (IsInMapRange(x - 1, y - 1) && IsGround(x - 1, y - 1)) count += 1;
        // right bottom
        if (IsInMapRange(x + 1, y - 1) && IsGround(x + 1, y - 1)) count += 1;
        return count;
    }


    private void GenerateTileMap()
    {
        CleanTileMap();

        // 地面
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                TileBase tile = IsGround(x, y) ? groundTile : waterTile;
                groundTileMap.SetTile(new Vector3Int(x, y), tile);
            }
        }

        // 物品
        int weightTotal = 0;
        for (int i = 0; i < itemSpawnDatas.Count; i++)
        {
            weightTotal += itemSpawnDatas[i].weight;
        }

        for (int x = 0; x < width/2-4; x++)
        {
            for (int y = 0; y < height/2-4; y++)
            {
                if (IsGround(x, y) && GetEigthNeighborsGroundCount(x, y) == 8) // 只有地面可以生成物品
                {
                    float randValue = UnityEngine.Random.Range(1, weightTotal + 1);
                    float temp = 0;

                    for (int i = 0; i < itemSpawnDatas.Count; i++)
                    {
                        temp += itemSpawnDatas[i].weight;
                        if (randValue < temp)
                        {
                            // 命中
                            if (itemSpawnDatas[i].Prefab)
                            {
                                // 生成在地图范围内
                                if (IsInMapRange(x, y))
                                {
                                    Instantiate(itemSpawnDatas[i].Prefab, new Vector3(x, y, 0), Quaternion.identity, randomProps.transform);
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    public bool IsInMapRange(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }

    public bool IsGround(int x, int y)
    {
        return mapData[x, y] > waterProbability;
    }


    public void CleanTileMap()
    {
        groundTileMap.ClearAllTiles();
        while (randomProps.transform.childCount > 0)
        {
            for (int i = 0; i < randomProps.transform.childCount; ++i)
            {
                DestroyImmediate(randomProps.transform.GetChild(i).gameObject);
            }
        }
    }

}
