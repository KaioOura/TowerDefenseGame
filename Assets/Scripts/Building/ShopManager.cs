using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]
    private ObjectData[] _objectDatas;

    [SerializeField]
    private ShopUIButton _shopUIButtonPrefab;

    [SerializeField]
    private Transform _buttonsContainer;

    public int gold = 10000;

    public event Action OnPlaceObject;
    public event Action<ObjectData> OnRequestObjecPlacement;

    // Start is called before the first frame update
    void Start()
    {
        PopPulate(_objectDatas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopPulate(ObjectData[] objects)
    {
        ShopUIButton shopUIButton;

        foreach (var obj in objects)
        {
            shopUIButton = Instantiate(_shopUIButtonPrefab, _buttonsContainer);
            shopUIButton.InitButton(obj.Sprite, obj.Price, () => TryToBuyObject(obj));
        }
    }

    void TryToBuyObject(ObjectData objectData)
    {
        if (!IsPurchaseValid(objectData))
            return;

        OnRequestObjecPlacement += BuildManager.PreparePlacement;
        OnRequestObjecPlacement?.Invoke(objectData);
        BuildManager.OnObjectPlaced += OnBuy;

    }

    bool IsPurchaseValid(ObjectData objectData)
    {
        if (objectData.Price > gold)
            return false;

        //Limite atingido

        return true;
    }

    public void OnBuy(ObjectData objectData)
    {
        OnRequestObjecPlacement -= BuildManager.PreparePlacement;
        BuildManager.OnObjectPlaced -= OnBuy;
        RemoveGold(objectData.Price);
    }

    void RemoveGold(int valueToRemove)
    {
        gold -= valueToRemove;
        gold = Mathf.Clamp(gold, 0, 99999);
    }

    void AddGold(int valueToRemove)
    {
        gold += valueToRemove;
        gold = Mathf.Clamp(gold, 0, 99999);
    }
}
