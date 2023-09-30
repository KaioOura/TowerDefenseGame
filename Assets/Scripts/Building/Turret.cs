using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ObjectPlaceable
{
    private int level = 1;

    private TurretData _turretData;
    private TurretStats _currentTurretStats;
    private ITargetable _enemyTarget;

    private float _lastTimeShot;

    public override void Awake()
    {
        base.Awake();
        _turretData = ObjectData as TurretData;
    }

    private void Start()
    {
        UpdateTurretStats(level);

    }

    private void Update()
    {
        GetEnemiesInRange();
        CalcShooting();
    }

    public virtual void UpdateTurretStats(int i)
    {
        _currentTurretStats = _turretData.GetStat(i);
    }

    public virtual void CalcShooting()
    {
        if (!CanShoot())
            return;

        Shoot();
    }

    void Shoot()
    {
        IShotInitializer turretShot = Instantiate(_turretData.TurretShot);
        turretShot.Initialize(level, _turretData.ShotTravelType, _enemyTarget);

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
            if (Vector3.Distance(transform.position, _enemyTarget.GetTransform().position) > _currentTurretStats.Range)
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, _currentTurretStats.Range);
    }

}
