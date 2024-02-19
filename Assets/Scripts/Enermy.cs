using UnityEngine;

public class Enermy : MonoBehaviour
{

    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject coinBig;
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject heart;       // 유니티에서 연동하게 생성
    [SerializeField] private GameObject enemyweapons;       // 적 탄환
    [SerializeField] private float moveSpeed = 10f;         // 몬스터 속도
    [SerializeField] private float hp = 1f;                 // 몬스터 체력

    private float minX = -20f;          // 맵 벗어나면 삭제하도록
    private float shootInterval = 1f; // 탄환 발사 간격
    private float lastShottime = 0f;        // 마지막 탄환 발사 시간

    public void SetMoveSpeed(float moveSpeed)      // 퍼블릭으로 다른 클래스에서 사용 가능
    {
        this.moveSpeed = moveSpeed;     // this는 클래스의 멤버변수를 지칭할때 사용
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;    // 매초당 다운
        if(transform.position.x < minX)     // 떨어지는 몬스터의 좌표값에 따라 삭제
        {
            Destroy(gameObject);
        }
        Enemy_Shoot();
    }

    private void Enemy_Shoot()    // 탄환 발사 함수
    {   // 만약, 현재 시간에서 마지막 탄환 발사 시간을 뺀것을 탄환 발사 간격과 비교
        if (Time.time - lastShottime > shootInterval)
        {
            if (Random.Range(1, 5) == 1)        // 25%의 확률로 탄환 발사
            {
                if (transform.position.x >= 0f)
                {
                    Instantiate(enemyweapons, transform.position, Quaternion.identity);
                }
            }
            lastShottime = Time.time; // 마지막 탄환 발사 시간 정정
        }
    }

    // 충돌 감지시 함수 실행(hp 감소)
    private void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.tag == "Weapon")   // 충돌 대상의 태그가 weapon인 경우
        {   // Weapon 클래스의 객체 생성해서 스크립트를 겟 컴포넌트하여 저장
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;    // 출동 했으니 체력 감소
            if (hp <= 0f)            // 체력이 0이하
            {
                if(gameObject.tag == "Boss")    // 만약 그게 보스라면
                {
                    GameManager.instance.SetGameClear();     // 게임 클리어 선언
                }
                Destroy(gameObject);    // 몬스터 사망
                Instantiate(coin, transform.position, Quaternion.identity); // coin복제품 생성
                if(Random.Range(1, 10) == 1)  // 큰 coin복제품 생성
                {
                    Instantiate(coinBig, transform.position, Quaternion.identity); 
                }
                if (Random.Range(1, 40) == 1)  // bomb복제품 생성
                {
                    Instantiate(bomb, transform.position, Quaternion.identity);
                }
                if (Random.Range(1, 30) == 1)  // Heart복제품 생성
                {
                    Instantiate(heart, transform.position, Quaternion.identity);
                }

            }
            Destroy(other.gameObject);  // 탄환은 충돌시 삭제
            
        }
    }
}
