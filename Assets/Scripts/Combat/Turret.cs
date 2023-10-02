using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System;

public class Turret : ObjectPlaceable
{
    private int level = 1;

    [SerializeField]
    private DrawRadiusAround _visualRange;
    [SerializeField]
    private Transform _visual;
    private TurretData _turretData;
    private TurretStats _currentTurretStats;
    private ITargetable _enemyTarget;
    private float _lastTimeShot;

    public TurretData TurretData => _turretData;
    public int Level => level;
    public bool IsMaxLevel => level >= _turretData.TurretLevel.Length;

    public event Action OnUpgradeSuccess;

    public override void Awake()
    {
        base.Awake();
        _turretData = ObjectData as TurretData;
    }

    private void Start()
    {
        UpdateTurretStats(level);

    }

    public override void Update()
    {
        if (!isOn)
            return;

        base.Update();

        LookToEnemy();
        GetEnemiesInRange();
        CalcShooting();
    }

    void LookToEnemy()
    {
        if (_enemyTarget == null)
            return;

        Vector3 lookPos = new Vector3(_enemyTarget.GetTransform().position.x, transform.position.y, _enemyTarget.GetTransform().position.z);
        _visual.LookAt(lookPos);
    }

    public virtual void UpdateTurretStats(int i)
    {
        _currentTurretStats = _turretData.GetStat(i);
        _visualRange.CreatePoints(_currentTurretStats.Range);
    }

    public virtual void CalcShooting()
    {
        if (!CanShoot())
            return;

        Shoot();
    }



    void Shoot()
    {
        Profiler.BeginSample("SpawnShot");
        IShotInitializer turretShot = Pooling.GetTurretShotBase(_turretData.TurretShot, transform.position);// Instantiate(_turretData.TurretShot);
        turretShot.Initialize(level, _turretData.BulletSpeed, _turretData.ShotTravelType, _enemyTarget);
        Profiler.EndSample();

        _lastTimeShot = Time.time;

    }

    bool CanShoot()
    {
        return _enemyTarget != null && Time.time >= _lastTimeShot + _currentTurretStats.FireRate;
    }

    public void GetEnemiesInRange()
    {
        if (_enemyTarget != null)
        {
            if (Vector3.Distance(transform.position, _enemyTarget.GetTransform().position) > _currentTurretStats.Range || !_enemyTarget.GetTransform().gameObject.activeSelf)
                _enemyTarget = null;

            return;
        }

        Collider[] targets =  Physics.OverlapSphere(transform.position, _currentTurretStats.Range, _turretData.EnemyLayer);

        if (targets.Length == 0)
            return;

        GetClosestTarget(targets);

    }

    public void GetClosestTarget(Collider[] Targets)
    {
        float minDist = Mathf.Infinity;
        float dist;
        Collider bestTarget = null;

        foreach (var target in Targets)
        {
            dist = Vector3.Distance(transform.position, target.transform.position);

            if (dist < minDist)
            {
                minDist = dist;
                bestTarget = target;
            }
        }

        if (bestTarget.TryGetComponent(out ITargetable targetable))
            _enemyTarget = targetable;

    }

    public void TryUpgrade()
    {
        if (level >= _turretData.TurretLevel.Length)
            return;

        if (!Shop.IsPurchaseValid(_turretData.GetStat(level + 1).Price))
            return;

        Shop.RemovePlayerCurrency(_turretData.GetStat(level + 1).Price);

        Upgrade();
    }

    void Upgrade()
    {
        level++;
        UpdateTurretStats(level);

        OnUpgradeSuccess?.Invoke();
    }

    //private void OnDrawGizmos()
    //{
    //    if (_currentTurretStats != null)
    //        Gizmos.DrawSphere(transform.position, _currentTurretStats.Range);
    //}

}
