using UnityEngine;

public class Tower_Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private int towerBuildGold = 50;
    [SerializeField]
    private EnemySpawner enemySpawner;// 현재 맵에 존재하는 적 리스트 정보를 얻기 위해..
    [SerializeField]
    private PlayerGold playerGold;

    public void SpawnTower(Transform tileTransform)
    {
        //타워 건설 가능 여부 확인
        //1. 타워를 건설할 만큼 돈이 없으면 타워 건설 x
        if (towerBuildGold > playerGold.CurrentGold)
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();

        //타워 건설 가능 여부 확인
        //2. 현재 타일의 위치에 이미 타워가 건설되어 있으면 타워 건설 x
        if(tile.IsBuildTower == true )
        {
            return;
        }
        //타워가 건설되어 있으므로 설정
        tile.IsBuildTower = true;
        //타워 건설에 필요한 골드만큼 감소
        playerGold.CurrentGold -= towerBuildGold;
        //선택한 타일의 위치에 타워 건설(타일보타 z축 -1의 위치에 배치)
        Vector3 positison = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerPrefab,positison,Quaternion.identity);
        //타워 무기에 enemySpawner 정보 전달
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
}
