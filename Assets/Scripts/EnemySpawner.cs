using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject      enemyPrefab; //�� ������
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    //[SerializeField]
    //private float           spawnTime; //���� �ֱ�
    [SerializeField]
    private Transform[]     wayPoints; //���� ���������� �̵� ���
    [SerializeField]
    private PlayerHp playerHp;
    [SerializeField]
    private PlayerGold playerGold;
    private Wave currentWave;//���� ���̺� ����
    private int currentEnemyCount; //���� ���̺꿡 �����ִ� �� ����(���۽� max, ����� -1)
    private List<Enemy>     enemyList;  //���� �ʿ� �����ϴ� ��� ���� ����

    //���� ������ ������ EnemySpawner���� �ϱ� ������ Set�� �ʿ� ����
    public List<Enemy> EnemyList => enemyList;
    //���� ���̺꿡 �����ִ� ��, �ִ� �� ����
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.maxEnemyCount;

    private void Awake()
    {
        //�� ����Ʈ �޸� �Ҵ�
        enemyList = new List<Enemy> ();
        //���� �ڷ�ƾ �Լ� ȣ��
        //StartCoroutine("SpawnEnemy");
    }
    public void StartWave(Wave wave)
    {
        //�Ű������� �޾ƿ� ���̺� ���� ����
        currentWave = wave;
        //���� ���̺��� �ִ� �� ���ڸ� ����
        currentEnemyCount = currentWave.maxEnemyCount;
        //���� ���̺� ����
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        //���� ���̺꿡�� ������ �� ����
        int spawnEnemyCount = 0;

        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            //GameObject clone = Instantiate(enemyPrefab); //�� ������Ʈ ����
            int enemyIndex = Random.Range(0,currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();// ��� ������ ���� enemy������Ʈ
            
            enemy.Setup(this,wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);
            //���� ���̺꿡�� ������ ���� ���� +1
            spawnEnemyCount++;

            //yield return new WaitForSeconds(spawnTime);
            //�� ���̺긶�� spawnTime�� �ٸ� �� �ֱ� ������ ���� ���̺�(currentWave)�� spawnTime ���
            yield return new WaitForSeconds(currentWave.spawnTime);//
        }
    }
    public void DestroyEnemy(EnemyDestroyType type,Enemy enemy,int gold)
    {
        //���� ��ǥ �������� �������� ��
        if(type == EnemyDestroyType.Arrive)
        {
            playerHp.TakeDamage(1);
        }
        else if (type == EnemyDestroyType.kill)
        {
            playerGold.CurrentGold += gold;
        }
        currentEnemyCount--;
        //����Ʈ���� ����ϴ� �� ���� ����
        enemyList.Remove(enemy);
        //�� ������Ʈ ����
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        //�� ü���� ��Ÿ���� Slider UI ����
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI ������Ʈ�� parent("Canvas"������Ʈ)�� �ڽ����� ����
        //Tip.UI�� ĵ������ �ڽ� ������Ʈ�� �����Ǿ� �־�� ȭ�鿡 ����
        sliderClone.transform.SetParent(canvasTransform);
        //���� �������� �ٲ� ũ�⸦ �ٽ� (1,1,1)fh tjfwjd
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI�� �i�ƴٴ� ����� �������� ����
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Slider UI�� �ڽ��� ü�� ������ ǥ��
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHp>());
    }
}
