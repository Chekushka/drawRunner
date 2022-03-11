using System.Collections;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterSwim : MonoBehaviour
    {
        [SerializeField] private GameObject boat;
        [SerializeField] private float sitDownSpeed;
        [SerializeField] private float boatSpeed;
        [SerializeField] private float failRotationSpeed;
        [SerializeField] private GameObject duck;
        
        private SwimActionState _currentState;
        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        private float _lastPlatformHeightPos;
        private bool _isFail;

        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponent<CharacterAnimation>();
        }

        public void Swim(float walkingSpeed)
        {
            switch (_currentState)
            {
                case SwimActionState.WalkingToBoat:
                    transform.position += Vector3.forward * walkingSpeed * Time.deltaTime;
                    break;
                case SwimActionState.SittingDown:
                    transform.position += Vector3.down * sitDownSpeed * Time.deltaTime;
                    break;
                case SwimActionState.Swimming:
                    transform.position += Vector3.forward * boatSpeed * Time.deltaTime;
                    boat.transform.position += Vector3.forward * boatSpeed * Time.deltaTime;
                    break;
                case SwimActionState.Falling:
                    transform.position += 
                        (Vector3.down * sitDownSpeed / 2.5f + Vector3.forward * walkingSpeed) * Time.deltaTime;
                    break;
                case SwimActionState.Drowning:
                    transform.RotateAround(transform.position, transform.up, 
                        Time.deltaTime * failRotationSpeed);
                    break;
            }
        }

        public void StartSwimAction()
        {
            _characterAnimation.GirlIdleDisable();
            boat.SetActive(true);
            _characterMovement.state = CharacterState.BoatSwimming;
            _isFail = false;
        }

        public void StartFailSwimAction()
        {
            duck.SetActive(true);
            StartCoroutine(WaitForStartAction());
        }

        public void SetCurrentState(SwimActionState state) => _currentState = state;
        public SwimActionState GetCurrentState() => _currentState;

        public void SetSitting() => _characterAnimation.GirlSetToSitting();
        public void SetSwimming() => _characterAnimation.GirlIdleEnable();

        public void SetDrowning()
        {
            SetCurrentState(SwimActionState.Drowning);
            _characterAnimation.GirlSetToDrowning();
        }

        public void SetLastHeightPos() => _lastPlatformHeightPos = transform.position.y;
        public void MoveToLastHeightPos() => transform.position = 
            new Vector3(transform.position.x, _lastPlatformHeightPos, transform.position.z);

        public bool GetFailStatus() => _isFail;

        private IEnumerator WaitForStartAction()
        {
            yield return new WaitForSeconds(0.4f);
            _characterAnimation.GirlIdleDisable();
            _characterMovement.state = CharacterState.BoatSwimming;
            _isFail = true;
        }
    }

    public enum SwimActionState
    {
        WalkingToBoat,
        SittingDown,
        Swimming,
        Falling,
        Drowning
    }
}
