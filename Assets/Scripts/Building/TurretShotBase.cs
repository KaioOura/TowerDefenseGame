using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShotBase : MonoBehaviour, IShotInitializer
{
    public int Level { get; set; }
    public float Damage { get; set; }
    public ShotTravelEnum ShotTravelType { get; set; }

    public virtual void Initialize(int level, ShotTravelEnum shotTravelEnum)
    {
     
    }

    public virtual void LaunchShot()
    {

    }

    public virtual void UpdateShotStats(int level)
    {

    }
}
