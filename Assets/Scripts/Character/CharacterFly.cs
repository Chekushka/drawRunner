using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterFly : MonoBehaviour
    {
        [Header("Fly parameters")]
        [SerializeField] private float jumpDistance;
        [SerializeField] private AnimationCurve flyCurve;
        [SerializeField] private AnimationCurve failFlyCurve;
        [SerializeField] private ParticleSystem landParticles;
        [SerializeField] private ParticleSystem wingsAppearParticles;
        [SerializeField] private SkinnedMeshRenderer wings;
        [SerializeField] private Material normalWings;
        [SerializeField] private Material smallWings;
        [SerializeField] private GameObject girlObject;
        
        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        private CameraChanging _cameraChanging;
        
        private Vector3 _endJumpPoint;
        private AnimationCurve _currentCurve;
        private float _flyingTime;

        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponent<CharacterAnimation>();
            _cameraChanging = FindObjectOfType<CameraChanging>();
            _endJumpPoint = Vector3.zero;
            SetRagDollKinematic(true);
        }

        public void StartFlyAction()
        {
            _characterAnimation.GirlIdleDisable();
            _characterAnimation.GirlStartFly();
            _characterAnimation.WingsStartFly();
            wingsAppearParticles.Play();
            wings.material = normalWings;
            SetJumpEndPoint(true);
            _characterMovement.state = CharacterState.Flying;
            _currentCurve = flyCurve;
        }
        
        public void StartFailFlyAction()
        {
            _characterAnimation.GirlIdleDisable();
            _characterAnimation.GirlStartFly();
            _characterAnimation.WingsStartFailFly();
            wingsAppearParticles.Play();
            wings.material = smallWings;
            SetJumpEndPoint(false);
            _characterMovement.state = CharacterState.Flying;
            _currentCurve = failFlyCurve;
        }
        
        public void Fly()
        {
            _flyingTime += Time.deltaTime;
            var pos = Vector3.Lerp(transform.position, _endJumpPoint, _flyingTime / 50);
            pos.y += _currentCurve.Evaluate(_flyingTime / 4);
            transform.position = pos;
        }
        
        private void SetJumpEndPoint(bool isNotFail)
        {
            var posZ = isNotFail ? jumpDistance : jumpDistance / 3 ;
            _endJumpPoint = transform.position + new Vector3(0,0,posZ);
        }
        
        public void SetToFall()
        {
            _characterAnimation.DisableGirlAnimator();
            SetRagDollKinematic(false);
            if (_characterMovement.state == CharacterState.FlyingJetPack)
                _cameraChanging.ChangeCamera(CameraType.Balloon);
        }

        public void SetToMovingAfterFly()
        {
            StartCoroutine(PlayLandParticles());
            _characterAnimation.GirlStartMovingAfterFly();
        }

        private void SetRagDollKinematic(bool newValue)
        {
            var bodies = girlObject.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in bodies)
                rb.isKinematic = newValue;
        }
        
        
        private IEnumerator PlayLandParticles()
        {
            yield return new WaitForSeconds(0.4f);
            landParticles.Play();
        }
    }
}
