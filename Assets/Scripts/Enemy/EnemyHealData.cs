using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyHealData", menuName = "ScriptableObjects/EnemyHealDataScriptableObject", order = 1)]
public class EnemyHealData : ScriptableObject
{
    [SerializeField]
    private CharacterEffectPack _healCharacterEffectPack;

    [SerializeField]
    private float _healRadius;

    [SerializeField]
    private float _healInterval;

    [SerializeField]
    private LayerMask _enemyLayer;

    public CharacterEffectPack HealCharacterEffectPack => _healCharacterEffectPack;
    public float HealRadius => _healRadius;
    public float HealInterval => _healInterval;
    public LayerMask EnemyLayer => _enemyLayer;

}
