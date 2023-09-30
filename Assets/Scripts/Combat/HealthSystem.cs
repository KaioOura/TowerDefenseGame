using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;

    public event Action OnDeath; // Inscrever método que chama o pooling para retornar esse objeto para a lista

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        OnDeath += ResetHealth;
    }

    private void OnDisable()
    {
        OnDeath -= ResetHealth;
    }

    public void Initialize(float maxHealth)
    {
        _maxHealth = maxHealth;

        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        //Tratar dano

        ApplyDamage(damage);

    }

    void ApplyDamage(float finalDamage)
    {
        _currentHealth -= finalDamage;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        if (_currentHealth > 0)
            return;

        OnDeath?.Invoke();
    }

    void Heal(float heal)
    {
        _currentHealth += heal;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }
}
