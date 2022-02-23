using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   [SerializeField] private CinemachineVirtualCamera virtualCamera;
   private float _shakeTimer;

   private void Update()
   {
      if (_shakeTimer > 0)
      {
         _shakeTimer -= Time.deltaTime;
         if (_shakeTimer <= 0f)
         {
            var cinemachineBasicMultiChannelPerlin = 
               virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
         }
      }
   }

   public void Shake(float intensity, float time)
   {
      var cinemachineBasicMultiChannelPerlin = 
         virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

      cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
      _shakeTimer = time;
   }
   
}
