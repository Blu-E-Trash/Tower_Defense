using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;//이동 경로 개수
    private Transform[] wayPoint;//이동 경로 정보
    private int currentIndex = 0;//현재 목표지점 인덕스
    private MoveMent2D movement2D; //오브젝트 이동 제어
    
    public void Setup(Transform[] wayPoint)
    {
        movement2D = GetComponent<MoveMent2D>();

        //적 이동 경로 WayPoint 정보 설정
        wayPointCount = wayPoint.Length;
        this.wayPoint = new Transform[wayPointCount];
        this.wayPoint = wayPoint;

        //적의 위치를 첫번째 waypoint 위치로 설정
        transform.position = wayPoint[currentIndex].position;

        //적의 이동 목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }

    // Update is called once per frame
    private IEnumerator Onmove()
    {
        //다음 이동방향 설정
        NextMoveTo();

        while(true){
            //적 오브젝트 회전
            transform.Rotate(Vector3.forward * 10);

            //적의 현재위치와 목표위치의 거리가 0.02*movement2D.MoveSpeed보다 적을 때 if 실행
            //movement2D.MoveSpeed를 곱하는 이유는 속도가 빠르면 한 프레임에 0.02보다 크게 움직이기 때문
            //if조건문에 걸리지 않고 경로를 탈주하는 오브젝트가 발생할 수 있음
            if (Vector3.Distance(transform.position, wayPoint[currentIndex].position)<0.02f* movement2D.MoveSpeed)
            {
                //다음 이동 방향 설정
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        //아직 이동할 waypoint가 남아있으면
        if (currentIndex < wayPointCount - 1)
        {
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoint[currentIndex].position;
            //이동 방향 설정  -> 다음 목표지점
            currentIndex++;
            Vector3 direction = (wayPoint[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        //현재 위치가 마지막 waypoint면
        else
            Destroy(gameObject);
    }
}
