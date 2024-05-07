using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject      enemyPrefab; //적 프리팹
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private float           spawnTime; //생성 주기
    [SerializeField]
    private Transform[]     wayPoints; //현재 스테이지의 이동 경로
    [SerializeField]
    private PlayerHp playerHp;
    private List<Enemy>     enemyList;  //현재 맵에 존재하는 모든 적의 정보

    //적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요 없음
    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        //적 리스트 메모리 할당
        enemyList = new List<Enemy> ();
        //생성 코루틴 함수 호출
        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab); //적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>();// 방금 생성된 적의 enemy컴포넌트
            
            enemy.Setup(this,wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

            yield return new WaitForSeconds(spawnTime);
        }
    }
    public void DestroyEnemy(EnemyDestroyType type,Enemy enemy)
    {
        //적이 목표 지점까지 도착했을 때
        if(type == EnemyDestroyType.Arrive)
        {
            playerHp.TakeDamage(1);
        }
        //리스트에서 사망하는 적 정보 삭제
        enemyList.Remove(enemy);
        //적 오브젝트 삭제
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        //적 체력을 나타내는 Slider UI 생성
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        //Slider UI 오브젝트를 parent("Canvas"오브젝트)의 자식으로 설정
        //Tip.UI는 캔버스의 자식 오브젝트로 설정되어 있어야 화면에 보임
        sliderClone.transform.SetParent(canvasTransform);
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)fh tjfwjd
        sliderClone.transform.localScale = Vector3.one;

        //Slider UI가 쫒아다닐 대상을 본인으로 설정
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //Slider UI에 자신의 체력 정보를 표시
        sliderClone.GetComponent<EnemyHpViewer>().Setup(enemy.GetComponent<EnemyHp>());
    }
}
