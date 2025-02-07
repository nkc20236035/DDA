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
            Debug.LogError("EnemyData‚È‚µ");
        }
    }



    void Update()
    {
        
    }
}
