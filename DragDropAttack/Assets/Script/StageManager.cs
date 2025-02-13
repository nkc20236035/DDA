using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private float timer = 0;
    public GameObject EnemyGene;
    public GameObject CommandGene;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 4)
        {
            EnemyGene.SetActive(true);
            CommandGene.SetActive(true);
        }

    }
}
