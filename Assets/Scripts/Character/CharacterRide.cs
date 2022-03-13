using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterRide : MonoBehaviour
    {
        [SerializeField] private GameObject car;
        [SerializeField] private float sitDownSpeed;
        [SerializeField] private float carSpeed;
        [SerializeField] private float sittingPosOffset = 0.4f;
        [SerializeField] private GameObject board;
        [SerializeField] private float skateBoardOffset;
        [SerializeField] private GameObject girlBodyObject;
        [SerializeField] private MMFeedbacks failFeedback;

        private RideActionState _currentState;
        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        private float _lastPlatformHeightPos;

        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponent<CharacterAnimation>();
        }

        public void Ride(float walkingSpeed)
        {
            switch (_currentState)
            {
                case RideActionState.WalkingToCar:
                    transform.position += Vector3.forward * walkingSpeed * Time.deltaTime;
                    break;
                case RideActionState.SittingDown:
                    transform.position += Vector3.down * sitDownSpeed * Time.deltaTime;
                    break;
                case RideActionState.Riding:
                    transform.position += Vector3.forward * carSpeed * Time.deltaTime;
                    car.transform.position += Vector3.forward * carSpeed * Time.deltaTime;
                    break;
            }
        }

        public void StartRideAction()
        {
            _characterAnimation.GirlIdleDisable();
            car.SetActive(true);
            _characterMovement.state = CharacterState.CarRiding;
        }

        public void StartFailRideAction()
        {
            transform.position += Vector3.up * skateBoardOffset ;
            board.SetActive(true);
            _characterAnimation.GirlIdleDisable();
            _characterAnimation.GirlSetSkateBoardRide();
            StartCoroutine(WaitForStartAction());
        }

        public void SetCurrentState(RideActionState state) => _currentState = state;
        public RideActionState GetCurrentState() => _currentState;

        public void SetSitting() => _characterAnimation.GirlSetToSitting();
        public void OffsetToDriverSeat() => transform.position += Vector3.left * sittingPosOffset;

        public void OffsetBackToCentre() => transform.position += Vector3.right * sittingPosOffset;
            

        public void SetLastHeightPos() => _lastPlatformHeightPos = transform.position.y;
        public void MoveToLastHeightPos() => transform.position = 
            new Vector3(transform.position.x, _lastPlatformHeightPos, transform.position.z);

        private IEnumerator WaitForStartAction()
        {
            yield return new WaitForSeconds(0.4f);
            _characterMovement.state = CharacterState.CarRiding;
            StartCoroutine(DelayedRagDollEnable());
        }

        private IEnumerator DelayedRagDollEnable()
        {
            yield return new WaitForSeconds(0.8f);
            _characterAnimation.DisableGirlAnimator();
            var bodies = girlBodyObject.GetComponentsInChildren<Rigidbody>();
            board.GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Collider>().enabled = false;
            foreach (var rb in bodies)
                rb.isKinematic = false;
            if(_characterMovement.isPlayerCharacter)
                failFeedback.PlayFeedbacks();
        }
    }

    public enum RideActionState
    {
        WalkingToCar,
        SittingDown,
        Riding
    }
}
