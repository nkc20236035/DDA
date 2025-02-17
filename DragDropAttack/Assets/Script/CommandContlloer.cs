using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CommandContlloer : MonoBehaviour
{
    private Vector3 initialPosition; // �h���b�O�O�̍��W
    private bool isDragging = false; // �h���b�O�����ǂ���
    private Camera mainCamera; // �J�����Q��
    StageManager stageManager;

    public CommandData commandData;
    private int Attack;
    private float AttackTime;
    void Start()
    {
        mainCamera = Camera.main;
        initialPosition = transform.position; // �����ʒu���L�^
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();

        if(commandData != null )
        {
            Attack = commandData.jissuu;
            AttackTime = commandData.Time;
        }
        else
        {
            Debug.LogError("CommandData�Ȃ�");
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
        Collider2D hitCollider = Physics2D.OverlapBox(transform.position, GetComponent<Collider2D>().bounds.size, 0,LayerMask.GetMask("Enemy"));
        if (hitCollider != null && hitCollider.CompareTag("Enemy"))
        {
            EnemyContlloer enemy = hitCollider.GetComponent<EnemyContlloer>();
            if (enemy != null && stageManager.Commandflag == true)
            {
                Debug.Log("�G���m");
                enemy.SetAttack(Attack,AttackTime); // EnemyContlloer��TakeDamage�ɑ��
                stageManager.getTime(AttackTime);
                stageManager.Commandflag = false;
                Destroy(gameObject); // ������j��
            }
            else
            {
                transform.position = initialPosition; // ���̈ʒu�ɖ߂�
            }
            
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

