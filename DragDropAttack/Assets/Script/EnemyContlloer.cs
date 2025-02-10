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
            Debug.LogError("EnemyData�Ȃ�");
        }
    }

    public void TakeDamage(int damage,float time)
    {
        Debug.Log(time);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            currentHP -= damage;
            Debug.Log($"{enemyData.enemyName} �� {damage} �_���[�W���󂯂�");
            if(currentHP <= 0)
            {
                Die();
            }
        }

        
    }

    void Die()
    {
        Debug.Log("���S");
        EnemyGene.OnEnemyDestroyed(); // EnemyGenerator��OnEnemyDestroyed�N���X���Ăяo��
        Destroy(gameObject);
    }


    void Update()
    {
        
    }
}
