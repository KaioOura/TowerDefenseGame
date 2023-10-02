using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private Image _healthImage;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _healthImage.fillAmount = currentHealth / maxHealth;
    }
}
