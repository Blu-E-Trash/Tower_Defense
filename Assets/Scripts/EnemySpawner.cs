using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab; //�� ������
    [SerializeField]
    private float spawnTime; //���� �ֱ�
    [SerializeField]
    private Transform[] wayPoints; //���� ���������� �̵� ���

    private void Awake()
    {
        //���� �ڷ�ƾ �Լ� ȣ��
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab); //�� ������Ʈ ����
            Enemy enemy = clone.GetComponent<Enemy>();// ��� ������ ���� enemy������Ʈ
            enemy.Setup(wayPoints);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
