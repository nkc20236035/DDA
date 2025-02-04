using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 10��ނ̓G�v���n�u
    public int totalWaves = 5; // ���v�E�F�[�u��
    public Transform spawnPoint; // ��ʍ��[�̃X�|�[���n�_
    public Transform targetPoint; // ��������⍶�̖ڕW�n�_

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
            yield return new WaitUntil(() => activeEnemies.Count == 0); // �S���|�����܂ő҂�
            currentWave++;
        }

        Debug.Log("Battle Finished!");
    }

    IEnumerator SpawnEnemies()
    {
        int enemyCount = Random.Range(2, 4); // 2~3�̐���
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]; // �����_���ȓG
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            activeEnemies.Add(enemy);
            StartCoroutine(MoveEnemy(enemy));
            yield return new WaitForSeconds(0.5f); // 0.5�b���Ƃɐ���
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
