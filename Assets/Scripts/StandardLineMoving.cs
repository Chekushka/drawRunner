using System;
using Character;
using UnityEngine;

public class StandardLineMoving : MonoBehaviour
{
    [SerializeField] private Transform drawObjectTransform;
    private CharacterMovement _characterMovement;
    private bool _isMovingUpDone;

    private void Start()
    {
        var characterMovementComponents = FindObjectsOfType<CharacterMovement>();
        for (var i = 0; i < characterMovementComponents.Length; i++)
        {
            if (characterMovementComponents[i].isPlayerCharacter)
                _characterMovement = characterMovementComponents[i];
        }
    }

    public void MoveLineToCurrentPos(LineRenderer line)
    {
        var linePoints = new Vector3[line.positionCount];
        line.GetPositions(linePoints);

        if (!_isMovingUpDone && _characterMovement.isOnHighPlatform)
        {
            for (var i = 0; i < linePoints.Length; i++)
                linePoints[i].y += _characterMovement.GetHighPlatformHeight();
            _isMovingUpDone = true;
        }

        for (var i = 0; i < linePoints.Length; i++)
            linePoints[i].z = drawObjectTransform.position.z;
            
        
        line.SetPositions(linePoints);
    }
}
