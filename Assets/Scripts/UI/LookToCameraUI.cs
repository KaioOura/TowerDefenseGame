using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCameraUI : MonoBehaviour
{

    private Transform _cameraTransform;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(GetCameraTransform());
    }

    // Update is called once per frame
    void Update()
    {
        if (_cameraTransform == null)
            return;

        transform.LookAt(_cameraTransform);
    }

    IEnumerator GetCameraTransform()
    {
        while(_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
            yield return null;
        }
    }
}
