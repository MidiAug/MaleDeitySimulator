using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    // 引用
    EnemyData enemyData;
    EnemyList enemyList;

    // 组件及对象
    GameObject enemy;

    // 属性
    public float enemyInterval = 3f;//敌人生成间隔时间

    private void Awake()
    {
        enemyList = Resources.Load<EnemyList>(typeof(EnemyList).Name);
        if (enemyList != null)
        {
            enemyData = enemyList.list[0];
        }
    }

    // 在Start方法中生成敌人
    private void Start()
    {
        enemy = GameObject.Find("Enemy");
        // 开始重复调用GenerateEnemy方法，每隔enemyInterval秒生成一个敌人
        // TODO:目前只考虑了一种敌人类型，之后敌人类型多起来之后还需要修改这里的生成敌人方法
        InvokeRepeating("GenerateEnemy", 0f, enemyInterval);

    }

    // 生成敌人的方法
    private void GenerateEnemy()
    {
        if (enemyData != null)
        {
            // 在屏幕外的随机位置生成敌人
            // TODO:当角色移动到地图边缘附近时，会出现敌人生成在墙外的情况，需要等地图系统完善修改
            Vector2 randomSpawnPosition = GetRandomSpawnPositionOutsideScreen();
            Instantiate(enemyData.enemyPrefab, randomSpawnPosition, Quaternion.identity, enemy.transform);
        }
    }

    // 获取屏幕外的随机生成位置
    private Vector2 GetRandomSpawnPositionOutsideScreen()
    {
        float screenX = Screen.width;
        float screenY = Screen.height;

        // 随机选择屏幕的边缘位置
        Vector2 randomSpawnPosition = new Vector2();

        float randomSide = Random.Range(0, 4);

        switch (randomSide)
        {
            case 0: // 上侧
                randomSpawnPosition = new Vector2(Random.Range(0, screenX), screenY + 1);
                break;
            case 1: // 右侧
                randomSpawnPosition = new Vector2(screenX + 1, Random.Range(0, screenY));
                break;
            case 2: // 下侧
                randomSpawnPosition = new Vector2(Random.Range(0, screenX), -1);
                break;
            case 3: // 左侧
                randomSpawnPosition = new Vector2(-1, Random.Range(0, screenY));
                break;
        }

        // 将屏幕坐标转换为世界坐标
        return Camera.main.ScreenToWorldPoint(randomSpawnPosition);
    }
}
