using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // ����
    EnemyData enemyData;
    EnemyList enemyList;

    // ���������
    GameObject enemy;

    // ����
    public float enemyInterval = 3f;//�������ɼ��ʱ��

    private void Awake()
    {
        enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);
        if (enemyList != null)
        {
            enemyData = enemyList.list[0];
        }
    }

    // ��Start���������ɵ���
    private void Start()
    {
        enemy = GameObject.Find("Enemy");
        // ��ʼ�ظ�����GenerateEnemy������ÿ��enemyInterval������һ������
        // TODO:Ŀǰֻ������һ�ֵ������ͣ�֮��������Ͷ�����֮����Ҫ�޸���������ɵ��˷���
        InvokeRepeating("GenerateEnemy", 0f, enemyInterval);

    }

    // ���ɵ��˵ķ���
    private void GenerateEnemy()
    {
        if (enemyData != null)
        {
            // ����Ļ������λ�����ɵ���
            // TODO:����ɫ�ƶ�����ͼ��Ե����ʱ������ֵ���������ǽ����������Ҫ�ȵ�ͼϵͳ�����޸�
            Vector2 randomSpawnPosition = GetRandomSpawnPositionOutsideScreen();
            Instantiate(enemyData.enemyPrefab, randomSpawnPosition, Quaternion.identity, enemy.transform);
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
