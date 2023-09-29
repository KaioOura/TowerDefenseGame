using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShotTypeData", menuName = "ScriptableObjects/ShopTypeDataScriptableObject", order = 1)]
public class ShotData : ScriptableObject
{
    [SerializeField] private ShotStats[] _shotStats;
    public ShotStats[] ShotStats => _shotStats;

    public ShotStats GetShotStat(int level)
    {
        level -= 1;

        level = Mathf.Clamp(level, 0, ShotStats.Length - 1);

        return ShotStats[level];
    }
}
