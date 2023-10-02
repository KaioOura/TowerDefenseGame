using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawRadiusAround : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 10)]
    public float radius = 5;

    LineRenderer line;

    private void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
    }

    void Start()
    {
    }

    public void CreatePoints(float radius)
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, 0f, y));

            angle += (380f / segments);
        }
    }
}
