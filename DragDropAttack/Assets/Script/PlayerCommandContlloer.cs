using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCommandContlloer : MonoBehaviour
{
    private Vector3 initialPosition; // �h���b�O�O�̍��W
    private bool isDragging = false; // �h���b�O�����ǂ���
    private Camera mainCamera; // �J�����Q��

    public CommandData commandData;
    private int Baffer;

    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // �����ʒu���L�^

        if(commandData != null)
        {
            Baffer = commandData.jissuu;
        }
        else
        {
            Debug.LogError("PlayerCommandData�Ȃ�");
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
            mousePosition.z = -mainCamera.transform.position.z; // �J�����Ƃ̋�����ݒ�
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Enemy�^�O�̃I�u�W�F�N�g�Əd�Ȃ��Ă��邩����
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, GetComponent<Collider2D>().bounds.size, 0, LayerMask.GetMask("Player"));
        if (hitCollider != null && hitCollider.CompareTag("Player"))
        {
            SimplePlayerController Player = hitCollider.GetComponent<SimplePlayerController>();
            Player.getHeal(Baffer);
            Destroy(gameObject); // ������j��

        }
        else
        {
            transform.position = initialPosition; // ���̈ʒu�ɖ߂�
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

