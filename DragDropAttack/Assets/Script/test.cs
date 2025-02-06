using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    EnemyGenerator EG;
    // Start is called before the first frame update
    void Start()
    {
        EG = GameObject.Find("EnemyGene").GetComponent<EnemyGenerator>(); 
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "com")
        {
            EG.OnEnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
