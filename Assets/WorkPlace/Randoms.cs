using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������������ж�����
/// </summary>
public class RandomJudges
{
    //����һ���������������ָ�����ʣ����ж��ɹ�
    public static bool RandomJudge(float possbility)
    {
        return Random.value > possbility;
    }

    //���������������(��ʱ����ʹ��)
    public static int RandomEnemy(float[] possibility, int n)
    {
        // ���ȣ�ת������Ϊ�ۻ���������
        float[] cumulativePossibility = new float[n];
        float sum = 0f;
        for (int i = 0; i < n; i++)
        {
            // ��ֹ���ʺͳ���1
            sum += possibility[i];
            if (sum > 1f) sum = 1f;
            cumulativePossibility[i] = sum;
        }

        // ��������������ҵ���Ӧ������
        float value = UnityEngine.Random.value;
        for (int i = 0; i < n; i++)
        {
            if (value <= cumulativePossibility[i])
                return i;
        }

        return n - 1;
    }
}
