using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceable : MonoBehaviour
{
    protected bool _isColliding;
    protected bool isOn;
    protected Collider _collider;

    [SerializeField]
    protected LayerMask _collisionLayer;

    [SerializeField] private ObjectData _objectData;
    public ObjectData ObjectData => _objectData;

    //public bool IsColliding => _isColliding;
    public Collider Collider => _collider;

    public virtual void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isOn)
            return;


    }

    public virtual bool IsColliding()
    {
        var colliders = Physics.OverlapBox(transform.position, new Vector3(_collider.bounds.size.x, _collider.bounds.size.y, _collider.bounds.size.z));

        return colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        if (!_collider)
            return;

        Gizmos.DrawCube(transform.position, new Vector3(_collider.bounds.size.x, _collider.bounds.size.y, _collider.bounds.size.z));
    }

    public void TurnOnOff(bool value)
    {
        isOn = value;
    }

    //public virtual void OnTriggerEnter(Collider other)
    //{
    //    _isColliding = true;
    //}

    //public virtual void OnTriggerStay(Collider other)
    //{
    //    _isColliding = true;
    //}

    //public virtual void OnTriggerExit(Collider other)
    //{
    //    _isColliding = false;
    //}
}
