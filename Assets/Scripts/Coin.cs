using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    private float minX = -20f;
    void Start()
    {
        Move();
    }

    void Move()
    {   // ������ٵ� Ÿ���� ������ ����� ���ӿ�����Ʈ�� ������ �ٵ�������Ʈ�� �����´�.
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float randomForce = Random.Range(8f,12f);  //  4~8��ŭ�� ���� �Ǽ���
        Vector2 moveVelocity = Vector2.left * randomForce;   // �������� ������ ���� ���� �ش�.
        // ������Ʈ�� ������ ������ ���� ���� ���ӿ�����Ʈ �̵�
        moveVelocity.y = Random.Range(-2f, 2f); // ���� x���� ������ ����2�� y���� �������ش�.
        rigidbody.AddForce(moveVelocity, ForceMode2D.Impulse);  // ���Ͱ� ����Ϸ�, ���޽��� ������ ��
    }
    
    void Update()
    {
        if (transform.position.x < minX)     // ������Ʈ�� ��ǥ���� ���� �����
        {   // ��� ����� �߰��Ǿ� ��ǻ� coin�� �ƴ϶� item�̶� ����ϴ°� ��Ȯ������.
            Destroy(gameObject);
        }
    }
}
