using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;

    [SerializeField]
    private TurretShot _turretShot;
    private List<TurretShotBase> _turretShotList = new List<TurretShotBase>();

    [SerializeField]
    private TurretExplosiveShot _turretExplosiveShot;
    private List<TurretShotBase> _turretExplosiveShotList = new List<TurretShotBase>();

    [SerializeField]
    private EnemyBase _enemyBase;
    [SerializeField]
    private List<EnemyBase> _enemyBaseList;

    private void Awake()
    {
        Instance = this;

        SpawnPrefabs();
    }



    void SpawnPrefabs()
    {
        SpawnTurretShot(100);
        SpawnExplosiveShot(100);
        SpawnEnemy(_enemyBase, 100);
    }


    public static EnemyBase GetEnemyBase(EnemyBase enemyBase, Vector3 startPos)
    {
        EnemyBase enemy = Instance.GetEnemyFromList(enemyBase);

        if (enemy == null)
            enemy = Instance.SpawnEnemy(enemyBase, 1);

        enemy.transform.position = startPos;
        enemy.gameObject.SetActive(true);

        return enemy;
    }

    public static TurretShotBase GetTurretShotBase(TurretShotBase turretShotBase, Vector3 startPos)
    {
        TurretShotBase turretShot = Instance.GetTurretShotFromList(turretShotBase);

        if (turretShot == null)
            turretShot = Instance.SpawnTurretShot(turretShotBase);

        turretShot.transform.position = startPos;

        turretShot.gameObject.SetActive(true);

        return turretShot;
    }

    EnemyBase GetEnemyFromList(EnemyBase enemyBase)
    {
        for (int i = 0; i < _enemyBaseList.Count; i++)
        {
            if (_enemyBaseList[i].isActiveAndEnabled)
                continue;

            return _enemyBaseList[i];
        }

        return null;
    }

    TurretShotBase GetTurretShotFromList(TurretShotBase turretShotBase)
    {
        if (turretShotBase == _turretShot)
        {
            for (int i = 0; i < _turretShotList.Count; i++)
            {
                if (_turretShotList[i].isActiveAndEnabled)
                    continue;

                return _turretShotList[i];
            }
        }
        else if (turretShotBase == Instance._turretExplosiveShot)
        {
            for (int i = 0; i < _turretExplosiveShotList.Count; i++)
            {
                if (_turretExplosiveShotList[i].isActiveAndEnabled)
                    continue;

                return _turretExplosiveShotList[i];
            }
        }


        return null;
    }

    EnemyBase SpawnEnemy(EnemyBase enemyBase, int num)
    {
        EnemyBase enemy = null;

        for (int i = 0; i < num; i++)
        {
            enemy = Instantiate(_enemyBase);
            enemy.gameObject.SetActive(false);

            _enemyBaseList.Add(enemy);
        }

        return enemy;
    }

    void SpawnTurretShot(int num)
    {
        TurretShot turretShot;

        for (int i = 0; i < num; i++)
        {
            turretShot = Instantiate(_turretShot);
            turretShot.gameObject.SetActive(false);
            _turretShotList.Add(turretShot);
        }
    }

    TurretShotBase SpawnTurretShot(TurretShotBase turretShotBase)
    {
        TurretShotBase turretShot;

        turretShot = Instantiate(_turretShot);
        turretShot.gameObject.SetActive(false);

        if (turretShotBase == Instance._turretShot)
            _turretShotList.Add(turretShot);
        else if (turretShotBase == Instance._turretExplosiveShot)
            _turretExplosiveShotList.Add(turretShot);

        return turretShot;
    }

    void SpawnExplosiveShot(int num)
    {
        TurretExplosiveShot turretShot;

        for (int i = 0; i < num; i++)
        {
            turretShot = Instantiate(_turretExplosiveShot);
            turretShot.gameObject.SetActive(false);
            _turretExplosiveShotList.Add(turretShot);
        }
    }
}
