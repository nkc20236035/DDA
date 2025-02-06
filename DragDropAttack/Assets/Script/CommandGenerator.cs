using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandGenerator : MonoBehaviour
{
    public GameObject[] commandPrefabs;   // コマンドプレハブいれるとこ
    public Transform spawnArea;
    public int commandCount = 3;          // 表示する最大コマンド数
    public float spacing = 2f;
    
    private List<GameObject> activeCommands = new List<GameObject>();

    void Start()
    {
        GenerateCommands();
    }

    void GenerateCommands()
    {
        if(commandPrefabs.Length == 0)
        {
            Debug.LogError("コマンド設置されてない");
            return;
        }

        foreach( var cmd in activeCommands )
        {
            if (cmd != null) Destroy(cmd);
        }
        activeCommands.Clear();


        float startX = -((commandCount - 1) * spacing) / 2;
        for(int i = 0; i < commandCount; i++)
        {
            int randIndex = Random.Range(0,commandPrefabs.Length);
            Vector3 spawnPosition = new Vector3(startX + (i * spacing), spawnArea.position.y, 0);
            GameObject newCommand = Instantiate(commandPrefabs[randIndex], spawnPosition, Quaternion.identity, spawnArea);
            activeCommands.Add(newCommand);
        }
    }
    public void OnCommandDestroyed(GameObject command)
    {
        activeCommands.Remove(command);
        if(activeCommands.Count == 0)
        {
            GenerateCommands();
        }
    }
}
