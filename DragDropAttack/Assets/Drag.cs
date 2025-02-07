using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;  // �ŏ��̈ʒu���o���Ă���

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;  // �h���b�O�J�n���̈ʒu���L�^
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;  // �}�E�X�ɍ��킹�ē���
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �G�Ƀh���b�v���ꂽ���ǂ������m�F
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            ActivateCommand(hit.collider.gameObject);  // �R�}���h����
            Destroy(gameObject);  // �R�}���h�͔����������
        }
        else
        {
            transform.position = startPosition;  // �G����Ȃ��ꏊ�Ȃ猳�ɖ߂�
        }
    }

    void ActivateCommand(GameObject enemy)
    {
        Debug.Log("�R�}���h�����I " + enemy.name);
        // �����Ɂu�P�̍U���v��u�h��v�̌��ʂ�����
    }

}
