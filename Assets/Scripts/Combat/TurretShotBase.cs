using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurretShotBase : MonoBehaviour, IShotInitializer
{
    public float Speed { get; set; }
    public ShotTravelEnum ShotTravelType { get; set; }
    public CharacterEffectPack CharacterEffectPack { get; set ; }

    private IEnumerator FollowTargetRoutine;
    private IEnumerator FollowPositionRoutine;

    protected event Action OnTargetReached;

    public virtual void Initialize(int level, float speed, ShotTravelEnum shotTravelEnum, ITargetable target)
    {

    }

    public virtual void LaunchShot(ITargetable target)
    {
        FollowTarget(target);
        return;

        //if (ShotTravelType == ShotTravelEnum.FollowTarget)
        //    FollowTarget(target);
        //else
        //    FollowPosition(target.GetTransform().position);

    }

    public virtual void UpdateShotStats(int level)
    {

    }

    public virtual void FollowTarget(ITargetable target)
    {
        if (FollowTargetRoutine != null)
            StopCoroutine(FollowTargetRoutine);

        FollowTargetRoutine = FollowTargetCoroutine(target);
        StartCoroutine(FollowTargetRoutine);
    }

    //public virtual void FollowPosition(Vector3 position)
    //{
    //    if (FollowPositionRoutine != null)
    //        StopCoroutine(FollowPositionRoutine);

    //    FollowPositionRoutine = FollowPositionCoroutine(position);
    //    StartCoroutine(FollowPositionRoutine);
    //}

    IEnumerator FollowTargetCoroutine(ITargetable target)
    {
        Vector3 targetPos = target.GetTransform().position;

        if (ShotTravelType == ShotTravelEnum.FollowTarget)
        {
            while (Vector3.Distance(transform.position, target.GetTransform().position) >= 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.GetTransform().position, Time.deltaTime * Speed);

                yield return null;
            }
        }
        else
        {
            while (Vector3.Distance(transform.position, targetPos) >= 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * Speed);

                yield return null;
            }
        }

        target.ApplyCharacterEffect(CharacterEffectPack);
        OnTargetReached?.Invoke();


        ResetShot();
    }

    IEnumerator FollowPositionCoroutine(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * 10);

            yield return null;
        }

        ResetShot();
    }

    public virtual void ResetShot()
    {
        gameObject.SetActive(false);
    }

}
