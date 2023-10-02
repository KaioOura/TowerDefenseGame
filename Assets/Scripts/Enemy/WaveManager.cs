using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPos;

    [SerializeField]
    private Wave[] _waves;

    [SerializeField]
    private InfinityWave _infinityWave;

    private int _waveLevel;
    private int _spawns;


    // Start is called before the first frame update
    void Start()
    {
        GetWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetWave()
    {
        if (_waveLevel >= _waves.Length)
        {
            _infinityWave.UpdateEnemyList(GetInfinityWaveEnemies());

            StartCoroutine(WaitForWave(_infinityWave));

            return;
        }

        StartCoroutine(WaitForWave(_waves[_waveLevel]));
    }

    IEnumerator WaitForWave(Wave wave)
    {
        yield return new WaitForSeconds(wave.TimeToStart);

        yield return StartCoroutine(SpawnEnemy(wave));

        yield return new WaitForSeconds(wave.TimeToEnd);

        _waveLevel++;

        GetWave();
    }

    IEnumerator SpawnEnemy(Wave wave)
    {
        float time = 0;

        while (_spawns < wave.Enemies.Count)
        {
            time += Time.deltaTime;

            if (time >= wave.TimeToSpawn)
            {
                Pooling.GetEnemyBase(wave.Enemies[_spawns], _spawnPos.position);

                time = 0;
                _spawns++;
            }

            yield return null;
        }

        _spawns = 0;

    }

    List<EnemyBase> GetInfinityWaveEnemies()
    {
        List<EnemyBase> enemyBases = new List<EnemyBase>();

        for (int i = 0; i < _infinityWave.MaxEnemy; i++)
        {
            int rand = Random.Range(0, _infinityWave.PossibleEnemies.Count);

            enemyBases.Add(_infinityWave.PossibleEnemies[rand]);
        }

        return enemyBases;
    }
}

[System.Serializable]
public class Wave
{
    public List<EnemyBase> Enemies;
    public float TimeToStart;
    public float TimeToEnd;
    public float TimeToSpawn;
}

[System.Serializable]
public class InfinityWave : Wave
{
    public List<EnemyBase> PossibleEnemies;
    public int MaxEnemy;

    public void UpdateEnemyList(List<EnemyBase> enemyBases)
    {
        Enemies = enemyBases;
    }
}
