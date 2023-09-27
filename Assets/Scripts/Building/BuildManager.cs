using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask layerMask;
    private NavMeshPath navMeshPath;

    private bool _isBuildModeOn;

    public ObjectPlaceable turret;
    private Collider turretCollider;

    private ObjectPlaceable currentTurret;

    // Start is called before the first frame update
    void Start()
    {
        navMeshPath = new NavMeshPath();
        //StartCoroutine(WaitBuildMode());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var GO = Instantiate(turret);
            currentTurret = GO.GetComponent<TurretPlaceable>();
        }

        UpdateObjectPosition();

        if (Input.GetMouseButtonDown(0))
        {
            TryToPlaceObject();
        }
    }

    void TryToPlaceObject()
    {
        if (currentTurret == null)
            return;

        if (currentTurret.IsColliding || !PathFinder.IsPathValid)
            return;

        Instantiate(currentTurret.ObjectData.GameObjectPrefab, currentTurret.transform.position, currentTurret.transform.rotation);

        Destroy(currentTurret.gameObject);

    }

    void UpdateObjectPosition()
    {
        if (currentTurret == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, layerMask))
        {
            currentTurret.transform.position = new Vector3(hit.point.x, hit.point.y + (currentTurret.Collider.bounds.size.y + 0.1f) / 2, hit.point.z);
        }
    }


}
