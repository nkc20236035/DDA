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
    EnemyGenerator EnemyGene;

    void Start()
    {
        EnemyGene = GameObject.Find("EnemyGene").GetComponent<EnemyGenerator>();

        if (enemyData != null)
        {
            currentHP = enemyData.maxHP;
            currentAttack = enemyData.attackPower;
        }
        else
        {
            Debug.LogError("EnemyDataなし");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        Debug.Log($"{enemyData.enemyName} は {damage} ダメージを受けた");
        if(currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("死亡");
        EnemyGene.OnEnemyDestroyed(); // EnemyGeneratorのOnEnemyDestroyedクラスを呼び出す
        Destroy(gameObject);
    }


    void Update()
    {
        
    }
}
