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

    public HealthSystem HealthSystem => _healthSystem;
    public EnemyData EnemyData => _enemyData;

    protected IEnumerator WaitStaggerRoutine;
    protected IEnumerator WaitSlowRoutine;

    public virtual void Awake()
    {
        TargetSpot = GameObject.Find("Target").transform;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        _navMeshAgent.SetDestination(TargetSpot.position);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, TargetSpot.position) <= 1f)
        {
            Player.TakeDamage(_enemyData.Damage);
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {

    }

    public virtual void OnEnable()
    {
        _navMeshAgent.SetDestination(TargetSpot.position);
        _characterEffect.OnReceiveDamage += OnTakeDamage;
        _characterEffect.OnReceiveStagger += OnStagger;
        _characterEffect.OnReceiveSlow += OnSlow;
        _characterEffect.OnReceiveHeal += OnHeal;
        _healthSystem.OnDeath += OnDie;

        _healthSystem.Initialize(_enemyData.MaxHealth);
        _navMeshAgent.speed = _enemyData.Speed;
    }

    public virtual void OnDisable()
    {
        _characterEffect.OnReceiveDamage -= OnTakeDamage;
        _characterEffect.OnReceiveStagger -= OnStagger;
        _characterEffect.OnReceiveSlow -= OnSlow;
        _characterEffect.OnReceiveHeal -= OnHeal;
        _healthSystem.OnDeath -= OnDie;
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

    void OnHeal(float healAmount)
    {
        _healthSystem.Heal(healAmount);
    }

    void OnSlow(float amount, float duration)
    {
        if (WaitSlowRoutine != null)
            StopCoroutine(WaitSlowRoutine);

        WaitSlowRoutine = WaitSlow(amount, duration);
        StartCoroutine(WaitSlowRoutine);
    }

    IEnumerator WaitSlow(float amount, float duration)
    {
        amount = Mathf.Clamp(amount, 1, 5);

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
        Player.AddCurrency(_enemyData.DropGold);
        Player.AddPoint(_enemyData.DropPoint);
        gameObject.SetActive(false);
    }
}
