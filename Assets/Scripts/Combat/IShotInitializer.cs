using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShotInitializer
{
    public float Speed { get; set; }
    public CharacterEffectPack CharacterEffectPack { get; set; }
    public ShotTravelEnum ShotTravelType { get; set; }

    public void Initialize(int level, float speed, ShotTravelEnum shotTravelEnum, ITargetable target);
    void LaunchShot(ITargetable target);
    public void UpdateShotStats(int level);
}
