using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSetButton : MonoBehaviour
{
    public string targetTag = "Command"; // 削除したいオブジェクトのタグ

    public void DestroyAllWithTag()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag); // タグのついたオブジェクトを取得

        foreach (GameObject obj in objects)
        {
            Destroy(obj); // オブジェクトを削除
        }
    }
}
