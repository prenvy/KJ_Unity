using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;   // ���� ������Ʈ Ÿ���� �迭 ����(����Ƽ���� Ȯ��)
    [SerializeField] private GameObject boss;       // boss ���ӿ�����Ʈ�� ����
    [SerializeField] private float spawnInterval = 1.5f; // �ڷ�ƾ�� �� �� �����ֱ� ����

    private float[] arrPosY = { -4f, -3f, -2f, -1f, 0, 1f, 2f, 3f, 4f };  // ���Ͱ� �����Ǵ� y��ǥ(x��ǥ ���̵�) 9�� ����

    void Start()                // ���� �Լ�
    {
        StartEnemyRoutine();    // ���� ������ �Լ� ȣ��
    }

    void StartEnemyRoutine()    // �� ������ �Լ�
    {
        StartCoroutine("EnemyRoutine"); // �ڷ�ƾ�� �����ض�(������ �ڷ�ƾ
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");  // �ڷ�ƾ�� �����(���� �ڷ�ƾ)
    }

    IEnumerator EnemyRoutine()  // �� ������ ����A �ڷ�ƾ
    {
        yield return new WaitForSeconds(1f);    // 3�� ��� �� ���� ����

        float moveSpeed = 5f;   // �� �̵��ӵ�
        int spawnConut = 0;     // �� ���� ������ ���� ���̵� ����
        int enemyIndex = 0;     // �� ���� ������ ����
        while (true)    
        {
            foreach (float posy in arrPosY)    // ��� ��ǥ�� ���ʹ� ���� �Լ�
            {
                SpawnEnemy(posy, enemyIndex, moveSpeed);   // ���ʹ� ���� �Լ� ȣ��
            }

            spawnConut++;                   // ���ʹ� ���� ī��Ʈ
            if (spawnConut % 10 == 0)       // 10, 20, 30, ...
            {
                enemyIndex++;               // 10�� �������� ���� ���ʹ̷� 
                moveSpeed += 1f;              // �Ź� �ӵ� ���
            }
            if (enemyIndex >= enemies.Length)   // ���� �Ĺݺ� ������
            {
                SpawnBoss();
                enemyIndex = 0;                 // �ε��� �ʱ�ȭ�� ���� ���� ������
                moveSpeed = 5f;                 // �ӵ� �ʱ�ȭ(������������ �� ����������) 
            }
            yield return new WaitForSeconds(spawnInterval);    // 3�� ��� �� ���� ����
        }
    }

    void SpawnEnemy(float posy, int index, float moveSpeed)   // x��ǥ ��ġ����, �迭���� ���� ���� �ڸ���
    {
        Vector3 spawnPos = new Vector3(transform.position.x, posy, transform.position.z); // ���� ��ġ ����
        
        if (Random.Range(0, 9) == 0)    // �� �����  ���� ������
        {
            index++;                    // ��, 20���� Ȯ���� �⺻�� 4���� �� ���� �� ����
        }
        if (index >= enemies.Length)    // ���� �ڵ忡 ���� ������ �迭�� ũ�⸦ �Ѵ� ���� �߻���
        {
            index = enemies.Length - 1; // ������ ���� �ʰ� ó��. ��� ���� ���ؼ� �� ���� �� �ȳ���
        }
        // ����ǰ�� �����Ͽ� ���ӿ�����Ʈ�� ����
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        // Enermy Ŭ������ enermy ��ü���ٰ� ����ǰ�� ���ӿ�����Ʈȭ ���� �� ��ũ��Ʈ ������Ʈ�� ����
        Enermy enermy = enemyObject.GetComponent<Enermy>();
        // ��ũ��Ʈ�� ����� ��ü(���� ����ǰ)�� Enermy�� �Լ��� ����� �� �ִ�.
        enermy.SetMoveSpeed(moveSpeed);
    }
    
    void SpawnBoss()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}