using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefab; // 敵オブジェクトを設定するところ
    public Transform spawnPoint;     // 敵が生成される場所
    public int waveCount = 5;        // バトルのウェーブ数
    public float spawnDelay = 0.5f;  // 敵の出現遅延
    public float moveSpeed = 5f;     // 敵の移動速度

    private int currentWave = 0;
    private int totalEnemiesInWave = 0;  // 1ウェーブの敵の数

    private Vector3[] spawnOffsets =
    {
        new Vector3(-2f,1f,0f),
        new Vector3(0f,2f,0f),
        new Vector3(2f,3f,0f),
    };

    void Start()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        if(currentWave < waveCount)
        {
            currentWave++;
            totalEnemiesInWave = Random.Range(2, 4);   // 各ウェーブ2～3体の敵をランダムで生成
            SpawnEnemies();
        }
        else
        {
            Debug.Log("Battle Finished!");
        }
    }

    void SpawnEnemies() // 敵の生成処理
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
}
