using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandContlloer : MonoBehaviour
{
    private Vector3 initialPosition; // ドラッグ前の座標
    private bool isDragging = false; // ドラッグ中かどうか
    private Camera mainCamera; // カメラ参照
    StageManager stageManager;

    public CommandData commandData;
    private int Attack;
    private float AttackTime;
    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // 初期位置を記録
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        if(commandData != null )
        {
            Attack = commandData.jissuu;
            AttackTime = commandData.Time;
        }
        else
        {
            Debug.LogError("CommandDataなし");
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
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, GetComponent<Collider2D>().bounds.size, 0,LayerMask.GetMask("Enemy"));
        if (hitCollider != null && hitCollider.CompareTag("Enemy"))
        {
            EnemyContlloer enemy = hitCollider.GetComponent<EnemyContlloer>();
            if (enemy != null && stageManager.Commandflag == true)
            {
                Debug.Log("敵検知");
                enemy.SetAttack(Attack,AttackTime); // EnemyContlloerのTakeDamageに代入
                stageManager.getTime(AttackTime);
                stageManager.Commandflag = false;
                Destroy(gameObject); // 自分を破壊
            }
            else
            {
                transform.position = initialPosition; // 元の位置に戻す
            }
            
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

