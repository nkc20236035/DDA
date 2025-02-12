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
    private int Damage = 0;
    private float Timer = 0f;

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

    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;

            if(Timer <= 0 && Damage > 0)
            {
                TakeDamage(Damage);
                Damage = 0;
            }
        }
    }

    public void SetAttack(int damage,float time)
    {
        Damage = damage;
        Timer = time;
    }

    public void TakeDamage(int damage)
    {
        
         currentHP -= damage;
         Debug.Log($"{enemyData.enemyName} �� {damage} �_���[�W���󂯂�");
        if (currentHP <= 0)
        {
            Die();
        }

        
    }

    void Die()
    {
        Debug.Log("���S");
        EnemyGene.OnEnemyDestroyed(); // EnemyGenerator��OnEnemyDestroyed�N���X���Ăяo��
        Destroy(gameObject);
    }


    
}
