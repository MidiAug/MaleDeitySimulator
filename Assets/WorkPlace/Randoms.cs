using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������ж�����
/// </summary>
public class RandomJudges
{
    public static bool RandomJudge(float possbility)
    {
        return Random.value > possbility;
    }
}