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
    {   // 리지드바디 타입의 변수를 만들어 게임오브젝트의 리지드 바디컴포넌트를 가져온다.
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        float randomForce = Random.Range(8f,12f);  //  4~8만큼의 랜덤 실수값
        Vector2 moveVelocity = Vector2.left * randomForce;   // 왼쪽으로 랜덤한 힘의 값을 준다.
        // 컴포넌트와 연동된 변수에 힘을 가해 게임오브젝트 이동
        moveVelocity.y = Random.Range(-2f, 2f); // 기존 x값만 가지던 벡터2에 y값도 설정해준다.
        rigidbody.AddForce(moveVelocity, ForceMode2D.Impulse);  // 벡터값 적용완료, 임펄스는 순간의 힘
    }
    
    void Update()
    {
        if (transform.position.x < minX)     // 오브젝트가 좌표값이 맵을 벗어나면
        {   // 계속 기능이 추가되어 사실상 coin이 아니라 item이라 명명하는게 정확해졌다.
            Destroy(gameObject);
        }
    }
}
