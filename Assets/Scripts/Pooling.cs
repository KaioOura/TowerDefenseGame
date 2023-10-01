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
    private TurretAreaShot _turretExplosiveShot;
    private List<TurretShotBase> _turretExplosiveShotList = new List<TurretShotBase>();

    [SerializeField]
    private TurretAreaShot _turretSlowShot;
    private List<TurretShotBase> _turretSlowShotList = new List<TurretShotBase>();

    [SerializeField]
    private EnemyBase _enemyBase;
    [SerializeField]
    private List<EnemyBase> _enemyBaseList;

    [SerializeField]
    private EnemyBase _enemyHealer;
    [SerializeField]
    private List<EnemyBase> _enemyHealerList;

    private void Awake()
    {
        Instance = this;

        SpawnPrefabs();
    }



    void SpawnPrefabs()
    {
        SpawnTurretShot(_turretShot, 100);
        SpawnTurretShot(_turretExplosiveShot, 100);
        SpawnTurretShot(_turretSlowShot, 100);

        SpawnEnemy(_enemyBase, 50);
        SpawnEnemy(_enemyHealer, 50);
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
            turretShot = Instance.SpawnTurretShot(turretShotBase, 1);

        turretShot.transform.position = startPos;

        turretShot.gameObject.SetActive(true);

        return turretShot;
    }

    EnemyBase GetEnemyFromList(EnemyBase enemyBase)
    {
        List<EnemyBase> List = new List<EnemyBase>();

        if (enemyBase == _enemyBase)
            List = _enemyBaseList;
        else if (enemyBase == _enemyHealer)
            List = _enemyHealerList;

        for (int i = 0; i < List.Count; i++)
        {
            if (List[i].isActiveAndEnabled)
                continue;

            return List[i];
        }

        return null;
    }

    TurretShotBase GetTurretShotFromList(TurretShotBase turretShotBase)
    {
        List<TurretShotBase> List = new List<TurretShotBase>();

        if (turretShotBase == _turretShot)
            List = _turretShotList;
        else if (turretShotBase == _turretExplosiveShot)
            List = _turretExplosiveShotList;
        else
            List = _turretSlowShotList;

        for (int i = 0; i < List.Count; i++)
        {
            if (List[i].isActiveAndEnabled)
                continue;

            return List[i];
        }

        return null;
    }

    EnemyBase SpawnEnemy(EnemyBase enemyBase, int num)
    {
        EnemyBase enemy = null;
        List<EnemyBase> listToAdd = new List<EnemyBase>();


        if (enemyBase == _enemyBase)
            listToAdd = _enemyBaseList;
        else if (enemyBase == _enemyHealer)
            listToAdd = _enemyHealerList;

        for (int i = 0; i < num; i++)
        {
            enemy = Instantiate(enemyBase);
            listToAdd.Add(enemy);
            enemy.gameObject.SetActive(false);
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

    TurretShotBase SpawnTurretShot(TurretShotBase turretShotBase, int num)
    {
        TurretShotBase turretShot = null;
        List<TurretShotBase> listToAdd = new List<TurretShotBase>();

        if (turretShotBase == _turretShot)
            listToAdd = _turretShotList;
        else if (turretShotBase == _turretExplosiveShot)
            listToAdd = _turretExplosiveShotList;
        else
            listToAdd = _turretSlowShotList;

        for (int i = 0; i < num; i++)
        {
            turretShot = Instantiate(turretShotBase);
             listToAdd.Add(turretShot);
            turretShot.gameObject.SetActive(false);
        }

        return turretShot;
    }

    void SpawnExplosiveShot(int num)
    {
        TurretAreaShot turretShot;

        for (int i = 0; i < num; i++)
        {
            turretShot = Instantiate(_turretExplosiveShot);
            turretShot.gameObject.SetActive(false);
            _turretExplosiveShotList.Add(turretShot);
        }
    }
}
