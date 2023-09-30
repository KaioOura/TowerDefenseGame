using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPos;

    [SerializeField]
    private Wave[] _waves;

    private int _waveLevel;
    private int _spawns;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForWave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForWave()
    {
        if (_waveLevel >= _waves.Length)
        {
            //Fim de jogo
            yield break;
        }

        yield return new WaitForSeconds(_waves[_waveLevel].TimeToStart);

        yield return StartCoroutine(SpawnEnemy());

    }

    IEnumerator SpawnEnemy()
    {
        float time = 0;

        while (_spawns < _waves[_waveLevel].Enemies.Length - 1)
        {
            time += Time.deltaTime;

            if (time >= _waves[_waveLevel].TimeToSpawn)
            {
                Pooling.GetEnemyBase(_waves[_waveLevel].Enemies[_spawns], _spawnPos.position);

                time = 0;
                _spawns++;
            }

            yield return null;
        }

    }
}

[System.Serializable]
public class Wave
{
    public EnemyBase[] Enemies;
    public float TimeToStart;
    public float TimeToEnd;
    public float TimeToSpawn;
}
