using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "ScriptableObjects/EnemyData", order =1)]
public class EnemyData : ScriptableObject
{
    public string enemyName;    // “G‚Ì–¼‘O
    public int maxHP;           // Å‘åHP
    public int attackPower;     // UŒ‚—Í
    public int attackTime;      // UŒ‚‚Ü‚Å‚ÌŠÔ
}