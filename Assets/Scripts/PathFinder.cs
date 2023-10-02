using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgentBase;
    [SerializeField] private Transform Target;

    [Header("Listener")]
    [SerializeField] 
    private BoolEventChannel OnEnterBuildEventChannel;

    private NavMeshPath navMeshPath;

    private NavMeshPath _currentNavMeshPath;

    private bool _updateNavMeshPath;

    public static bool IsPathValid;


    public NavMeshPath CurrentNavMeshPath => _currentNavMeshPath;

    // Start is called before the first frame update
    void Start()
    {
        OnEnterBuildEventChannel.OnEventRaised += ShouldCalcPath;

        navMeshPath = new NavMeshPath();
        StartCoroutine(WaitBuildMode());
    }

    private void OnDestroy()
    {
        OnEnterBuildEventChannel.OnEventRaised -= ShouldCalcPath;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShouldCalcPath(bool value)
    {
        _updateNavMeshPath = value;
    }

    IEnumerator WaitBuildMode()
    {
        while (true)
        {
            if (_updateNavMeshPath)
            {
                //Checar path
                if (navMeshAgentBase.CalculatePath(Target.position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                {
                    //move to target
                    Debug.Log("Path Available");
                    _currentNavMeshPath = navMeshPath;
                    IsPathValid = true;
                }
                else
                {
                    //Fail condition here
                    Debug.Log("Path Unvailable");
                    IsPathValid = false;

                }
            }

            yield return null;
        }
    }
}
