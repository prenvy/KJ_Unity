using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    private float movespeed = 3f;  // ��� �����̴� �ӵ� ����

    void Update()
    {
        // ���ӿ�����Ʈ(���) ��ġ�� �������� speed��ŭ �����ϰ� ���� ���Ͽ� �����̰�
        transform.position += Vector3.left * movespeed * Time.deltaTime;
        // 
        if (transform.position.x < -18f)     // ����� ���� ������������
        {   // ����� �ٽ� ���������� �̵�
            transform.position += new Vector3(36f, 0, 0); 
        }   // �̸� ���1,2 ��� ����
    }
}
