using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class MainScripts : MonoBehaviour
{
    public Button btnStart;          // 시작 버튼
    public Button btnWeaponUpgrad;   // 강화 버튼
    public Button btnReset;   // 리셋 버튼
    int temp;   // 플레이어 프리팹 데이터를 받아오기 위한 임시 변수
    int coin;   // 플레이어 프리팹 데이터를 받아오기 위한 임시 변수
    float textTime;// 매 시간 더하기

    [SerializeField] public TextMeshProUGUI coinText;   // 텍스트 표시 할 코인 개수
    [SerializeField] public TextMeshProUGUI systemText; // 텍스트 표시 할 대화 창 

    private void Start()
    {
        btnStart.onClick.AddListener(SceneMove);       // 버튼 클릭시 게임시작
        btnWeaponUpgrad.onClick.AddListener(Upgrad);   // 버튼 클릭시 강화 
        btnReset.onClick.AddListener(Reset);          // 버튼 클릭시 리셋
        temp = PlayerPrefs.GetInt("weaponIndex");      // 무기 번호 불러오기
        coin = PlayerPrefs.GetInt("Coin");             // coin 값 불러오기
    }
    void Update()
    {
        PlayerPrefs.SetInt("weaponIndex", temp);    // 웨폰 업데이트
        PlayerPrefs.SetInt("Coin", coin);           // 코인 업데이트
        coinText.SetText(coin.ToString());          // 메인에서 코인 개수 보여주기
        if(systemText != null)                      // 말풍선이 입력되어있으면
        {
            textTime += Time.deltaTime;             // 매 시간 더하기
            if (textTime >= 3.0f)                    // 일정 시간 이상이면
            {
                textTime = 0;                       // 타이머 초기화
                Text_Reset();                       // 텍스트 리셋 함수
            }
        }
    }
    private void SceneMove()    // 게임씬 이동
    {
        SceneManager.LoadScene("SampleScene");  
    }
    
    private void Upgrad()       // 무기 강화
    {
        textTime = 0;           // 새로 버튼을 누르면 말풍선 표시시간 값 초기화
        if (temp < 4)           // 무기 강화치가 최대치가 아니면
        {
            if (coin > (temp + 1) * 50)     // 코인이 있으면 무기강화 실행
            {
                coin -= (temp + 1) * 50;    // 코인 감소 
                temp++;         // 무기 인덱스 번호 +1
                systemText.SetText("강화 완료!!!");
                coinText.SetText(coin.ToString());
            }
            else systemText.SetText($"돈이 부족해!\n강화 비용은\n{(temp + 1) * 50}골드야!");  // 코인 없으면 불가능
        }
        else systemText.SetText("더이상 강화 할수 없어!"); // 최대치면 불가능
    }
    private void Reset()        // 로컬 데이터를 리셋한다.(초기화)
    {
        textTime = 0;           // 새로 버튼을 누르면 말풍선 표시시간 값 초기화
        temp = 0;
        coin = 0;
        /*PlayerPrefs.SetInt("Coin", 0);*/
        systemText.SetText("게임 데이터를 초기화 했어!"); // 게임 데이터 초기화
    }
    private void Text_Reset()
    {
        systemText.SetText(""); // null 값 입력
    }
}
