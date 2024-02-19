using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyWeapon : MonoBehaviour
{   // �� ������ �ӵ�, �������� ������, ����

    [SerializeField] private float speed = 10f;  // źȯ �ӵ�

    public Vector2 target;      // źȯ�� ������ �÷��̾��� ��ǥ

    public void Start()
    {
        GameObject player = GameObject.Find("Player");  // Player�� ��ġ���� ã�� ���� ���ӿ�����Ʈ�� ������
        Rigidbody2D rb = GetComponent<Rigidbody2D>();    // ź�� rb�� ��������
        if (player != null)     // ����ó��
        {   // ��ǥ ���ϱ� 
            target = new Vector2(player.gameObject.transform.position.x - transform.position.x,
            player.gameObject.transform.position.y - transform.position.y).normalized;
            if (rb != null)     // ����ó��
            {   // źȯ�� ������ٵ� �����ͼ� �ֵ����� ���޽�
                if (Random.Range(0, 2) == 0) rb.AddForce(target * speed, ForceMode2D.Impulse);
                else rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            }
        }
        
        Destroy(gameObject, 2f);    // źȯ�� �����ǰ� 2���� �����Ѵ�.
    }

    public void Update()
    {

    }
    
}
