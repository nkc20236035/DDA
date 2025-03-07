using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [Header("浮遊設定")]
    public float amplitude = 0.5f; // 上下の振れ幅
    public float speed = 1f;       // 動く速さ

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // 初期位置を保存
    }

    private void Update()
    {
        // Sin波を使って上下に移動
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
