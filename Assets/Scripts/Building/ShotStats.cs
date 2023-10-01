using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShotStats
{
    public CharacterEffectPack CharacterEffectPack;
}

[System.Serializable]
public class ExplosiveShotStats : ShotStats
{
    public CharacterEffectPack ExplosiveCharacterEffectPack;
    public float ExplosionRadius;
}

