using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;//�̵� ��� ����
    private Transform[] wayPoint;//�̵� ��� ����
    private int currentIndex = 0;//���� ��ǥ���� �δ���
    private MoveMent2D movement2D; //������Ʈ �̵� ����
    
    public void Setup(Transform[] wayPoint)
    {
        movement2D = GetComponent<MoveMent2D>();

        //�� �̵� ��� WayPoint ���� ����
        wayPointCount = wayPoint.Length;
        this.wayPoint = new Transform[wayPointCount];
        this.wayPoint = wayPoint;

        //���� ��ġ�� ù��° waypoint ��ġ�� ����
        transform.position = wayPoint[currentIndex].position;

        //���� �̵� ��ǥ���� ���� �ڷ�ƾ �Լ� ����
        StartCoroutine("OnMove");
    }

    // Update is called once per frame
    private IEnumerator Onmove()
    {
        //���� �̵����� ����
        NextMoveTo();

        while(true){
            //�� ������Ʈ ȸ��
            transform.Rotate(Vector3.forward * 10);

            //���� ������ġ�� ��ǥ��ġ�� �Ÿ��� 0.02*movement2D.MoveSpeed���� ���� �� if ����
            //movement2D.MoveSpeed�� ���ϴ� ������ �ӵ��� ������ �� �����ӿ� 0.02���� ũ�� �����̱� ����
            //if���ǹ��� �ɸ��� �ʰ� ��θ� Ż���ϴ� ������Ʈ�� �߻��� �� ����
            if (Vector3.Distance(transform.position, wayPoint[currentIndex].position)<0.02f* movement2D.MoveSpeed)
            {
                //���� �̵� ���� ����
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        //���� �̵��� waypoint�� ����������
        if (currentIndex < wayPointCount - 1)
        {
            //���� ��ġ�� ��Ȯ�ϰ� ��ǥ ��ġ�� ����
            transform.position = wayPoint[currentIndex].position;
            //�̵� ���� ����  -> ���� ��ǥ����
            currentIndex++;
            Vector3 direction = (wayPoint[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //���� ��ġ�� ������ waypoint��
        else
            Destroy(gameObject);
    }
}
