using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceable : MonoBehaviour
{
    protected bool _isColliding;
    protected Collider _collider;
    [SerializeField] private ObjectData _objectData;
    public ObjectData ObjectData => _objectData;

    public bool IsColliding => _isColliding;
    public Collider Collider => _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        _isColliding = true;
    }

    public virtual void OnTriggerStay(Collider other)
    {
        _isColliding = true;
    }

    public virtual void OnTriggerExit(Collider other)
    {
        _isColliding = false;
    }
}
