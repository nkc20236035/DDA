using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCommandContlloer : MonoBehaviour
{
    private Vector3 initialPosition; // ドラッグ前の座標
    private bool isDragging = false; // ドラッグ中かどうか
    private Camera mainCamera; // カメラ参照

    public CommandData commandData;
    private int Baffer;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // 初期位置を記録

        if(commandData != null)
        {
            Baffer = commandData.jissuu;
        }
        else
        {
            Debug.LogError("PlayerCommandDataなし");
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -mainCamera.transform.position.z; // カメラとの距離を設定
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Enemyタグのオブジェクトと重なっているか判定
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, GetComponent<Collider2D>().bounds.size, 0, LayerMask.GetMask("Player"));
        if (hitCollider != null && hitCollider.CompareTag("Player"))
        {
            SimplePlayerController Player = hitCollider.GetComponent<SimplePlayerController>();
            Player.getHeal(Baffer);
            Destroy(gameObject); // 自分を破壊

        }
        else
        {
            transform.position = initialPosition; // 元の位置に戻す
        }
    }

    void OnDestroy()
    {
        CommandGenerator generator = FindObjectOfType<CommandGenerator>();
        if (generator != null)
        {
            generator.OnCommandDestroyed(gameObject);
        }
    }
}

