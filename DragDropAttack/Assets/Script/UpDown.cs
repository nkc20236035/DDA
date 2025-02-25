using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [Header("���V�ݒ�")]
    public float amplitude = 0.5f; // �㉺�̐U�ꕝ
    public float speed = 1f;       // ��������

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // �����ʒu��ۑ�
    }

    private void Update()
    {
        // Sin�g���g���ď㉺�Ɉړ�
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
