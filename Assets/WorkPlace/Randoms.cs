using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成随机数、判定的类
/// </summary>
public class RandomJudges
{
    //生成一个随机数，若大于指定概率，则判定成功
    public static bool RandomJudge(float possbility)
    {
        return Random.value > possbility;
    }

    //用于生成随机敌人(暂时不能使用)
    public static int RandomEnemy(float[] possibility, int n)
    {
        // 首先，转换概率为累积概率数组
        float[] cumulativePossibility = new float[n];
        float sum = 0f;
        for (int i = 0; i < n; i++)
        {
            // 防止概率和超过1
            sum += possibility[i];
            if (sum > 1f) sum = 1f;
            cumulativePossibility[i] = sum;
        }

        // 生成随机数，并找到对应的索引
        float value = UnityEngine.Random.value;
        for (int i = 0; i < n; i++)
        {
            if (value <= cumulativePossibility[i])
                return i;
        }

        return n - 1;
    }
}
