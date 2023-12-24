using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject player; //获取玩家位置
    public float arrowSpeed = 3f;   // 箭矢速度
    public float maxDistance = 10f;  // 箭矢的最大飞行距离
    public float damage = 10f;  // 箭矢造成的伤害

    private Vector3 targetDirection;  // 箭矢飞行的方向
    private Vector3 startPosition;    // 箭矢的初始位置

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        targetDirection = (player.transform.position - transform.position).normalized;
        float angle=Mathf.Atan2(targetDirection.y, targetDirection.x)*Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle); // 箭矢朝向玩家
    }

    void Update()
    {
        // 箭矢向前飞行
        transform.Translate(targetDirection * arrowSpeed * Time.deltaTime, Space.World);

        // 检查是否超出最大距离，如果是则销毁箭矢
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 如果碰到玩家，造成伤害并销毁箭矢
            other.GetComponent<PlayerController>().Attacked(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Build"))
        {
            // 如果碰到障碍物，销毁箭矢
            Destroy(gameObject);
        }
        else if (other.CompareTag("Crystal"))
        {
          other.transform.parent.GetComponent<CrystallController>().Attacked(damage);
          Destroy(gameObject);
        }
    }
}
