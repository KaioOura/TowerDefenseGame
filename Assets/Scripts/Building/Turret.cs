using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ObjectPlaceable
{
    private int level = 1;
    protected float _range;
    protected float _fireRate;
    protected float _damage;
    private TurretData _turretData;
    private TurretStats _currentTurretStats;


    public override void Awake()
    {
        base.Awake();
        _turretData = ObjectData as TurretData;
    }

    private void Start()
    {
        UpdateTurretStats(level);

        InvokeRepeating("Shoot", 0, 1);
    }

    public virtual void UpdateTurretStats(int i)
    {
        _currentTurretStats = _turretData.GetStat(i);
    }

    void Shoot()
    {
        IShotInitializer turretShot = Instantiate(_turretData.TurretShot);
        turretShot.Initialize(level, _turretData.ShotTravelType);
    }

}
