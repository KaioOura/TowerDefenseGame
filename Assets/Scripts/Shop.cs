using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static bool IsPurchaseValid(float price)
    {
        if (price > Player.Currency)
        {
            Debug.Log("Not enought currency");
            return false;
        }

        return true;
    }

    public static void AddPlayerCurrency(int price)
    {
        Player.AddCurrency(price);
    }

    public static void RemovePlayerCurrency(int price)
    {
        Player.RemoveCurrency(price);
    }
}
