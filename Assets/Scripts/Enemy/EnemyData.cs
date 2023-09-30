using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyDataScriptableObject", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField]
    private Team _team;

    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private float _speed;

    public Team Team => _team;
    public float MaxHealth => _maxHealth;
    public float Speed => _speed;
}
