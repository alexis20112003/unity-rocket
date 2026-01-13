using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public float timer;
    public float timerCoin;
    public float timerSpawn;
    public float timerSpawnCoin;
    public List<GameObject> obstacles = new();
    public GameObject coin;

    // Update is called once per frame
    void Update()
    {
        if (timerCoin >= timerSpawnCoin)
        {
            SpawnCoin();
            timerCoin = 0;
        }
        if (timer >= timerSpawn)
        {
            SpawnObstacles();
            timer = 0;
        }
        timer += Time.deltaTime;
        timerCoin += Time.deltaTime;
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

        private void SpawnCoin()
    {
        Instantiate(coin, transform.position, Quaternion.identity);
    }
}
