using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShotInitializer
{
    public int Level { get; set; }
    public float Damage { get; set; }
    public ShotTravelEnum ShotTravelType { get; set; }

    public void Initialize(int level, ShotTravelEnum shotTravelEnum);
    void LaunchShot();
    public void UpdateShotStats(int level);
}
