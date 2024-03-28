using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float timeBetweenWaves = 7f;
    private float countdown = 1f;

    public Transform spawnPoint;
    public Text waveCountdownText;
    public Text waveNumberText;

    private int waveNumber = 1;
    private Transform playerTransform;

    public int enemiesPerWave = 5;
    public int additionalEnemiesPerWave = 2;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        UpdateWaveNumberText();
    }

    private void Update()
    {
        spawnedEnemies.RemoveAll(item => item == null); 

        if (countdown <= 0f && spawnedEnemies.Count == 0)
        {
            StartCoroutine(StartWaveCountdown(2));
            countdown = timeBetweenWaves + 2;
        }
        else
        {
            countdown -= Time.deltaTime;
            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
            waveCountdownText.text = countdown > 3 ? $"Next Wave In: {countdown - 3:00}" : "";
        }
    }

    IEnumerator StartWaveCountdown(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            waveCountdownText.text = $"Wave Starts In: {count}";
            yield return new WaitForSeconds(0.5f);
            count--;
        }
        waveCountdownText.text = "";
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int enemiesToSpawn = enemiesPerWave + (waveNumber - 1) * additionalEnemiesPerWave;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
        UpdateWaveNumberText();
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            GameObject spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            spawnedEnemies.Add(spawnedEnemy);
            spawnedEnemy.GetComponent<Enemy>().target = playerTransform;
        }
    }

    private void UpdateWaveNumberText()
    {
        waveNumberText.text = $"Wave: {waveNumber}";
    }
}
