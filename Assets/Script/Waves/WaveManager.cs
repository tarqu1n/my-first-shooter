using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] monsters;

    private int currentWave;
    void Start()
    {
        LoadWave(1);
        Debug.Log("here");
    }

    public void SpawnWave(WaveData waveData)
    {
        foreach(MonsterDatum monsterData in waveData.enemies)
        {
            foreach (GameObject monsterObject in monsters)
            {
                if (monsterObject.name == monsterData.type)
                {
                    for (int i = 0; i < monsterData.quantity; i++)
                    {
                         Instantiate(monsterObject, spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position, Quaternion.identity);
                    }
                }
            }
        }
    }

    void LoadWave(int waveNumber)
    {
        currentWave = 1;
        string fileName = $"WaveData/{waveNumber}.json";

        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            WaveData waveData = JsonUtility.FromJson<WaveData>(dataAsJson);
            SpawnWave(waveData);
        }
        else
        {
            Debug.LogError($"Cannot load wave {waveNumber} data!");
        }
    }
}

[System.Serializable]
public class MonsterDatum
{
    public string type;
    public int quantity;
}

[System.Serializable]
public class WaveData
{
    public string name;
    public List<MonsterDatum> enemies;
}