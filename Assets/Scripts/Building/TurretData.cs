using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObjects/TurretDataScriptableObject", order = 1)]
public class TurretData : ObjectData
{
    [SerializeField] private TurretShotBase _turretShot;
    [SerializeField] private TurretStats[] _turretLevel;

    [SerializeField] private ShotTravelEnum _shotTravelType;


    public TurretShotBase TurretShot => _turretShot;
    public TurretStats[] TurretLevel => _turretLevel;
    public ShotTravelEnum ShotTravelType => _shotTravelType;

    public TurretStats GetStat(int level)
    {
        level -= 1;

        level = Mathf.Clamp(level, 0, TurretLevel.Length - 1);

        return TurretLevel[level];
    }

}

[System.Serializable]
public class TurretStats
{
    public float Range;
    public float FireRate;
}

public enum ShotTravelEnum
{
    FollowTarget,
    FollowFixedPosition
}
