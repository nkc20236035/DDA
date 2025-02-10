using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCommandData", menuName = "CommandObjects/CommandData", order =1)]
public class CommandData : ScriptableObject
{
    public string CommandName;  // コマンド名
    public int jissuu;          // 効果
    public float Time;            // 攻撃発生時間

}
