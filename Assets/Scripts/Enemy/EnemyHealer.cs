using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : EnemyBase
{
    [SerializeField]
    private EnemyHealData enemyHealData;

    IEnumerator _healRoutine;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        StartHealing();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        if (_healRoutine != null)
            StopCoroutine(_healRoutine);
    }

    void StartHealing()
    {
        if (_healRoutine != null)
            StopCoroutine(_healRoutine);

        _healRoutine = HealCoroutine();
        StartCoroutine(_healRoutine);
    }

    IEnumerator HealCoroutine()
    {
        float time = 0;

        while(gameObject.activeSelf)
        {
            if (time >= enemyHealData.HealInterval)
            {
                AreaHeal();
                time = 0;
            }

            time += Time.deltaTime;

            yield return null;

        }
    }

    void AreaHeal()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, enemyHealData.HealRadius, enemyHealData.EnemyLayer);

        foreach (var item in enemies)
        {
            if (item.TryGetComponent(out ITargetable targetable))
                targetable.ApplyCharacterEffect(enemyHealData.HealCharacterEffectPack);
        }
    }
}
