using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // ����
    EnemyData enemyData;
    EnemyList enemyList;

    // ���������
    GameObject enemy;

    // ����
    public float enemyInterval = 3f;    //�������ɼ��ʱ��
    public float enemyPossibility = 0.7f;  //���ɵڶ�����˸���
    private void Awake()
    {
        enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);

    }

    private void Start()
    {
        // ����ʹ�� GameObject.Find("Enemy")
        enemy = GameObject.Find("Enemy");  // �ҵ� enemy ����
        InvokeRepeating(nameof(GenerateEnemy), 0f, enemyInterval);
    }

    private void GenerateEnemy()
    {
        if (enemyList != null && enemyList.list != null)
        {
            if (RandomJudges.RandomJudge(enemyPossibility))
            {
                enemyData = enemyList.list[1];
                //Debug.Log("Selected Enemy Type: 1");
            }
            else
            {
                enemyData = enemyList.list[0];
                //Debug.Log("Selected Enemy Type: 0");
            }
        }

        if (enemyData != null && enemy != null)
        {
            Vector2 randomSpawnPosition = GetRandomSpawnPositionOutsideScreen();
            Vector3 worldSpawnPosition = Camera.main.ScreenToWorldPoint(randomSpawnPosition);

            //Debug.Log($"Spawn Position: {worldSpawnPosition}");

            Instantiate(enemyData.enemyPrefab, worldSpawnPosition, Quaternion.identity, enemy.transform);
        }
    }



    // ��ȡ��Ļ����������λ��
    private Vector2 GetRandomSpawnPositionOutsideScreen()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;

        // ���ѡ����Ļ�ı�Եλ��
        Vector2 randomSpawnPosition = new Vector2();

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
