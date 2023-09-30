using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour, ITargetable
{
    [SerializeField]
    private EnemyData _enemyData;

    [SerializeField]
    private HealthSystem _healthSystem;

    [SerializeField]
    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private Transform TargetSpot;

    private void Awake()
    {
        TargetSpot = GameObject.Find("Target").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthSystem.Initialize(_enemyData.MaxHealth);



        _navMeshAgent.SetDestination(TargetSpot.position);
    }

    private void OnEnable()
    {
        _navMeshAgent.SetDestination(TargetSpot.position);
        _healthSystem.OnDeath += OnDie;
    }

    private void OnDisable()
    {
        _healthSystem.OnDeath -= OnDie;
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
    
    void OnDie()
    {
        gameObject.SetActive(false);
    }
}
