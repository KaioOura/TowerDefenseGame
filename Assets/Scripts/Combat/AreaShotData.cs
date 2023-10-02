using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AreaShotData", menuName = "ScriptableObjects/AreaShotDataScriptableObject", order = 1)]
public class AreaShotData : ScriptableObject
{
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private AreaShotStats[] _areaShotStats;


    public AreaShotStats[] AreaShotStats => _areaShotStats;
    public LayerMask EnemyLayer => _enemyLayer;

    public AreaShotStats GetAreaShotStat(int level)
    {
        level -= 1;

        level = Mathf.Clamp(level, 0, AreaShotStats.Length - 1);

        return AreaShotStats[level];
    }
}
