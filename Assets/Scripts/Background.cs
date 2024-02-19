using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    private float movespeed = 3f;  // 배경 움직이는 속도 정의

    void Update()
    {
        // 게임오브젝트(배경) 위치를 왼쪽으로 speed만큼 일정하게 힘을 가하여 움직이게
        transform.position += Vector3.left * movespeed * Time.deltaTime;
        // 
        if (transform.position.x < -18f)     // 배경이 전부 지나가버리면
        {   // 배경을 다시 오른쪽으로 이동
            transform.position += new Vector3(36f, 0, 0); 
        }   // 이를 배경1,2 모두 적용
    }
}
