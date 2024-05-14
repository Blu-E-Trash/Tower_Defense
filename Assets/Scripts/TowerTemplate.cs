using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab;//Ÿ�� ������ ���� ������
    public Weapon[] weapon;//������ Ÿ��(����)����

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite;
        public float damage;
        public float rate;
        public float range;
        public int cost;
        public int sell;
    }
}