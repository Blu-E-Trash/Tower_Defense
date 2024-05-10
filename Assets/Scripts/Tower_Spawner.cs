using UnityEngine;

public class Tower_Spawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate towerTemplate;
    //[SerializeField]
    //private GameObject towerPrefab;
    //[SerializeField]
    //private int towerBuildGold = 50;
    [SerializeField]
    private EnemySpawner enemySpawner;// ���� �ʿ� �����ϴ� �� ����Ʈ ������ ��� ����..
    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private SystemTextViewer systemTextViewer;

    public void SpawnTower(Transform tileTransform)
    {
        //Ÿ�� �Ǽ� ���� ���� Ȯ��
        //1. Ÿ���� �Ǽ��� ��ŭ ���� ������ Ÿ�� �Ǽ� x
        if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)
        {
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();

        //Ÿ�� �Ǽ� ���� ���� Ȯ��
        //2. ���� Ÿ���� ��ġ�� �̹� Ÿ���� �Ǽ��Ǿ� ������ Ÿ�� �Ǽ� x
        if(tile.IsBuildTower == true )
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }
        //Ÿ���� �Ǽ��Ǿ� �����Ƿ� ����
        tile.IsBuildTower = true;
        //Ÿ�� �Ǽ��� �ʿ��� ��常ŭ ����
        playerGold.CurrentGold -= towerTemplate.weapon[0].cost;
        //������ Ÿ���� ��ġ�� Ÿ�� �Ǽ�(Ÿ�Ϻ�Ÿ z�� -1�� ��ġ�� ��ġ)
        Vector3 positison = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerTemplate.towerPrefab,positison,Quaternion.identity);
        //Ÿ�� ���⿡ enemySpawner ���� ����
        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold,tile);
    }
}
