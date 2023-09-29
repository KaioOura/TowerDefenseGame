using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveData", menuName = "ScriptableObjects/ExplosiveDataScriptableObject", order = 1)]
public class ExplosiveShotData : ScriptableObject
{
    [SerializeField] private ExplosiveShotStats[] _explosiveShotStats;


    public ExplosiveShotStats[] ExplosiveShotStats => _explosiveShotStats;

    public ExplosiveShotStats GetExplosiveShotStat(int level)
    {
        level -= 1;

        level = Mathf.Clamp(level, 0, ExplosiveShotStats.Length - 1);

        return ExplosiveShotStats[level];
    }
}
