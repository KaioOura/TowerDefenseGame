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

    [SerializeField]
    private CharacterEffect _characterEffect;

    protected IEnumerator WaitStaggerRoutine;
    protected IEnumerator WaitSlowRoutine;

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

    private void OnDestroy()
    {

    }

    private void OnEnable()
    {
        _navMeshAgent.SetDestination(TargetSpot.position);
        _characterEffect.OnReceiveDamage += OnTakeDamage;
        _characterEffect.OnReceiveStagger += OnStagger;
        _characterEffect.OnReceiveSlow += OnSlow;
        _healthSystem.OnDeath += OnDie;
    }

    private void OnDisable()
    {
        _healthSystem.OnDeath -= OnDie;
        _characterEffect.OnReceiveDamage -= OnTakeDamage;
        _characterEffect.OnReceiveStagger -= OnStagger;
        _characterEffect.OnReceiveSlow -= OnSlow;
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

    public void ApplyCharacterEffect(CharacterEffectPack characterEffectPack)
    {
        _characterEffect.OnReceiveEffect(characterEffectPack);
    }

    void OnTakeDamage(float damage)
    {
        _healthSystem.TakeDamage(damage);
    }

    void OnSlow(float amount, float duration)
    {
        if (WaitSlowRoutine != null)
            return;

        WaitSlowRoutine = WaitSlow(amount, duration);
        StartCoroutine(WaitSlowRoutine);
    }

    IEnumerator WaitSlow(float amount, float duration)
    {
        _navMeshAgent.speed = _navMeshAgent.speed / amount;

        yield return new WaitForSeconds(duration);

        _navMeshAgent.speed = _enemyData.Speed;

        _navMeshAgent.isStopped = false;
        WaitStaggerRoutine = null;
    }

    void OnStagger(float duration)
    {
        if (WaitStaggerRoutine != null)
            return;

        WaitStaggerRoutine = WaitStagger(duration);
        StartCoroutine(WaitStaggerRoutine);
    }

    IEnumerator WaitStagger(float time)
    {
        _navMeshAgent.isStopped = true;

        yield return new WaitForSeconds(time);

        _navMeshAgent.isStopped = false;
        WaitStaggerRoutine = null;
    }

    void OnDie()
    {
        gameObject.SetActive(false);
    }
}
