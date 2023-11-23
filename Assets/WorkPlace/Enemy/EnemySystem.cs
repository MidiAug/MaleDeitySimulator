using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // ����
    EnemyData enemyData;
    EnemyList enemyList;

    // ���������
    GameObject enemySystem;

    // ����
    public float enemyInterval = 3f;    //�������ɼ��ʱ��
    public float[] enemyPossibility =new float[3] { 0.6f, 0.3f, 0.1f };//���ɵ�һ������������˸���
    private void Awake()
    {
        enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);

    }

    private void Start()
    {
        // ����ʹ�� GameObject.Find("Enemy")
        enemySystem = GameObject.Find("EnemySystem");  // �ҵ� EnemySystem ����
        InvokeRepeating(nameof(GenerateEnemy), 2f, enemyInterval);
    }

    private void GenerateEnemy()
    {
        if (enemyList != null && enemyList.list != null)
        {
            if (RandomJudges.RandomJudge(0.4f))
            {
                enemyData = enemyList.list[0];
            }
            else
            {
                if(RandomJudges.RandomJudge(0.25f))
                {
                    enemyData = enemyList.list[1];
                }
                else
                    enemyData= enemyList.list[2];
            }
        }

        if (enemyData != null)
        {
            Vector2 randomSpawnPosition = GetRandomSpawnPositionOutsideScreen();
            // ʹ��worldSpawnPosition����ʼ�����½�����
            // Vector3 worldSpawnPosition = Camera.main.ScreenToWorldPoint(randomSpawnPosition);

            //Debug.Log($"Spawn Position: {worldSpawnPosition}");

            Instantiate(enemyData.enemyPrefab, randomSpawnPosition, Quaternion.identity, enemySystem.transform.GetChild(0));
        }
    }



    // ��ȡ��Ļ����������λ��
    private Vector2 GetRandomSpawnPositionOutsideScreen()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;

        // ���ѡ����Ļ�ı�Եλ��
        Vector2 randomSpawnPosition = new();

        float randomSide = Random.Range(0, 4);
        switch (randomSide)
        {
            case 0: // �ϲ�
                randomSpawnPosition = new Vector2(Random.Range(0, screenX), screenY + 1);
                break;
            case 1: // �Ҳ�
                randomSpawnPosition = new Vector2(screenX + 1, Random.Range(0, screenY));
                break;
            case 2: // �²�
                randomSpawnPosition = new Vector2(Random.Range(0, screenX), -1);
                break;
            case 3: // ���
                randomSpawnPosition = new Vector2(-1, Random.Range(0, screenY));
                break;
        }

        // ����Ļ����ת��Ϊ��������
        return Camera.main.ScreenToWorldPoint(randomSpawnPosition);
    }
}
