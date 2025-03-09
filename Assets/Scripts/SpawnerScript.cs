using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] foodPrefab;
    public float spawnRate;
    private float spawnerPosition;
    private int prefabRoll;
    public LogicScript logic;

    void Start()
    {
        SetSpawnerPosition();
    }

    void Update()
    {
        if (logic.gameScreen.activeSelf)
        {
            transform.position = new Vector3(spawnerPosition, 7, 0);
            spawnRate -= Time.deltaTime;
        }
        if (spawnRate <= 0)
        {
            SetPrefab();
            Instantiate(foodPrefab[prefabRoll], transform.position, Quaternion.identity);
            SetSpawnerPosition();
            if(logic.playerScore < 10)
            {
                spawnRate = 1.1f;
            }
            else if (logic.playerScore >= 10 && logic.playerScore < 40)
            {
                spawnRate = 1f;
            }
            else if (logic.playerScore >= 40 && logic.playerScore < 60)
            {
                spawnRate = 0.8f;
            }
            else if (logic.playerScore >= 60 && logic.playerScore < 80)
            {
                spawnRate = 0.6f;
            }
            else
            {
                spawnRate = 0.5f;
            }
        }
    }

    public void SetSpawnerPosition()
    {
        spawnerPosition = Random.Range(-7.5f, 7.5f);
    }

    public void SetPrefab()
    {
        prefabRoll = Random.Range(0, 2);
    }

}
