using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretExplosiveShot : TurretShotBase
{
    [SerializeField] private ExplosiveShotData _explosiveShotData;
    private ExplosiveShotStats _currentShotStats;


    private float _radius;
    private float _explosiveDamage;

    public override void UpdateShotStats(int i)
    {
        _currentShotStats = _explosiveShotData.GetExplosiveShotStat(i);

        Damage = _currentShotStats.Damage;

        _radius = _currentShotStats.ExplosionRadius;
        _explosiveDamage = _currentShotStats.ExplosionDamage;
    }

    public override void Initialize(int level, ShotTravelEnum shotTravelEnum)
    {
        Level = level;
        ShotTravelType = shotTravelEnum;
        UpdateShotStats(level);

        LaunchShot();
    }


    public override void LaunchShot()
    {
        
    }
}
