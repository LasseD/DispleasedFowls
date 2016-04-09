using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Bird birdToClone;

    private int enemiesRemainingToSpawn;
    private float nextSpawnTime;
    private Wave currentWave;
    private int currentWaveNumber;
    private int enemiesRemainingAlive;

    void Start()
    {
        currentWaveNumber = -1;
    }

    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            --enemiesRemainingToSpawn;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            Bird spawnedBird = Instantiate(birdToClone, Vector3.zero, Quaternion.identity) as Bird;

            //spawnedBird.OnDeath += OnBirdDeath;
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

    internal void NextWave()
    {
        ++currentWaveNumber;
        print("Wave " + currentWaveNumber + "starting!");
        if (currentWaveNumber >= waves.Length)
            currentWaveNumber = 0;
        currentWave = waves[currentWaveNumber];
        enemiesRemainingToSpawn = currentWave.BirdCount;
        enemiesRemainingAlive = currentWave.BirdCount;
        var airship = GameObject.FindGameObjectWithTag("Airship").GetComponent<Airship>();
        SpawnEnemies(airship.boxCollider.offset);
    }

    private void SpawnEnemies(Vector2 ctr)
    {
        Instantiate(birdToClone, Vector3.zero, Quaternion.identity);
        var sigma = MakeRandomFloat(0, TAU);
        var r = MakeRandomFloat(SpawnPointMinDistance, SpawnPointMaxDistance);
        var delta = VectorRadian(sigma, r);
        Instantiate(birdToClone, ctr + delta, Quaternion.identity);
    }

    private const double TAU = System.Math.PI * 2;
    private static double RandomFloat(System.Random r, double a, double b) {
        if (b <= a) return a; // <- Actually this case is undefined (if a!=b).
        var delta = b - a;
        var k = r.NextDouble() ; // <- k \in [0,1]
        return k * delta + a;
    }
    private static double MakeRandomFloat(double a, double b) {
        // Should we be using Unity.Random?
        return RandomFloat(new System.Random(), a, b);
    }
    private const float SpawnPointMinDistance = 42f;
    private const float SpawnPointMaxDistance = 1337f;
    private static Vector2 VectorRadian(double sigma, double r) {
        var x = System.Math.Sin(sigma) * r;
        var y = System.Math.Cos(sigma) * r;
        return new Vector2((float)x, (float)y);
    }

    [System.Serializable]
    public class Wave
    {
        public int BirdCount;
        public float timeBetweenSpawns;
    }
}
