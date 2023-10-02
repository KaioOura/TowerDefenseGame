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

    [Header("Broadcaster")]
    [SerializeField]
    private BoolEventChannel OnEnterBuildEventChannel;

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

        OnEnterBuildEventChannel.RaiseEvent(true);

    }

    bool IsPurchaseValid(ObjectData objectData)
    {
        if (!Shop.IsPurchaseValid(objectData.Price))
            return false;

        //Checar limite de tal turret

        //Limite atingido

        return true;
    }

    public void OnBuy(ObjectData objectData)
    {
        OnEnterBuildEventChannel.RaiseEvent(false);
        OnRequestObjecPlacement -= BuildManager.PreparePlacement;
        BuildManager.OnObjectPlaced -= OnBuy;
        Shop.RemovePlayerCurrency(objectData.Price);
    }
}
