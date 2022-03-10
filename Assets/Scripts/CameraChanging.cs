using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanging : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;
   
    public void ChangeCamera(CameraType cameraType)
    {
        SetCameraPrioritiesToZero();
        cameras[(int)cameraType].Priority = 1;
    }

    public void BalloonCameraViewObjectChange(Transform newViewObject)
    {
        cameras[(int)CameraType.Balloon].m_Follow = newViewObject;
        cameras[(int)CameraType.Balloon].m_LookAt = newViewObject;
    }
    
    private void SetCameraPrioritiesToZero()
    {
        foreach (var cam in cameras)
            cam.Priority = 0;
    }
}

public enum CameraType
{
    Main,
    Draw,
    WallBrake,
    Finish,
    TwoCharactersStart,
    Balloon
}
