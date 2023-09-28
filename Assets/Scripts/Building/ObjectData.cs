using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _gameObjectPrefab;


    public string Name => _name;
    public Sprite Sprite => _sprite;
    public int Price => _price;
    public GameObject GameObjectPrefab => _gameObjectPrefab;

}
