using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;  // 最初の位置を覚えておく

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // ドラッグ開始時の位置を記録
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;  // マウスに合わせて動く
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 敵にドロップされたかどうかを確認
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            ActivateCommand(hit.collider.gameObject);  // コマンド発動
            Destroy(gameObject);  // コマンドは発動後消える
        }
        else
        {
            transform.position = startPosition;  // 敵じゃない場所なら元に戻る
        }
    }

    void ActivateCommand(GameObject enemy)
    {
        Debug.Log("コマンド発動！ " + enemy.name);
        // ここに「単体攻撃」や「防御」の効果を入れる
    }

}
