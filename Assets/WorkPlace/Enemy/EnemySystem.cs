using System.Diagnostics;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // ����
    EnemyData enemyData;
    EnemyList enemyList;

    // ���������
    GameObject enemySystem;

    // ����
    public float enemyInterval = 1.5f;    //�������ɼ��ʱ��
    public float[] enemyPossibility =new float[3] { 0.6f, 0.3f, 0.1f };//���ɵ�һ������������˸���

    // ���˲���
    private int enemyAmount; //ÿ��������������
    private float spawnInterval = 5f;//ÿ�����ʱ��
    private float timerIsSpawn = 0f;//�жϵ��������ڵ����Ƿ�������ϵļ�ʱ��
    private float timerNotSpawn = 0f;//��¼���μ���ļ�ʱ��
    private bool isSpawn = true;//�Ƿ�Ϊ��������ʱ
    private int spawnCount = 1;//���μ���

    public bool IsSpawn { get { return isSpawn; } }


  private void Awake()
      {
          enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);

      }

    private void Start()
    {
        enemyAmount = 10;
        // ����ʹ�� GameObject.Find("Enemy")
        enemySystem = GameObject.Find("EnemySystem");  // �ҵ� EnemySystem ����
        InvokeRepeating(nameof(GenerateEnemy), 2f, enemyInterval);
    }

    private void Update()
    {
      if (isSpawn)
      {
        timerIsSpawn += Time.deltaTime;
        if (timerIsSpawn > enemyInterval * enemyAmount)
        {
          isSpawn = false;
          ++spawnCount;
          timerIsSpawn = 0;
        }
      }
      else
      {
        timerNotSpawn += Time.deltaTime;
        if (timerNotSpawn > spawnInterval)
        {
          isSpawn = true;
          enemyAmount += spawnCount * 3;
          timerNotSpawn = 0;
        }

    }
    }

  private void GenerateEnemy()
    {
      if (isSpawn)
      {
        // ͨ��Ȩ�������ȡ��������
        if (enemyList != null && enemyList.list != null)
        {
          float totalWeight = 0f;
          foreach (EnemyData tmp in enemyList.list)
          {
            totalWeight += tmp.weight;
          }

          float randomValue = Random.value * totalWeight;
          foreach (EnemyData tmp in enemyList.list)
          {
            randomValue -= tmp.weight;
            if (randomValue <= 0f)
            {
              enemyData = tmp;
              break;
            }
          }
        }

        if (enemyData != null)
        {
          Vector2 randomSpawnPosition = GetRandSpawnPosOutScreen();
          // ʹ��worldSpawnPosition����ʼ�����½�����
          // Vector3 worldSpawnPosition = Camera.main.ScreenToWorldPoint(randomSpawnPosition);

          //Debug.Log($"Spawn Position: {worldSpawnPosition}");

          Instantiate(enemyData.enemyPrefab, randomSpawnPosition, Quaternion.identity, enemySystem.transform.GetChild(0));
        }
      }
    }



    // ��ȡ��Ļ����������λ��
    private Vector2 GetRandSpawnPosOutScreen()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;

        // ���ѡ����Ļ�ı�Եλ��
        Vector2 randSpawnPos = new();

        float randomSide = Random.Range(0, 4);
        switch (randomSide)
        {
            case 0: // �ϲ�
                randSpawnPos = new Vector2(Random.Range(0, screenX), screenY + 1);
                break;
            case 1: // �Ҳ�
                randSpawnPos = new Vector2(screenX + 1, Random.Range(0, screenY));
                break;
            case 2: // �²�
                randSpawnPos = new Vector2(Random.Range(0, screenX), -1);
                break;
            case 3: // ���
                randSpawnPos = new Vector2(-1, Random.Range(0, screenY));
                break;
        }

        // ����Ļ����ת��Ϊ��������
        return Camera.main.ScreenToWorldPoint(randSpawnPos);
    }
}
