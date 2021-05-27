using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnPoint;
 
    //Sets the time between waves/rounds for enemies to spawn at 5 seconds.
    public float timeBetweenWaves = 5f;

    private float countdown = 2f;

    public Text waveCountdownText;
    
    // Wave/round indicator, the higher the rounds the more difficult the game gets. 
    private int waveIndex = 0;

    //Sets timer for enemy objects based on , so at the start of a round they are not inside of one another.
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }

    // Spawns enemy objects based on the waveIndex variable.
    IEnumerator SpawnWave()
    {
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            //Sets a timer, so at the start of a round the enemy objects are not inside of one another.
            yield return new WaitForSeconds(.25f);
        }
        
    }

    //Spawns the enemy object at the spawnpoint location.
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
