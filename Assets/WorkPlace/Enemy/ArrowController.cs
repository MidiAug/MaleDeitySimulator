using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject player; //��ȡ���λ��
    public float arrowSpeed = 3f;   // ��ʸ�ٶ�
    public float maxDistance = 10f;  // ��ʸ�������о���
    public float damage = 10f;  // ��ʸ��ɵ��˺�

    private Vector3 targetDirection;  // ��ʸ���еķ���
    private Vector3 startPosition;    // ��ʸ�ĳ�ʼλ��

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startPosition = transform.position;
        targetDirection = (player.transform.position - transform.position).normalized;
        float angle=Mathf.Atan2(targetDirection.y, targetDirection.x)*Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0,0,angle); // ��ʸ�������
    }

    void Update()
    {
        // ��ʸ��ǰ����
        transform.Translate(targetDirection * arrowSpeed * Time.deltaTime, Space.World);

        // ����Ƿ񳬳������룬����������ټ�ʸ
        if (Vector3.Distance(startPosition, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ���������ң�����˺������ټ�ʸ
            other.GetComponent<PlayerController>().Attacked(damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Build"))
        {
            // ��������ϰ�����ټ�ʸ
            Destroy(gameObject);
        }
        else if (other.CompareTag("Crystal"))
        {
          other.transform.parent.GetComponent<CrystallController>().Attacked(damage);
          Destroy(gameObject);
        }
    }
}
