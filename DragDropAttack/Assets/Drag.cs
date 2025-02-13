using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 prevPos; //保存しておく初期position
    private RectTransform rectTransform; // 移動したいオブジェクトのRectTransform
    private RectTransform parentRectTransform; // 移動したいオブジェクトの親(Panel)のRectTransform
    public RectTransform dropTarget; // ドロップ先の枠

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
    }

    // ドラッグ開始時の処理
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグ前の位置を記憶
        prevPos = rectTransform.anchoredPosition;
    }

    // ドラッグ中の処理
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPosition = GetLocalPosition(eventData.position);
        rectTransform.anchoredPosition = localPosition;
    }

    // ドラッグ終了時の処理
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsPointerOverTarget(eventData))
        {
            // 枠内にドロップされた場合、枠の中央にスナップ
            rectTransform.anchoredPosition = dropTarget.anchoredPosition;
        }
        else
        {
            // 枠外の場合、元の位置に戻す
            rectTransform.anchoredPosition = prevPos;
        }
    }

    // ScreenPositionからlocalPositionへの変換関数
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);
        return result;
    }

    // ドロップ先の枠内にポインタがあるか判定
    private bool IsPointerOverTarget(PointerEventData eventData)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(dropTarget, eventData.position, Camera.main);
    }

}
