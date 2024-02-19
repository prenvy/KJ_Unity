using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;   // 게임 오브젝트 타입의 배열 생성(유니티에서 확인)
    [SerializeField] private GameObject boss;       // boss 게임오브젝트를 받음
    [SerializeField] private float spawnInterval = 1.5f; // 코루틴에 쓸 몹 스폰주기 정의

    private float[] arrPosY = { -4f, -3f, -2f, -1f, 0, 1f, 2f, 3f, 4f };  // 몬스터가 생성되는 y좌표(x좌표 행이동) 9곳 생성

    void Start()                // 시작 함수
    {
        StartEnemyRoutine();    // 몹이 나오는 함수 호출
    }

    void StartEnemyRoutine()    // 몹 나오는 함수
    {
        StartCoroutine("EnemyRoutine"); // 코루틴을 시작해라(실행할 코루틴
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");  // 코루틴을 멈춰라(멈출 코루틴)
    }

    IEnumerator EnemyRoutine()  // 몹 나오는 패턴A 코루틴
    {
        yield return new WaitForSeconds(1f);    // 3초 대기 후 하위 실행

        float moveSpeed = 5f;   // 몹 이동속도
        int spawnConut = 0;     // 몹 스폰 개수를 통해 난이도 조절
        int enemyIndex = 0;     // 몹 스폰 종류를 결정
        while (true)    
        {
            foreach (float posy in arrPosY)    // 모든 좌표에 에너미 생성 함수
            {
                SpawnEnemy(posy, enemyIndex, moveSpeed);   // 에너미 생성 함수 호출
            }

            spawnConut++;                   // 에너미 생성 카운트
            if (spawnConut % 10 == 0)       // 10, 20, 30, ...
            {
                enemyIndex++;               // 10번 생성마다 다음 에너미로 
                moveSpeed += 1f;              // 매번 속도 상승
            }
            if (enemyIndex >= enemies.Length)   // 게임 후반부 접어들면
            {
                SpawnBoss();
                enemyIndex = 0;                 // 인덱스 초기화로 몹이 쉽게 나오게
                moveSpeed = 5f;                 // 속도 초기화(게임진행으로 꽤 빨라져잇음) 
            }
            yield return new WaitForSeconds(spawnInterval);    // 3초 대기 후 하위 실행
        }
    }

    void SpawnEnemy(float posy, int index, float moveSpeed)   // x좌표 위치값과, 배열에서 꺼낼 원소 자릿수
    {
        Vector3 spawnPos = new Vector3(transform.position.x, posy, transform.position.z); // 생성 위치 설정
        
        if (Random.Range(0, 9) == 0)    // 몹 출몰에  대한 랜덤성
        {
            index++;                    // 즉, 20프로 확률로 기본몹 4마리 더 강한 몹 생성
        }
        if (index >= enemies.Length)    // 위의 코드에 의해 변수가 배열의 크기를 넘는 오류 발생시
        {
            index = enemies.Length - 1; // 오류가 나지 않게 처리. 모든 몹이 강해서 더 강한 몹 안나옴
        }
        // 복제품을 생성하여 게임오브젝트에 저장
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        // Enermy 클래스의 enermy 객체에다가 복제품을 게임오브젝트화 시켜 얻어낸 스크립트 컴포넌트를 저장
        Enermy enermy = enemyObject.GetComponent<Enermy>();
        // 스크립트가 저장된 객체(기존 복제품)는 Enermy의 함수를 사용할 수 있다.
        enermy.SetMoveSpeed(moveSpeed);
    }
    
    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}