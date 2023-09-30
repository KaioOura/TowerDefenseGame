using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, ITargetable
{
    [SerializeField]
    private EnemyData _enemyData;

    [SerializeField]
    private HealthSystem _healthSystem;

    // Start is called before the first frame update
    void Start()
    {
        _healthSystem.Initialize(_enemyData.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public HealthSystem GetHealthSystem()
    {
        return _healthSystem;
    }
}
