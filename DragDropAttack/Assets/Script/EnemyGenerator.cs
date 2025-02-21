using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefab; // �G�I�u�W�F�N�g��ݒ肷��Ƃ���
    public Transform spawnPoint;     // �G�����������ꏊ
    public int waveCount = 5;        // �o�g���̃E�F�[�u��
    public float spawnDelay = 0.5f;  // �G�̏o���x��
    public float moveSpeed = 5f;     // �G�̈ړ����x
    public Text text;
    public Image finish;

    private int currentWave = 0;     // ���݂̃E�F�[�u
    private int totalEnemiesInWave = 0;  // 1�E�F�[�u�̓G�̐�

    ImageMove imagemove;
    StageManager stagemanager;

    private Vector3[] spawnOffsets =
    {
        new Vector3(4f,-1f,0f),
        new Vector3(6f,0f,0f),
        new Vector3(8f,1f,0f),
    };

    public float wonTimer = 0f;
    public bool wonFlag = false;
    
    void Start()
    {
        StartNextWave();
        Color color = finish.color;
        color.a = 0f;
        finish.color = color;
        imagemove = GameObject.Find("Won").GetComponent<ImageMove>();
        stagemanager = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    void StartNextWave()
    {
        if(currentWave < waveCount)
        {
            currentWave++;
            text.text = "Wave " + currentWave +"/5";
            totalEnemiesInWave = Random.Range(2, 4);   // �e�E�F�[�u2�`3�̂̓G�������_���Ő���
            SpawnEnemies();
        }
        else
        {
            Debug.Log("Battle Finished!");
            Color color = finish.color;
            color.a = 1f;
            finish.color = color;
            stagemanager.finish();
            wonTimer = 3f;
        }
    }

    void SpawnEnemies() // �G�̐�������
    {
        for(int i =0; i < totalEnemiesInWave; i++)
        {
            GameObject enemy = Instantiate(GetRandomEnemy(), spawnPoint.position, Quaternion.identity);
            Vector3 offset = spawnOffsets[i];
            Vector3 targetPoint = spawnPoint.position + offset;

            StartCoroutine(MoveEnemyToPosition(enemy, targetPoint));
        }
    }

    GameObject GetRandomEnemy()
    {
        int randdomIndex = Random.Range(0, enemyPrefab.Length);
        return enemyPrefab[randdomIndex];
    }

    System.Collections.IEnumerator MoveEnemyToPosition(GameObject enemy, Vector3 targetPoint)
    {
        float journeyLength = Vector3.Distance(enemy.transform.position, targetPoint);
        float startTime = Time.time;

        while(Vector3.Distance(enemy.transform.position,targetPoint)>0.1f)
        {
            float distanceCoverd = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCoverd / journeyLength;
            enemy.transform.position = Vector3.Lerp(enemy.transform.position, targetPoint, fractionOfJourney);
            yield return null;
        }

        Debug.Log("Enemy reached target!");
    }



    public void OnEnemyDestroyed()
    {
        totalEnemiesInWave--;
        if(totalEnemiesInWave <= 0)
        {
            StartNextWave();
        }
    }

    void Update()
    {
        if(wonTimer > 0)
        {
            wonTimer -= Time.deltaTime;
            if(wonTimer <= 0 )
            {
                Color color = finish.color;
                color.a = 0;
                finish.color = color;
                wonFlag = true;
            }
        }
    }

}
