using System.Collections.Generic;
using UnityEngine;

public class StandardLineMoving : MonoBehaviour
{
    [SerializeField] private Transform drawObjectTransform;

    public void MoveLineToCurrentPos(LineRenderer line)
    {
        var linePoints = new Vector3[line.positionCount];
        line.GetPositions(linePoints);
        
        for (var i = 0; i < linePoints.Length; i++)
            linePoints[i].z = drawObjectTransform.position.z;
        
        line.SetPositions(linePoints);
    }
}
