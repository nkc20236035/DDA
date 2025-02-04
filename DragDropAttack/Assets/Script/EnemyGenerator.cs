using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 10種類の敵プレハブ
    public int totalWaves = 5; // 合計ウェーブ数
    public Transform spawnPoint; // 画面左端のスポーン地点
    public Transform targetPoint; // 中央よりやや左の目標地点

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (currentWave < totalWaves)
        {
            yield return SpawnEnemies();
            yield return new WaitUntil(() => activeEnemies.Count == 0); // 全部倒されるまで待つ
            currentWave++;
        }

        Debug.Log("Battle Finished!");
    }

    IEnumerator SpawnEnemies()
    {
        int enemyCount = Random.Range(2, 4); // 2~3体生成
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; // ランダムな敵
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            activeEnemies.Add(enemy);
            StartCoroutine(MoveEnemy(enemy));
            yield return new WaitForSeconds(0.5f); // 0.5秒ごとに生成
        }
    }

    IEnumerator MoveEnemy(GameObject enemy)
    {
        float moveTime = 2.0f;
        float elapsedTime = 0f;
        Vector3 startPos = enemy.transform.position;
        Vector3 endPos = targetPoint.position;

        while (elapsedTime < moveTime)
        {
            enemy.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        enemy.transform.position = endPos;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Destroy(enemy);
    }
}
