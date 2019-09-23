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
    public WaveState waveState;
    private SpawnPointController[] spawnPointControllers;
    void Start()
    {
        spawnPointControllers = new SpawnPointController[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointControllers[i] = spawnPoints[i].GetComponent<SpawnPointController>();
        }

        LoadWave(1);
    }

    private void AssignToSpawnPoints()
    {

    }

    public void SpawnWave(WaveData waveData)
    {

        foreach (MonsterDatum monsterData in waveData.enemies)
        {
            foreach (GameObject monsterObject in monsters)
            {
                if (monsterObject.name == monsterData.type)
                {
                    for (int i = 0; i < monsterData.quantity; i++)
                    {
                        spawnPointControllers[Random.Range(0, spawnPoints.Length)].Assign(monsterObject, monsterData.stage, 1);
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
        StartCoroutine(loadStreamingAsset(filePath));


    }
    IEnumerator loadStreamingAsset(string filePath)
    {
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            WWW www = new WWW(filePath);
            yield return www;
            WaveData waveData = JsonUtility.FromJson<WaveData>(www.text);
            SpawnWave(waveData);
        }
        else
        {
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
                Debug.LogError($"Cannot load {filePath} data!");
            }
        }
    }
}

[System.Serializable]
public class MonsterDatum
{
    public string type;
    public int stage;
    public int quantity;
}

[System.Serializable]
public class WaveData
{
    public string name;
    public List<MonsterDatum> enemies;
}

public enum WaveState
{
    Spawning,
    Complete
}