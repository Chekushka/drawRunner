using UnityEngine;

public class OnRagDollCollisionCameraChanging : MonoBehaviour
{
   [SerializeField] private Transform viewPoint;
   private CameraChanging _cameraChanging;

   private const int GirlLayer = 7;
   private void Start() => _cameraChanging = FindObjectOfType<CameraChanging>();

   private void OnCollisionEnter(Collision other)
   {
      if(other.gameObject.layer != GirlLayer) return;
      _cameraChanging.BalloonCameraViewObjectChange(viewPoint);
   }
}
