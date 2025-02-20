using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ImageMove : MonoBehaviour
{

    public Vector3 positionA = new Vector3 (1000, 500, 0);
    public float moveSpeed = 3000;
    public GameObject EnemyGene;
    EnemyGenerator enemyGenerator;

    private Vector3 target;
    void Start()
    {
        target = positionA;
        enemyGenerator = EnemyGene.GetComponent<EnemyGenerator>();
    }
    void Update()
    {
        if (enemyGenerator.wonFlag == true && enemyGenerator.wonTimer <= 0)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target, moveSpeed * Time.deltaTime);
        }

    }
}
