using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float speed = 5; // 변수의 값을 유니티에서 직접 지정할 수 있게 해줌
    [SerializeField] private GameObject[] weapons; // 게임 오브젝트타입 배열 생성
    [SerializeField] private int weaponIndex = 0;    // 배열 인덱스 조절용 변수
    [SerializeField] private Transform shootTransform;   // 포지션 타입 변수 생성
    [SerializeField] private float shootInterval = 0.2f;    // 탄환 발사 간격
    [SerializeField] private float lastShottime = 0f;        // 마지막 탄환 발사 시간
    [SerializeField] public TextMeshProUGUI HpText;   // 유니티 연동 텍스르 변수
    [SerializeField] private int hp = 2; // 플레이어 체력

    private void Start()
    {
        weaponIndex = PlayerPrefs.GetInt("weaponIndex");    // 게임 시작시 무기번호를 불러온다.
    }

    void Update()
    {
        HpText.SetText(hp.ToString());  // 매 순간 hp를 텍스트로 보여준다.

        // 키보드로 이동하는 방법 1
        float horizontalinput = Input.GetAxisRaw("Horizontal"); // 가로 방향값
        float verticalinput = Input.GetAxisRaw("Vertical"); // 세로 방향값
        Vector3 moveTo = new Vector3(horizontalinput, verticalinput, 0f);  // 방향값을 벡터화
        transform.position += moveTo * speed * Time.deltaTime;   // 벡터값으로 초당 속도 구현

        // 마우스로 이동하는 방법 
        /*Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 월드 기준 마우스
        float tox = Mathf.Clamp(mouse_pos.x, -2.35f, 2.35f); // 값의 범위를 지정하여 저장
        transform.position = new Vector3(tox, transform.position.y, transform.position.z);*/

        if(Input.GetKey(KeyCode.Z) ) { Shoot(); }   // Z를 누르면 탄환 발사
    }
    private void Shoot()    // 탄환 발사 함수
    {   // 만약, 현재 시간에서 마지막 탄환 발사 시간을 뺀것을 탄환 발사 간격과 비교
        if (Time.time - lastShottime > shootInterval) 
        {
            
            
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.Euler(0, 0, 90));
            lastShottime = Time.time; // 마지막 탄환 발사 시간 정정
        }
    }
    private void OnTriggerEnter2D(Collider2D other) // 플레이어가 다른것과 충돌 했을떄
    {    // 만약 충돌한것(other) 게임오브젝트 태그가 Enemy, Boss라면
        if (other.gameObject.tag == "Enemy"||other.tag == "Boss"||other.gameObject.tag == "EnemyWeapon")
        {
            hp--;   // 적과 충돌시 체력감소
            if (hp <= 0)
            {
                hp = 0; // 여러 탄환을 동시에 맞으면 -가 되버림
                HpText.SetText(hp.ToString());  // 매 순간 hp를 텍스트로 보여준다.
                GameManager.instance.SetGameOver(); // 인스턴스가 잇어 바로 접근 가능
                Destroy(gameObject);                // 플레이어 삭제
                PlayerPrefs.SetInt("weaponIndex", weaponIndex); // 현재 웨폰인덱스 값 보내기
                //그걸 메인씬에서 값을 불러와 강화하고 그걸 스타트시 프리팹스에 저장
            }
            Destroy(other.gameObject);              // 부딛힌 적 삭제
        }

        else if(other.gameObject.tag == "Coin") // 만약 충돌한것이 coin이라면
        {
            GameManager.instance.IncreaseCoin();    // 타클래스의 만들어진 인스턴스로 매소드 호출
            Destroy(other.gameObject);          // 코인 삭제
        }
        else if (other.gameObject.tag == "Coin_Big") // 만약 충돌한것이 coin_Big이라면
        {
            GameManager.instance.IncreaseCoin_Big();    // 타클래스의 만들어진 인스턴스로 매소드 호출
            Destroy(other.gameObject);          // 코인 삭제
        }
        else if (other.gameObject.tag == "Bomb") // 만약 충돌한것이 Bomb
        {
            GameManager.instance.Bomb();        // 타클래스의 만들어진 인스턴스로 매소드 호출
            Destroy(other.gameObject);          // Bomb 삭제
        }
        else if (other.gameObject.tag == "Heart") // 만약 충돌한것이 Geart
        {
            hp++;                               // 체력 회복
            Destroy(other.gameObject);          // Heart 삭제
        }
    }
}
