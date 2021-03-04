using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewDist;
    [Range (0,360)]
    public float viewAngle;

    public float meshResolution;
    
    void DrawFieldOfView()
    {
        int rayCount = (int)(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / rayCount;
        for (int i = 0; i <= rayCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
        }

    }
    public Vector3 DirFromAngle(float deg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            deg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(deg * Mathf.Deg2Rad), 0, Mathf.Cos(deg * Mathf.Deg2Rad));
    }
}
