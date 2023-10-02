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

    [SerializeField]
    private int _damage;

    [SerializeField]
    private int _dropGold;

    [SerializeField]
    private int _dropPoint;

    public Team Team => _team;
    public float MaxHealth => _maxHealth;
    public float Speed => _speed;
    public int Damage => _damage;
    public int DropGold => _dropGold;
    public int DropPoint => _dropPoint;
}
