using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    [Header("•‚—Vİ’è")]
    public float amplitude = 0.5f; // ã‰º‚ÌU‚ê•
    public float speed = 1f;       // “®‚­‘¬‚³

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position; // ‰ŠúˆÊ’u‚ğ•Û‘¶
    }

    private void Update()
    {
        // Sin”g‚ğg‚Á‚Äã‰º‚ÉˆÚ“®
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
