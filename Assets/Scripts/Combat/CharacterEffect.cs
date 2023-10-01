using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEffect : MonoBehaviour
{ 
    public event Action<float> OnReceiveDamage;
    public event Action<float> OnReceiveHeal;
    public event Action<float> OnReceiveStagger;
    public event Action<float, float> OnReceiveSlow;
    public event Action<float, float> OnReceiveSpeed;

    public void OnReceiveEffect(CharacterEffectPack characterEffectPack)
    {
        if (characterEffectPack.CharacterEffectEnum.HasFlag(CharacterEffectEnum.Damage))
            OnReceiveDamage?.Invoke(characterEffectPack.Damage);

        if (characterEffectPack.CharacterEffectEnum.HasFlag(CharacterEffectEnum.Heal))
            OnReceiveHeal?.Invoke(characterEffectPack.Heal);

        if (characterEffectPack.CharacterEffectEnum.HasFlag(CharacterEffectEnum.Stagger))
            OnReceiveStagger?.Invoke(characterEffectPack.StaggerTime);

        if (characterEffectPack.CharacterEffectEnum.HasFlag(CharacterEffectEnum.Slow))
            OnReceiveSlow?.Invoke(characterEffectPack.SlowAmount, characterEffectPack.SlowTime);

        if (characterEffectPack.CharacterEffectEnum.HasFlag(CharacterEffectEnum.Speed))
            OnReceiveSpeed?.Invoke(characterEffectPack.SpeedAmount, characterEffectPack.SpeedTime);
    }
}

[Serializable]
public class CharacterEffectPack
{
    [SerializeField]
    private CharacterEffectEnum _effectEnum;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _heal;

    [SerializeField]
    private float _staggerTime;

    [SerializeField]
    private float _slowTime;

    [SerializeField]
    private float _slowAmount;

    [SerializeField]
    private float _speedTime;

    [SerializeField]
    private float _speedAmount;

    public CharacterEffectEnum CharacterEffectEnum => _effectEnum;
    public float Damage => _damage;
    public float Heal => _heal;
    public float StaggerTime =>_staggerTime;
    public float SlowTime => _slowTime;
    public float SlowAmount => _slowAmount;
    public float SpeedTime => _speedTime;
    public float SpeedAmount => _speedAmount;

}

[Flags]
public enum CharacterEffectEnum
{
    Damage = 1,
    Stagger = 2,
    Slow = 4,
    Speed = 8,
    Heal = 12,
}
