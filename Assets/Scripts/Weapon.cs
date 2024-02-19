using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private float speed = 10f;
    [SerializeField] public float damage = 1f;   // Enermy클래스의 충돌 이벤트 함수에서 사용됨

    private void Start()
    {
        Destroy(gameObject, 2f);    // 탄환은 생성되고 1초후 삭제한다.
    }
    
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;  // 탄환의 힘의 방향
    }
}
