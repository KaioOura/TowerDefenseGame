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
        CharacterEffectPack = _currentShotStats.ShotCharacterEffectPack;
    }

    public override void Initialize(int level,  float speed, ShotTravelEnum shotTravelEnum, ITargetable target)
    {
        ShotTravelType = shotTravelEnum;
        Speed = speed;

        UpdateShotStats(level);

        LaunchShot(target);
    }
}
