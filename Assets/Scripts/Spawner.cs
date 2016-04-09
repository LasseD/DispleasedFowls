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
        currentWaveNumber = 0;
        enemiesRemainingToSpawn = 0;
        nextSpawnTime = 0;
    }

    void Update()
    {
        //print(string.Format("enemiesRemainingToSpawn: {0}", enemiesRemainingToSpawn));
        //print(string.Format("nextSpawnTime: {0}", nextSpawnTime));
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            --enemiesRemainingToSpawn;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

            SpawnBird();
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
        print("Wave " + currentWaveNumber + " starting!");
        if (currentWaveNumber >= waves.Length)
            currentWaveNumber = 0;
        currentWave = waves[currentWaveNumber];
        enemiesRemainingToSpawn = currentWave.BirdCount;
        enemiesRemainingAlive = currentWave.BirdCount;
        ++currentWaveNumber;
    }

    private void SpawnBird() {
        var airship = GameManager.instance.getAirship();
        SpawnBirdAt(airship.BoxCollider.offset);
    }

    private void SpawnBirdAt(Vector2 ctr)
    {
        var sigma = MakeRandomFloat(0, TAU);
        var r = MakeRandomFloat(SpawnPointMinDistance, SpawnPointMaxDistance);
        var delta = VectorRadian(sigma, r);
        var bird = Instantiate(birdToClone, ctr + delta, Quaternion.identity) as Bird;
        bird.OnDone += OnBirdDeath;
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
    public float SpawnPointMinDistance = 12f;
    public float SpawnPointMaxDistance = 13f;
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
