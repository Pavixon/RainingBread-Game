using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] foodPrefab;
    private float spawnRate=1;
    private float spawnerPosition;
    private int prefabRoll;
    private float[] spawnSlots = new float[] { -6f, 5f, -4f, -4f, -2f, -1f, 0f, 1f, 2f, 3f, 4f, 5f, 6f };
    public LogicScript logic;
    private float gameTime;
    private float lastSpawnSlot = 0;

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
            if (spawnerPosition != lastSpawnSlot)
            {
                transform.position = new Vector3(spawnerPosition, 7, 0);
                lastSpawnSlot = spawnerPosition;
            }
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
        float rate = 1.5f - (gameTime / 150f); 
        return Mathf.Clamp(rate, 0.5f, 1.5f);  
    }

    public void SetSpawnerPosition()
    {
        float newSlot;
        do
        {
            newSlot = spawnSlots[Random.Range(0, spawnSlots.Length)];
        } while (newSlot == lastSpawnSlot);

        spawnerPosition = newSlot;
    }

    public void SetPrefab()    
    {
        prefabRoll = Random.Range(0, foodPrefab.Length);
    }

}
