using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] public float damage = 1f;   // EnermyŬ������ �浹 �̺�Ʈ �Լ����� ����

    private void Start()
    {
        Destroy(gameObject, 2f);    // źȯ�� �����ǰ� 1���� �����Ѵ�.
    }
    
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;  // źȯ�� ���� ����
    }
}
