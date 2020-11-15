using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Pathfinding;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> waveConfigs;
    private WaveConfig currentWave;
    [SerializeField] private TextMeshProUGUI waveIndexText;
    // [SerializeField] private int startingWaveIndex;
    private int waveIndex = 0;
    private int waveCount = 0;
    // [SerializeField] private bool looping;
    private int waveKills = 0;
    private bool waveComplete = true;

    void Start()
    {
        waveCount = waveConfigs.Count;
    }

    void Update()
    {
        if (waveIndex == waveCount)
        {
            enabled = false;
        }
        else if (waveComplete)
        {
            currentWave = waveConfigs[waveIndex];
            waveKills = 0;
            Debug.Log("spawn wave 1");
            StartCoroutine(SpawnWave());
            waveComplete = false;
        }
        else if (waveKills == currentWave.GetNumberOfEnemies())
        {   
            Debug.Log("WAVE COMPLETED");
            waveCompleted();
        }
    }

    // Start is called before the first frame update
    /*
    private IEnumerator Start()
    {   
        do
        {
            // Once completed, come back and start it again
            yield return StartCoroutine(SpawnWave());
            ++waveIndex;
        } while (looping);
    }
    /*

    /*
    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingWaveIndex; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
            yield return new WaitForSeconds(10);
        }
    }
    */

    private IEnumerator SpawnWave()
    {
        Debug.Log("Spawn wave");
        waveIndexText.text = "Wave " + (waveIndex + 1);
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++)
        {
            GameObject newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetSpawnPoint(),
                Quaternion.identity);
            // Change variables of enemy here
            newEnemy.GetComponent<AIPath>().maxSpeed = currentWave.GetMoveSpeed();
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

    private void waveCompleted()
    {
        waveComplete = true;
        FindObjectOfType<AudioManager>().Play("levelWin clip");
        ++waveIndex;
    }

    public void waveKill()
    {
        ++waveKills;
    }

    public int GetZombiesLeft()
    {
        return currentWave.GetNumberOfEnemies() - waveKills;
    }
}
