using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData", order =1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;    // �G�̖��O
    public int maxHP;           // �ő�HP
    public int attackPower;     // �U����
    public int attackTime;      // �U���܂ł̎���
}