using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShotStats
{
    public CharacterEffectPack ShotCharacterEffectPack;
}

[System.Serializable]
public class AreaShotStats : ShotStats
{
    public CharacterEffectPack AreaCharacterEffectPack;
    public float AreaRadius;
}

