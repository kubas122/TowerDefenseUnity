using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f; // Time beetween waves
    private float countdown = 2f;
    private int waveIndex = 0; // wave number

    public GameManager gameManager;

    public float difficultyMultiplier = 1.05f;  // increase enemy difficulty
    public int baseEnemyCount = 5; // number if enemies in first wave

    public Transform normalEnemyPrefab;
    public Transform tankEnemyPrefab;
    public Transform fastEnemyPrefab;

    public Transform bossPrefab;

    public AudioSource normalMusic; // Normal music
    public AudioSource bossMusic; // Boss music

    private bool bossActive = false; // Boss flag

    public TMP_Text waveCounterText;

    void Start()
    {
        waveCounterText.text = "";
    }

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;  // wave index

        waveCounterText.text = "" + waveIndex.ToString("D3");

        if (waveIndex % 5 == 0)
        {
            SpawnBoss();
        }
        else
        {
            int enemyCount = baseEnemyCount + waveIndex * 2;  //  higher wave, more enemies

            // Enemies distribution number per wave
            int normalCount = Mathf.RoundToInt(enemyCount * 0.6f);
            int tankCount = Mathf.RoundToInt(enemyCount * 0.2f);
            int fastCount = Mathf.RoundToInt(enemyCount * 0.2f);

            // spawn normal
            for (int i = 0; i < normalCount; i++)
            {
                SpawnEnemy(normalEnemyPrefab);
                yield return new WaitForSeconds(0.5f);
            }

            // Spawn Tank
            for (int i = 0; i < tankCount; i++)
            {
                SpawnEnemy(tankEnemyPrefab);
                yield return new WaitForSeconds(0.5f);
            }

            // Spawn Speed
            for (int i = 0; i < fastCount; i++)
            {
                SpawnEnemy(fastEnemyPrefab);
                yield return new WaitForSeconds(0.5f);
            }

            if (!bossActive && !normalMusic.isPlaying)
            {
                normalMusic.Play();
            }
        }
    }


    void SpawnEnemy(Transform enemyPrefab)
    {
        Transform enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.health *= Mathf.Pow(difficultyMultiplier, waveIndex - 1);
        }
    }

    void SpawnBoss()
    {
        Transform boss = Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        Enemy bossScript = boss.GetComponent<Enemy>();
        if (bossScript != null)
        {
            bossScript.isBoss = true;
            bossScript.health *= 5;
            bossScript.goldReward = 100;

            StartBossMusic();

            bossScript.OnDeath += EndBossMusic;
        }
    }

    void StartBossMusic()
    {
        normalMusic.Stop();
        bossMusic.Play();
        bossActive = true;
    }

    void EndBossMusic()
    {
        bossMusic.Stop();
        normalMusic.Play();
        bossActive = false;
    }
}
