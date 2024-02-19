using UnityEngine;

public class Enermy : MonoBehaviour
{

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coinBig;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject heart;       // ����Ƽ���� �����ϰ� ����
    [SerializeField] private GameObject enemyweapons;       // �� źȯ
    [SerializeField] private float moveSpeed = 10f;         // ���� �ӵ�
    [SerializeField] private float hp = 1f;                 // ���� ü��

    private float minX = -20f;          // �� ����� �����ϵ���
    private float shootInterval = 1f; // źȯ �߻� ����
    private float lastShottime = 0f;        // ������ źȯ �߻� �ð�

    public void SetMoveSpeed(float moveSpeed)      // �ۺ����� �ٸ� Ŭ�������� ��� ����
    {
        this.moveSpeed = moveSpeed;     // this�� Ŭ������ ��������� ��Ī�Ҷ� ���
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;    // ���ʴ� �ٿ�
        if(transform.position.x < minX)     // �������� ������ ��ǥ���� ���� ����
        {
            Destroy(gameObject);
        }
        Enemy_Shoot();
    }

    private void Enemy_Shoot()    // źȯ �߻� �Լ�
    {   // ����, ���� �ð����� ������ źȯ �߻� �ð��� ������ źȯ �߻� ���ݰ� ��
        if (Time.time - lastShottime > shootInterval)
        {
            if (Random.Range(1, 5) == 1)        // 25%�� Ȯ���� źȯ �߻�
            {
                if (transform.position.x >= 0f)
                {
                    Instantiate(enemyweapons, transform.position, Quaternion.identity);
                }
            }
            lastShottime = Time.time; // ������ źȯ �߻� �ð� ����
        }
    }

    // �浹 ������ �Լ� ����(hp ����)
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.tag == "Weapon")   // �浹 ����� �±װ� weapon�� ���
        {   // Weapon Ŭ������ ��ü �����ؼ� ��ũ��Ʈ�� �� ������Ʈ�Ͽ� ����
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;    // �⵿ ������ ü�� ����
            if (hp <= 0f)            // ü���� 0����
            {
                if(gameObject.tag == "Boss")    // ���� �װ� �������
                {
                    GameManager.instance.SetGameClear();     // ���� Ŭ���� ����
                }
                Destroy(gameObject);    // ���� ���
                Instantiate(coin, transform.position, Quaternion.identity); // coin����ǰ ����
                if(Random.Range(1, 10) == 1)  // ū coin����ǰ ����
                {
                    Instantiate(coinBig, transform.position, Quaternion.identity); 
                }
                if (Random.Range(1, 40) == 1)  // bomb����ǰ ����
                {
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
                if (Random.Range(1, 30) == 1)  // Heart����ǰ ����
                {
                    Instantiate(heart, transform.position, Quaternion.identity);
                }

            }
            Destroy(other.gameObject);  // źȯ�� �浹�� ����
            
        }
    }
}
