using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Bird bird;

    private int enemiesRemainingToSpawn;
    private float nextSpawnTime;
    private Wave currentWave;
    private int currentWaveNumber;
    private int enemiesRemainingAlive;

    void Start()
    {
        currentWaveNumber = -1;
        NextWave();
    }

    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            --enemiesRemainingToSpawn;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
            Bird spawnedBird = Instantiate(bird, Vector3.zero, Quaternion.identity) as Bird;
            spawnedBird.OnDeath += OnBirdDeath;
        }
    }

    private void OnBirdDeath()
    {
        --enemiesRemainingAlive;
        if (enemiesRemainingAlive <= 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        ++currentWaveNumber;
        print("Wave " + currentWaveNumber + "starting!");
        if (currentWaveNumber >= waves.Length)
            currentWaveNumber = 0;
        currentWave = waves[currentWaveNumber];
        enemiesRemainingToSpawn = currentWave.BirdCount;
        enemiesRemainingAlive = currentWave.BirdCount;
    }

    [System.Serializable]
    public class Wave
    {
        public int BirdCount;
        public float timeBetweenSpawns;
    }
}
