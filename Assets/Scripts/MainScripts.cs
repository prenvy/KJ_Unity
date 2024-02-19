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
    public Button btnStart;          // ���� ��ư
    public Button btnWeaponUpgrad;   // ��ȭ ��ư
    public Button btnReset;   // ���� ��ư
    int temp;   // �÷��̾� ������ �����͸� �޾ƿ��� ���� �ӽ� ����
    int coin;   // �÷��̾� ������ �����͸� �޾ƿ��� ���� �ӽ� ����
    float textTime;// �� �ð� ���ϱ�

    [SerializeField] public TextMeshProUGUI coinText;   // �ؽ�Ʈ ǥ�� �� ���� ����
    [SerializeField] public TextMeshProUGUI systemText; // �ؽ�Ʈ ǥ�� �� ��ȭ â 

    private void Start()
    {
        btnStart.onClick.AddListener(SceneMove);       // ��ư Ŭ���� ���ӽ���
        btnWeaponUpgrad.onClick.AddListener(Upgrad);   // ��ư Ŭ���� ��ȭ 
        btnReset.onClick.AddListener(Reset);          // ��ư Ŭ���� ����
        temp = PlayerPrefs.GetInt("weaponIndex");      // ���� ��ȣ �ҷ�����
        coin = PlayerPrefs.GetInt("Coin");             // coin �� �ҷ�����
    }
    void Update()
    {
        PlayerPrefs.SetInt("weaponIndex", temp);    // ���� ������Ʈ
        PlayerPrefs.SetInt("Coin", coin);           // ���� ������Ʈ
        coinText.SetText(coin.ToString());          // ���ο��� ���� ���� �����ֱ�
        if(systemText != null)                      // ��ǳ���� �ԷµǾ�������
        {
            textTime += Time.deltaTime;             // �� �ð� ���ϱ�
            if (textTime >= 3.0f)                    // ���� �ð� �̻��̸�
            {
                textTime = 0;                       // Ÿ�̸� �ʱ�ȭ
                Text_Reset();                       // �ؽ�Ʈ ���� �Լ�
            }
        }
    }
    private void SceneMove()    // ���Ӿ� �̵�
    {
        SceneManager.LoadScene("SampleScene");  
    }
    
    private void Upgrad()       // ���� ��ȭ
    {
        textTime = 0;           // ���� ��ư�� ������ ��ǳ�� ǥ�ýð� �� �ʱ�ȭ
        if (temp < 4)           // ���� ��ȭġ�� �ִ�ġ�� �ƴϸ�
        {
            if (coin > (temp + 1) * 50)     // ������ ������ ���Ⱝȭ ����
            {
                coin -= (temp + 1) * 50;    // ���� ���� 
                temp++;         // ���� �ε��� ��ȣ +1
                systemText.SetText("��ȭ �Ϸ�!!!");
                coinText.SetText(coin.ToString());
            }
            else systemText.SetText($"���� ������!\n��ȭ �����\n{(temp + 1) * 50}����!");  // ���� ������ �Ұ���
        }
        else systemText.SetText("���̻� ��ȭ �Ҽ� ����!"); // �ִ�ġ�� �Ұ���
    }
    private void Reset()        // ���� �����͸� �����Ѵ�.(�ʱ�ȭ)
    {
        textTime = 0;           // ���� ��ư�� ������ ��ǳ�� ǥ�ýð� �� �ʱ�ȭ
        temp = 0;
        coin = 0;
        /*PlayerPrefs.SetInt("Coin", 0);*/
        systemText.SetText("���� �����͸� �ʱ�ȭ �߾�!"); // ���� ������ �ʱ�ȭ
    }
    private void Text_Reset()
    {
        systemText.SetText(""); // null �� �Է�
    }
}
