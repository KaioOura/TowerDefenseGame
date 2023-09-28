using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopUIButton : MonoBehaviour
{
    [SerializeField]
    private Image _itemImage;

    [SerializeField]
    private TextMeshProUGUI _itemPriceTxt;

    [SerializeField]
    private Button button; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitButton(Sprite itemSprite, int itemPrice, Action callback)
    {
        button.onClick.RemoveAllListeners();

        _itemImage.sprite = itemSprite;
        _itemPriceTxt.text = itemPrice.ToString();
        button.onClick.AddListener(() => callback?.Invoke());
    }
}
