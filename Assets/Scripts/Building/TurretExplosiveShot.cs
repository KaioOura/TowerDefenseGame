using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretExplosiveShot : TurretShotBase
{
    [SerializeField] private ExplosiveShotData _explosiveShotData;
    private ExplosiveShotStats _currentShotStats;

    private CharacterEffectPack _explosiveCharacterEffectPack;


    private float _radius;
    private float _explosiveDamage;

    private void OnEnable()
    {
        OnTargetReached += Explode;
    }

    private void OnDisable()
    {
        OnTargetReached -= Explode;
    }

    public override void UpdateShotStats(int i)
    {
        _currentShotStats = _explosiveShotData.GetExplosiveShotStat(i);

        CharacterEffectPack = _currentShotStats.CharacterEffectPack;
        _radius = _currentShotStats.ExplosionRadius;
    }

    public override void Initialize(int level, ShotTravelEnum shotTravelEnum, ITargetable target)
    {
        ShotTravelType = shotTravelEnum;
        UpdateShotStats(level);

        LaunchShot(target);
    }

    void Explode()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, _radius, _explosiveShotData.EnemyLayer);

        foreach (var item in enemies)
        {
            if (item.TryGetComponent(out ITargetable targetable))
                targetable.ApplyCharacterEffect(_currentShotStats.ExplosiveCharacterEffectPack);
        }
    }

}
