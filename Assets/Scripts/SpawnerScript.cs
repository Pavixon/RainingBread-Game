using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] foodPrefab;
    private float spawnRate=1;
    private float spawnerPosition;
    private int prefabRoll;
    private float[] spawnSlots = new float[] { -6f, -4f, -2f, 0f, 2f, 4f, 6f };
    public LogicScript logic;
    private float gameTime;

    void Start()
    {
        GetSpawnRate();
        SetSpawnerPosition();
        gameTime = 0f;
        prefabRoll = 0;
    }

    void Update()
    {
        if (logic.gameScreen.activeSelf)
        {
            gameTime += Time.deltaTime;
            transform.position = new Vector3(spawnerPosition, 7, 0);
            spawnRate -= Time.deltaTime;
        }
        if (spawnRate <= 0)
        {
            SetPrefab();
            Instantiate(foodPrefab[prefabRoll], transform.position, Quaternion.identity);
            SetSpawnerPosition();
            spawnRate = GetSpawnRate();
        }
    }

    private float GetSpawnRate()
    {
        // Example: spawn rate decreases gradually over 120 seconds
        // Initial rate: 1.5s → minimum rate: 0.4s
        float rate = 1.5f - (gameTime / 120f); // decrease rate over 2 minutes
        return Mathf.Clamp(rate, 0.4f, 1.5f);  // prevent going below 0.4s
    }

    public void SetSpawnerPosition()
    {
        spawnerPosition = spawnSlots[Random.Range(0, spawnSlots.Length)];
    }

    public void SetPrefab()    
    {
        prefabRoll = Random.Range(0, foodPrefab.Length);
    }

}
