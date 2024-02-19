using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ����: ���ø����̼� ������ �ش� Ŭ������ �ν��Ͻ��� ���� �ϳ���
    // �����ϵ��� �ϴ� ���� �ֿ� �������� �������� ���ټ��� ������(���ӸŴ����� ����)
    public static GameManager instance = null;  // �ν��Ͻ��� �����Ͽ� �ΰ��� �ش�.

    [SerializeField] private TextMeshProUGUI coinText;   // ����Ƽ ���� �ؽ��� ����
    [SerializeField] private GameObject gameOverPanel;   // ���ӿ��� ������ 
    [SerializeField] private GameObject gameOverClear;   // ���ӿ��� ������ 
    [SerializeField] int coin = 0;   // coin ����

    [HideInInspector] public bool isGameOver = false;      // �ν����Ϳ��� �Ⱥ��̰� ���� 

    private void Awake()    // ��ŸƮ ���� ���� ����
    { // �̱��� ������ ���� ����ó��, ���� �� ���� ��ü�� �����ϰ� ����
        if (instance == null)    
        {
            instance = this;
        }
    }

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");       // 'Coin'�� �ش��ϴ� �� ��������
        coinText.SetText(coin.ToString());      // coin���� ���ڿ�ȭ �� �� �ؽ�Ʈȭ �Ͽ� ����
    }

    private void Update()
    {
        coinText.SetText(coin.ToString());      // coin���� ���ڿ�ȭ �� �� �ؽ�Ʈȭ �Ͽ� ����
    }


    public void IncreaseCoin() => coin++; // coin���� �޼ҵ�
    public void IncreaseCoin_Big() => coin += 5; // coin_Big���� �޼ҵ�

    public void Bomb() // �� ���� ������ �޼ҵ�
    {   // Enemy �±׸� ���� ��� ������Ʈ�� ã�� ���ӿ�����Ʈ �迭�� �ִ´�.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject i in enemies) Destroy(i);  // ��ȯ�Ͽ� ���� �����Ѵ�.
    }

    public void SetGameOver()   // ���ӿ��� �޼ҵ�
    {
        isGameOver = true;      // ���� ����
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();   //��ũ��Ʈ ������ ����
        if(enemySpawner != null)    // ��������
        {
            enemySpawner.StopEnemyRoutine();        // �ڷ�ƾ ���� �޼ҵ� ����
        }
        Invoke("ShowGameOverPanel", 1f);        // 1�� ������ �� �Լ�ȣ��(����ǥ ����) 
    }
    public void SetGameClear()   // ����Ŭ���� �޼ҵ�
    {
        isGameOver = true;      // ���� ����
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();   //��ũ��Ʈ ������ ����
        if (enemySpawner != null)    // ��������
        {
            enemySpawner.StopEnemyRoutine();        // �ڷ�ƾ ���� �޼ҵ� ����
        }
        Invoke("ShowGameClearPanel", 1f);        // 1�� ������ �� �Լ�ȣ��(����ǥ ����) 
    }
    void ShowGameOverPanel()    // ���ӿ��� �� 1�� ������ �� â���̰�
    {
        gameOverPanel.SetActive(true);  // ���ӿ��� â ���̰�
    }
    void ShowGameClearPanel()    // ���ӿ��� �� 1�� ������ �� â���̰�
    {
        gameOverClear.SetActive(true);  // ���ӿ��� â ���̰�
    }


    public void PlayAgain() // �ٽ� ���� �޼ҵ�
    {
        SceneManager.LoadScene("SampleScene");  // �̹� samplescene�̾ ����� ȿ���� ��
        PlayerPrefs.SetInt("Coin", coin);       // 'Coin'�� coin ������ ����
    }

    public void SceneMove() // ���ξ����� ���� �޼ҵ�
    {
        PlayerPrefs.SetInt("Coin", coin);       // 'Coin'�� coin ������ ����
        SceneManager.LoadScene("MainScene");    // ���ξ� �ҷ�����

    }
}
