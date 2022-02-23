using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterRun : MonoBehaviour
    {
        [Header("Run parameters")] 
        [SerializeField] private float runSpeed;
        [SerializeField] private GameObject hat;
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private ParticleSystem failStarParticles;
        [SerializeField] private ParticleSystem itemAppearParticles;
        [SerializeField] private Collider helmetCollider;
        public bool isAbleToDestroyWall;

        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponent<CharacterAnimation>();
        }
        
        public void Run() => transform.position += Vector3.forward * runSpeed * Time.deltaTime;

        public void StartRunAction()
        {
            _characterAnimation.GirlIdleDisable();
            _characterAnimation.GirlStartRun();
            _characterAnimation.HelmetShow();
            _characterMovement.state = CharacterState.Running;
            itemAppearParticles.Play();
            helmetCollider.enabled = true;
            isAbleToDestroyWall = true;
        }
        public void StartFailRunAction()
        {
            _characterAnimation.GirlIdleDisable();
            _characterAnimation.GirlStartRun();
            _characterMovement.state = CharacterState.Running;
            itemAppearParticles.Play();
            helmetCollider.enabled = false;
            hat.SetActive(true);
            isAbleToDestroyWall = false;
        }
        
        public void WallFailFall()
        {
            _characterMovement.state = CharacterState.Idle;
            _characterAnimation.GirlWallFall();
            failStarParticles.Play();
        }
        
        public void DisableHelmet() => _characterAnimation.HelmetHide();

        public void PlayHitParticles() =>  hitParticle.Play();
    }
}
