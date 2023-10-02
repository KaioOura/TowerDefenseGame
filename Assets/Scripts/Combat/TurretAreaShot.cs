using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAreaShot : TurretShotBase
{
    [SerializeField] private AreaShotData _explosiveShotData;
    private AreaShotStats _currentShotStats;

    private float _radius;

    private void OnEnable()
    {
        OnTargetReached += AreaHit;
    }

    private void OnDisable()
    {
        OnTargetReached -= AreaHit;
    }

    public override void UpdateShotStats(int i)
    {
        _currentShotStats = _explosiveShotData.GetAreaShotStat(i);

        CharacterEffectPack = _currentShotStats.ShotCharacterEffectPack;
        _radius = _currentShotStats.AreaRadius;
    }

    public override void Initialize(int level,  float speed, ShotTravelEnum shotTravelEnum, ITargetable target)
    {
        ShotTravelType = shotTravelEnum;
        Speed = speed;
        UpdateShotStats(level);

        LaunchShot(target);
    }

    void AreaHit()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, _radius, _explosiveShotData.EnemyLayer);

        foreach (var item in enemies)
        {
            if (item.TryGetComponent(out ITargetable targetable))
                targetable.ApplyCharacterEffect(_currentShotStats.AreaCharacterEffectPack);
        }
    }

}
