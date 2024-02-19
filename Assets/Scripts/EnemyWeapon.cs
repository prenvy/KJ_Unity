using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyWeapon : MonoBehaviour
{   // 적 무기의 속도, 데미지와 움직임, 삭제

    [SerializeField] private float speed = 10f;  // 탄환 속도

    public Vector2 target;      // 탄환이 추적할 플레이어의 좌표

    public void Start()
    {
        GameObject player = GameObject.Find("Player");  // Player의 위치값을 찾기 위해 게임오브젝트를 가져옴
        Rigidbody2D rb = GetComponent<Rigidbody2D>();    // 탄의 rb를 가져오기
        if (player != null)     // 예외처리
        {   // 좌표 구하기 
            target = new Vector2(player.gameObject.transform.position.x - transform.position.x,
            player.gameObject.transform.position.y - transform.position.y).normalized;
            if (rb != null)     // 예외처리
            {   // 탄환의 리지드바디를 가져와서 애드포스 임펄스
                if (Random.Range(0, 2) == 0) rb.AddForce(target * speed, ForceMode2D.Impulse);
                else rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            }
        }
        
        Destroy(gameObject, 2f);    // 탄환은 생성되고 2초후 삭제한다.
    }

    public void Update()
    {

    }
    
}
