using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnemyContlloer : MonoBehaviour
{
    public EnemyData enemyData;

    private int currentHP;
    private int currentAttack;

    void Start()
    {

        if (enemyData != null)
        {
            currentHP = enemyData.maxHP;
            currentAttack = enemyData.attackPower;
        }
        else
        {
            Debug.LogError("EnemyDataÇ»Çµ");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{enemyData.enemyName} ÇÕ {damage} É_ÉÅÅ[ÉWÇéÛÇØÇΩ");
        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("éÄñS");
        Destroy(gameObject);
    }


    void Update()
    {
        
    }
}
