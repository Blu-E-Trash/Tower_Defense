using UnityEngine;

public class MoveMent2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed; // moveSpeed 변수의 프로퍼티

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
