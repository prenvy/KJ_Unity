using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴: 어플리케이션 내에서 해당 클래스의 인스턴스가 오직 하나만
    // 존재하도록 하는 것이 주요 목적으로 전역적인 접근성을 가진다(게임매니저에 적합)
    public static GameManager instance = null;  // 인스턴스를 생성하여 널값을 준다.

    [SerializeField] private TextMeshProUGUI coinText;   // 유니티 연동 텍스르 변수
    [SerializeField] private GameObject gameOverPanel;   // 게임오버 윈도우 
    [SerializeField] private GameObject gameOverClear;   // 게임오버 윈도우 
    [SerializeField] int coin = 0;   // coin 정의

    [HideInInspector] public bool isGameOver = false;      // 인스펙터에서 안보이게 해줌 

    private void Awake()    // 스타트 보다 먼저 실행
    { // 싱글톤 패턴을 위한 예외처리, 오직 한 개의 객체만 존재하게 만듬
        if (instance == null)    
        {
            instance = this;
        }
    }

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");       // 'Coin'에 해당하는 값 가져오기
        coinText.SetText(coin.ToString());      // coin값을 문자열화 한 뒤 텍스트화 하여 적용
    }

    private void Update()
    {
        coinText.SetText(coin.ToString());      // coin값을 문자열화 한 뒤 텍스트화 하여 적용
    }


    public void IncreaseCoin() => coin++; // coin증가 메소드
    public void IncreaseCoin_Big() => coin += 5; // coin_Big증가 메소드

    public void Bomb() // 맵 전부 데미지 메소드
    {   // Enemy 태그를 가진 모든 오브젝트를 찾아 게임오브젝트 배열에 넣는다.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemies) Destroy(i);  // 순환하여 전부 삭제한다.
    }

    public void SetGameOver()   // 게임오버 메소드
    {
        isGameOver = true;      // 게임 종료
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();   //스크립트 참조를 위해
        if(enemySpawner != null)    // 오류방지
        {
            enemySpawner.StopEnemyRoutine();        // 코루틴 중지 메소드 실행
        }
        Invoke("ShowGameOverPanel", 1f);        // 1초 딜레이 후 함수호출(따옴표 주의) 
    }
    public void SetGameClear()   // 게임클리어 메소드
    {
        isGameOver = true;      // 게임 종료
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();   //스크립트 참조를 위해
        if (enemySpawner != null)    // 오류방지
        {
            enemySpawner.StopEnemyRoutine();        // 코루틴 중지 메소드 실행
        }
        Invoke("ShowGameClearPanel", 1f);        // 1초 딜레이 후 함수호출(따옴표 주의) 
    }
    void ShowGameOverPanel()    // 게임오버 시 1초 딜레이 후 창보이게
    {
        gameOverPanel.SetActive(true);  // 게임오버 창 보이게
    }
    void ShowGameClearPanel()    // 게임오버 시 1초 딜레이 후 창보이게
    {
        gameOverClear.SetActive(true);  // 게임오버 창 보이게
    }


    public void PlayAgain() // 다시 시작 메소드
    {
        SceneManager.LoadScene("SampleScene");  // 이미 samplescene이어도 재시작 효과가 남
        PlayerPrefs.SetInt("Coin", coin);       // 'Coin'에 coin 변수값 저장
    }

    public void SceneMove() // 메인씬으로 가는 메소드
    {
        PlayerPrefs.SetInt("Coin", coin);       // 'Coin'에 coin 변수값 저장
        SceneManager.LoadScene("MainScene");    // 메인씬 불러오기

    }
}
