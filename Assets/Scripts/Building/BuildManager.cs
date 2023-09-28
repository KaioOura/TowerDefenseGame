using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask layerMask;
    private NavMeshPath navMeshPath;

    private bool _isBuildModeOn;

    public ObjectPlaceable turret;
    private Collider turretCollider;

    public ObjectPlaceable CurrentPlaceable => currentTurret;

    private ObjectPlaceable currentTurret;

    public static event Action<ObjectData> OnObjectPlaced;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        navMeshPath = new NavMeshPath();
        //StartCoroutine(WaitBuildMode());
    }

    // Update is called once per frame
    void Update()
    {

        UpdateObjectPosition();

        if (Input.GetMouseButtonDown(0))
        {
            TryToPlaceObject();
        }
    }

    public static void PreparePlacement(ObjectData objectData)
    {
        if (Instance == null)
            return;

        Instance.PrepareLocalPlacement(objectData);
    }

    public void PrepareLocalPlacement(ObjectData objectData)
    {
        var GO = Instantiate(objectData.GameObjectPrefab);
        currentTurret = GO.GetComponent<Turret>();
    }

    void TryToPlaceObject()
    {
        if (CurrentPlaceable == null)
            return;

        if (CurrentPlaceable.IsColliding || !PathFinder.IsPathValid)
            return;

        OnObjectPlaced?.Invoke(currentTurret.ObjectData);

        Instantiate(CurrentPlaceable.ObjectData.GameObjectPrefab, CurrentPlaceable.transform.position, CurrentPlaceable.transform.rotation);

        Destroy(CurrentPlaceable.gameObject);

    }

    void UpdateObjectPosition()
    {
        if (CurrentPlaceable == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000, layerMask))
        {
            CurrentPlaceable.transform.position = new Vector3(hit.point.x, hit.point.y + (CurrentPlaceable.Collider.bounds.size.y + 0.1f) / 2, hit.point.z);
        }
    }


}
