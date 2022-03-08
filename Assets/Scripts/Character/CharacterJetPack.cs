using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterJetPack : MonoBehaviour
    {
        [SerializeField] private float jumpDistance;
        [SerializeField] private AnimationCurve flyCurve;
        [SerializeField] private ParticleSystem landParticles;
        [SerializeField] private MMFeedbacks jetPackAppear;
        [SerializeField] private MMFeedbacks jetPackDisappear;
        [SerializeField] private MMFeedbacks balloonFeedback;
        [SerializeField] private float failFlySpeed = 2f;

        public bool isFlyFail;
        
        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        private float _flyingTime;
        private Vector3 _endJumpPoint;

        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponent<CharacterAnimation>();
            _endJumpPoint = Vector3.zero;
        }

        public void StartJetPackAction()
        {
            _characterAnimation.GirlIdleDisable();
            SetJumpEndPoint();
            jetPackAppear.PlayFeedbacks();
            StartCoroutine(JetPackEnable());
        }
       
        public void StartFailJetPackAction()
        {
            _characterAnimation.GirlIdleDisable();
            SetJumpEndPoint();
            balloonFeedback.PlayFeedbacks();
            isFlyFail = true;
            _characterMovement.state = CharacterState.FlyingJetPack;
            _characterAnimation.GirlStartJetPackFly();
        }

        public void SetToMovingAfterJetPack()
        {
            StartCoroutine(PlayLandParticles());
            _characterAnimation.GirlStartMovingAfterFly();
        }
        
        public void JetPackFly()
        {
            if (isFlyFail)
            {
                transform.position += Vector3.up * failFlySpeed * Time.deltaTime; 
            }
            else
            {
                _flyingTime += Time.deltaTime;
                var pos = Vector3.Lerp(transform.position, _endJumpPoint, _flyingTime / 20);
                pos.y += flyCurve.Evaluate(_flyingTime / 10);
                transform.position = pos;
            }
        }

        public void DisableJetpack() => jetPackDisappear.PlayFeedbacks();

        private void SetJumpEndPoint()
        {
            var posZ = jumpDistance;
            _endJumpPoint = transform.position + new Vector3(0,0,posZ);
        }

        private IEnumerator JetPackEnable()
        {
            yield return new WaitForSeconds(0.4f);
            _characterMovement.state = CharacterState.FlyingJetPack;
            _characterAnimation.GirlStartJetPackFly();
            isFlyFail = false;
        }

        private IEnumerator PlayLandParticles()
        {
            yield return new WaitForSeconds(0.4f);
            landParticles.Play();
        }
    }
}
