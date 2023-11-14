using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成随机数、判定的类
/// </summary>
public class RandomJudges
{
    public static bool RandomJudge(float possbility)
    {
        return Random.value > possbility;
    }
}