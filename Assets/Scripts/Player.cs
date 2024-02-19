using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float speed = 5; // ������ ���� ����Ƽ���� ���� ������ �� �ְ� ����
    [SerializeField] private GameObject[] weapons; // ���� ������ƮŸ�� �迭 ����
    [SerializeField] private int weaponIndex = 0;    // �迭 �ε��� ������ ����
    [SerializeField] private Transform shootTransform;   // ������ Ÿ�� ���� ����
    [SerializeField] private float shootInterval = 0.2f;    // źȯ �߻� ����
    [SerializeField] private float lastShottime = 0f;        // ������ źȯ �߻� �ð�
    [SerializeField] public TextMeshProUGUI HpText;   // ����Ƽ ���� �ؽ��� ����
    [SerializeField] private int hp = 2; // �÷��̾� ü��

    private void Start()
    {
        weaponIndex = PlayerPrefs.GetInt("weaponIndex");    // ���� ���۽� �����ȣ�� �ҷ��´�.
    }

    void Update()
    {
        HpText.SetText(hp.ToString());  // �� ���� hp�� �ؽ�Ʈ�� �����ش�.

        // Ű����� �̵��ϴ� ��� 1
        float horizontalinput = Input.GetAxisRaw("Horizontal"); // ���� ���Ⱚ
        float verticalinput = Input.GetAxisRaw("Vertical"); // ���� ���Ⱚ
        Vector3 moveTo = new Vector3(horizontalinput, verticalinput, 0f);  // ���Ⱚ�� ����ȭ
        transform.position += moveTo * speed * Time.deltaTime;   // ���Ͱ����� �ʴ� �ӵ� ����

        // ���콺�� �̵��ϴ� ��� 
        /*Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ���� ���� ���콺
        float tox = Mathf.Clamp(mouse_pos.x, -2.35f, 2.35f); // ���� ������ �����Ͽ� ����
        transform.position = new Vector3(tox, transform.position.y, transform.position.z);*/

        if(Input.GetKey(KeyCode.Z) ) { Shoot(); }   // Z�� ������ źȯ �߻�
    }
    private void Shoot()    // źȯ �߻� �Լ�
    {   // ����, ���� �ð����� ������ źȯ �߻� �ð��� ������ źȯ �߻� ���ݰ� ��
        if (Time.time - lastShottime > shootInterval) 
        {
            
            
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.Euler(0, 0, 90));
            lastShottime = Time.time; // ������ źȯ �߻� �ð� ����
        }
    }
    private void OnTriggerEnter2D(Collider2D other) // �÷��̾ �ٸ��Ͱ� �浹 ������
    {    // ���� �浹�Ѱ�(other) ���ӿ�����Ʈ �±װ� Enemy, Boss���
        if (other.gameObject.tag == "Enemy"||other.tag == "Boss"||other.gameObject.tag == "EnemyWeapon")
        {
            hp--;   // ���� �浹�� ü�°���
            if (hp <= 0)
            {
                hp = 0; // ���� źȯ�� ���ÿ� ������ -�� �ǹ���
                HpText.SetText(hp.ToString());  // �� ���� hp�� �ؽ�Ʈ�� �����ش�.
                GameManager.instance.SetGameOver(); // �ν��Ͻ��� �վ� �ٷ� ���� ����
                Destroy(gameObject);                // �÷��̾� ����
                PlayerPrefs.SetInt("weaponIndex", weaponIndex); // ���� �����ε��� �� ������
                //�װ� ���ξ����� ���� �ҷ��� ��ȭ�ϰ� �װ� ��ŸƮ�� �����ս��� ����
            }
            Destroy(other.gameObject);              // �ε��� �� ����
        }

        else if(other.gameObject.tag == "Coin") // ���� �浹�Ѱ��� coin�̶��
        {
            GameManager.instance.IncreaseCoin();    // ŸŬ������ ������� �ν��Ͻ��� �żҵ� ȣ��
            Destroy(other.gameObject);          // ���� ����
        }
        else if (other.gameObject.tag == "Coin_Big") // ���� �浹�Ѱ��� coin_Big�̶��
        {
            GameManager.instance.IncreaseCoin_Big();    // ŸŬ������ ������� �ν��Ͻ��� �żҵ� ȣ��
            Destroy(other.gameObject);          // ���� ����
        }
        else if (other.gameObject.tag == "Bomb") // ���� �浹�Ѱ��� Bomb
        {
            GameManager.instance.Bomb();        // ŸŬ������ ������� �ν��Ͻ��� �żҵ� ȣ��
            Destroy(other.gameObject);          // Bomb ����
        }
        else if (other.gameObject.tag == "Heart") // ���� �浹�Ѱ��� Geart
        {
            hp++;                               // ü�� ȸ��
            Destroy(other.gameObject);          // Heart ����
        }
    }
}
