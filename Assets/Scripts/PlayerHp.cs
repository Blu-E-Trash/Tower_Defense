using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20;
    private float currentHp;

    public float MaxHP => maxHP;
    public float CurrentHp => currentHp;

    private void Awake()
    {
        currentHp = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if(currentHp <= 0) { 
        }
    }
}
