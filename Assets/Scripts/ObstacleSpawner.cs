using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public float timer;
    public float timerSpawn;
    public List<GameObject> obstacles = new();

    // Update is called once per frame
    void Update()
    {
        if (timer >= timerSpawn)
        {
            print("spawn");
            SpawnObstacles();
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    private void SpawnObstacles()
    {
        if (obstacles.Count == 0) return;

        // 1️⃣ Choisir un index aléatoire
        int randomIndex = Random.Range(0, obstacles.Count);

        // 2️⃣ Récupérer le prefab correspondant
        GameObject obstacleRandom = obstacles[randomIndex];

        // 3️⃣ Instancier le prefab
        Instantiate(obstacleRandom, transform.position, Quaternion.identity);
    }
}
