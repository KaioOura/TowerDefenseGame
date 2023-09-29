using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShot : TurretShotBase
{
    [SerializeField] private ShotData ShotData;
    private ShotStats _currentShotStats;

    public override void UpdateShotStats(int i)
    {
        _currentShotStats = ShotData.GetShotStat(i);
        Damage = _currentShotStats.Damage;
    }

    public override void LaunchShot()
    {

    }

    public override void Initialize(int level, ShotTravelEnum shotTravelEnum)
    {
        Level = level;
        ShotTravelType = shotTravelEnum;

        UpdateShotStats(Level);

        LaunchShot();
    }
}
