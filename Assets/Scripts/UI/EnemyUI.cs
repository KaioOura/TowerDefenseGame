using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    [SerializeField]
    private EnemyBase _enemyBase;

    [SerializeField]
    private HealthBarUI _healthBarUI;

    private void Awake()
    {
        _enemyBase.HealthSystem.OnHealthChange += _healthBarUI.UpdateHealthBar;
    }

    private void OnEnable()
    {

    }

    private void OnDestroy()
    {
        _enemyBase.HealthSystem.OnHealthChange -= _healthBarUI.UpdateHealthBar;
    }

}
