using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 prevPos; //�ۑ����Ă�������position
    private RectTransform rectTransform; // �ړ��������I�u�W�F�N�g��RectTransform
    private RectTransform parentRectTransform; // �ړ��������I�u�W�F�N�g�̐e(Panel)��RectTransform
    public RectTransform dropTarget; // �h���b�v��̘g

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent as RectTransform;
    }

    // �h���b�O�J�n���̏���
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �h���b�O�O�̈ʒu���L��
        prevPos = rectTransform.anchoredPosition;
    }

    // �h���b�O���̏���
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPosition = GetLocalPosition(eventData.position);
        rectTransform.anchoredPosition = localPosition;
    }

    // �h���b�O�I�����̏���
    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsPointerOverTarget(eventData))
        {
            // �g���Ƀh���b�v���ꂽ�ꍇ�A�g�̒����ɃX�i�b�v
            rectTransform.anchoredPosition = dropTarget.anchoredPosition;
        }
        else
        {
            // �g�O�̏ꍇ�A���̈ʒu�ɖ߂�
            rectTransform.anchoredPosition = prevPos;
        }
    }

    // ScreenPosition����localPosition�ւ̕ϊ��֐�
    private Vector2 GetLocalPosition(Vector2 screenPosition)
    {
        Vector2 result = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, Camera.main, out result);
        return result;
    }

    // �h���b�v��̘g���Ƀ|�C���^�����邩����
    private bool IsPointerOverTarget(PointerEventData eventData)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(dropTarget, eventData.position, Camera.main);
    }

}
