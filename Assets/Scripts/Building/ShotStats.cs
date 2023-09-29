using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShotStats
{
    public float Damage;
    public bool DoStagger;
}

[System.Serializable]
public class ExplosiveShotStats : ShotStats
{
    public float ExplosionRadius;
    public float ExplosionDamage;
}

