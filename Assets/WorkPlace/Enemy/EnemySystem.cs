using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // 引用
    EnemyData enemyData;
    EnemyList enemyList;

    // 组件及对象
    GameObject enemySystem;

    // 属性
    public float enemyInterval = 3f;    //敌人生成间隔时间
    public float[] enemyPossibility =new float[3] { 0.6f, 0.3f, 0.1f };//生成第一、二、三类敌人概率
    private void Awake()
    {
        enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);

    }

    private void Start()
    {
        // 不再使用 GameObject.Find("Enemy")
        enemySystem = GameObject.Find("EnemySystem");  // 找到 EnemySystem 对象
        InvokeRepeating(nameof(GenerateEnemy), 2f, enemyInterval);
    }

    private void GenerateEnemy()
    {
        // 通过权重随机获取敌人类型
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
            // 使用worldSpawnPosition怪物始终左下角生成
            // Vector3 worldSpawnPosition = Camera.main.ScreenToWorldPoint(randomSpawnPosition);

            //Debug.Log($"Spawn Position: {worldSpawnPosition}");

            Instantiate(enemyData.enemyPrefab, randomSpawnPosition, Quaternion.identity, enemySystem.transform.GetChild(0));
        }
    }



    // 获取屏幕外的随机生成位置
    private Vector2 GetRandSpawnPosOutScreen()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;

        // 随机选择屏幕的边缘位置
        Vector2 randSpawnPos = new();

        float randomSide = Random.Range(0, 4);
        switch (randomSide)
        {
            case 0: // 上侧
                randSpawnPos = new Vector2(Random.Range(0, screenX), screenY + 1);
                break;
            case 1: // 右侧
                randSpawnPos = new Vector2(screenX + 1, Random.Range(0, screenY));
                break;
            case 2: // 下侧
                randSpawnPos = new Vector2(Random.Range(0, screenX), -1);
                break;
            case 3: // 左侧
                randSpawnPos = new Vector2(-1, Random.Range(0, screenY));
                break;
        }

        // 将屏幕坐标转换为世界坐标
        return Camera.main.ScreenToWorldPoint(randSpawnPos);
    }
}
