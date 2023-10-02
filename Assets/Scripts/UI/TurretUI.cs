using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretUI : MonoBehaviour
{
    [SerializeField]
    private Turret _turret;

    [SerializeField]
    private Transform _buttonContainer;

    [SerializeField]
    private ShopUIButton _shopUIButton;

    private ShopUIButton _currentShopUIButton;


    private void Awake()
    {
        _currentShopUIButton = Instantiate(_shopUIButton, _buttonContainer);
        _turret.OnUpgradeSuccess += OnUpgrade;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnUpgrade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _turret.OnUpgradeSuccess -= OnUpgrade;
    }

    void OnUpgrade()
    {
        UpdateUpgradeButton(() => _turret.TryUpgrade());
    }

    void UpdateUpgradeButton(Action onUpgrade)
    {
        _currentShopUIButton.InitButton(null, _turret.TurretData.GetStat(_turret.Level + 1).Price, onUpgrade);
    }
}
