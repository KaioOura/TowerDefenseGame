using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public static string PlayerName;
    public static int Currency = 100;
    public static int Score;
    public static int HighScore;

    [SerializeField]
    private HealthSystem _healthSystem;

    [Header("Broadcaster")]
    [SerializeField] 
    private FloatEventChannel OnCurrencyEventChannel;

    [SerializeField]
    private FloatEventChannel OnScoreEventChannel;

    [SerializeField]
    private FloatEventChannel OnHighScoreEventChannel;

    [SerializeField]
    private VoidEventChannel OnPlayerEventChannel;

    [SerializeField]
    private FloatEventChannel OnPlayerHitEventChannel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        HighScore = PlayerPrefs.GetInt(SaveVariables.PlayerHighScore, 0);

        _healthSystem.Initialize(100);

        OnPlayerHitEventChannel.RaiseEvent(_healthSystem.CurrentHealth);
        OnCurrencyEventChannel.RaiseEvent(Currency);
        OnScoreEventChannel.RaiseEvent(Score);
        OnHighScoreEventChannel.RaiseEvent(HighScore);


        _healthSystem.OnDeath += OnDie;
    }

    private void OnDestroy()
    {
        _healthSystem.OnDeath -= OnDie;
    }

    public static void TakeDamage(int value)
    {
        Instance._healthSystem.TakeDamage(value);
        Instance.OnPlayerHitEventChannel.RaiseEvent(Instance._healthSystem.CurrentHealth);
    }

    void OnDie()
    {
        OnPlayerEventChannel.RaiseEvent();
    }

    public static void AddCurrency(int amountToCalc)
    {
        Currency += amountToCalc;
        Mathf.Clamp(Currency, 0, 99999);
        Instance.OnCurrencyEventChannel.RaiseEvent(Currency);
    }

    public static void RemoveCurrency(int amountToCalc)
    {
        Currency -= amountToCalc;
        Mathf.Clamp(Currency, 0, 99999);
        Instance.OnCurrencyEventChannel.RaiseEvent(Currency);
    }

    public static void SetCurrency(int amountToSet)
    {
        Currency = amountToSet;
        Instance.OnCurrencyEventChannel.RaiseEvent(Currency);
    }

    public static void AddPoint(int amountToCalc)
    {
        Score += amountToCalc;
        Mathf.Clamp(Score, 0, 99999);
        Instance.OnScoreEventChannel.RaiseEvent(Score);

        if (Score > HighScore)
        {
            Instance.OnHighScoreEventChannel.RaiseEvent(Score);
            HighScore = Score;

            PlayerPrefs.SetInt(SaveVariables.PlayerHighScore, HighScore);
        }
    }

    public static void RemovePoint(int amountToCalc)
    {
        Score -= amountToCalc;
        Mathf.Clamp(Score, 0, 99999);
        Instance.OnScoreEventChannel.RaiseEvent(Score);
    }

    public static void SetPoint(int amountToSet)
    {
        Score = amountToSet;
        Instance.OnScoreEventChannel.RaiseEvent(Score);
    }
}
