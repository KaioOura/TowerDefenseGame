using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectData : ScriptableObject
{
    [SerializeField] private GameObject _gameObjectPrefab;

    public GameObject GameObjectPrefab => _gameObjectPrefab;
}
