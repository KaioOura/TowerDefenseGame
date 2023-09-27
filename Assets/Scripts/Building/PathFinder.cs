using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgentBase;
    [SerializeField] private Transform Target;
    private NavMeshPath navMeshPath;

    private NavMeshPath _currentNavMeshPath;

    private bool _updateNavMeshPath;

    public static bool IsPathValid;


    public NavMeshPath CurrentNavMeshPath => _currentNavMeshPath;

    // Start is called before the first frame update
    void Start()
    {
        navMeshPath = new NavMeshPath();
        StartCoroutine(WaitBuildMode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitBuildMode()
    {
        _updateNavMeshPath = true;

        while (_updateNavMeshPath)
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
            yield return null;
        }
    }
}
