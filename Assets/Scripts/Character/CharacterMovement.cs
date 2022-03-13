using Triggers;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterMovement : MonoBehaviour
    {
        public bool isPlayerCharacter;
        public bool isOnHighPlatform;
        public CharacterState state;
        [SerializeField] private float movementSpeed;

        private CharacterRun _characterRun;
        private CharacterFly _characterFly;
        private CharacterJetPack _characterJetPack;
        private CharacterSwim _characterSwim;
        private CharacterExtinguishing _characterExtinguishing;
        private CharacterRide _characterRide;
        private CharacterAnimation _characterAnimation;
        private CameraChanging _cameraChanging;

        private float _currentHeight;
        private const float HighPlatformHeight = 10f;

        private void Start()
        {
            _characterRun = GetComponent<CharacterRun>();
            _characterFly = GetComponent<CharacterFly>();
            _characterJetPack = GetComponent<CharacterJetPack>();
            _characterSwim = GetComponent<CharacterSwim>();
            _characterExtinguishing = GetComponent<CharacterExtinguishing>();
            _characterRide = GetComponent<CharacterRide>();
            _characterAnimation = GetComponent<CharacterAnimation>();
            _cameraChanging = FindObjectOfType<CameraChanging>();
            state = CharacterState.Moving;
            _currentHeight = transform.position.y;
        }

        private void Update()
        {
            switch (state)
            {
                case CharacterState.Moving:
                    transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
                    break;
                case CharacterState.Flying:
                    _characterFly.Fly();
                    break;
                case CharacterState.Running:
                    _characterRun.Run();
                    break;
                case CharacterState.FlyingJetPack:
                    _characterJetPack.JetPackFly();
                    break;
                case CharacterState.BoatSwimming:
                    _characterSwim.Swim(movementSpeed);
                    break;
                case CharacterState.Extinguishing:
                    _characterExtinguishing.Extinguish();
                    break;
                case CharacterState.CarRiding:
                    _characterRide.Ride(movementSpeed);
                    break;
                case CharacterState.Idle:
                    break;
            }
        }

        public void StartCorrectAction(Item item)
        {
            switch (item)
            {
                case Item.Wings:
                    _characterFly.StartFlyAction();
                    break;
                case Item.Helmet:
                    _characterRun.StartRunAction();
                    if(isPlayerCharacter) 
                        _cameraChanging.ChangeCamera(CameraType.WallBrake);
                    break;
                case Item.JetPack:
                    _characterJetPack.StartJetPackAction();
                    isOnHighPlatform = true;
                    break;
                case Item.Boat:
                    _characterSwim.StartSwimAction();
                    break;
                case Item.Extinguisher:
                    _characterExtinguishing.StartExtinguishing();
                    break;
                case Item.Car:
                    _characterRide.StartRideAction();
                    if(isPlayerCharacter) 
                        _cameraChanging.ChangeCamera(CameraType.Riding);
                    break;
            }
        }
        public void StartFailAction(Item item)
        {
            switch (item)
            {
                case Item.Wings:
                    _characterFly.StartFailFlyAction();
                    break;
                case Item.Helmet:
                    _characterRun.StartFailRunAction();
                    if(isPlayerCharacter) 
                        _cameraChanging.ChangeCamera(CameraType.WallBrake);
                    break;
                case Item.JetPack:
                    _characterJetPack.StartFailJetPackAction();
                    break;
                case Item.Boat:
                    _characterSwim.StartFailSwimAction();
                    break;
                case Item.Extinguisher:
                    _characterExtinguishing.StartFailExtinguishing();
                    break;
                case Item.Car:
                    _characterRide.StartFailRideAction();
                    break;
            }
        }

        public void SetToIdle()
        {
            _characterAnimation.GirlIdleEnable();
            state = CharacterState.Idle;
        }

        public void StartMovingAnimation()
        {
            switch (state)
            {
                case CharacterState.Flying:
                    _characterFly.SetToMovingAfterFly();
                    break;
                case CharacterState.FlyingJetPack:
                    state = CharacterState.Idle;
                    _characterJetPack.DisableJetpack();
                    _characterJetPack.SetToMovingAfterJetPack();
                    _currentHeight += HighPlatformHeight;
                    transform.position = new Vector3(transform.position.x,_currentHeight,transform.position.z);
                    break;
                default:
                    _characterAnimation.GirlStartRegularMoving();
                    break;
            }
        }

        public void SetToMoving()
        {
            switch (state)
            {
                case CharacterState.Running:
                    _characterRun.DisableHelmet();
                    break;
                case CharacterState.BoatSwimming:
                    _characterAnimation.GirlIdleDisable();
                    _characterSwim.MoveToLastHeightPos();
                    break;
                case CharacterState.CarRiding:
                    _characterAnimation.GirlIdleDisable();
                    _characterRide.MoveToLastHeightPos();
                    _characterRide.OffsetBackToCentre();
                    break;
            }

            state = CharacterState.Moving;
            _characterRun.isAbleToDestroyWall = false;
            if(isPlayerCharacter)
                _cameraChanging.ChangeCamera(CameraType.Main);
        }

        public void SetToFinish()
        {
            state = CharacterState.Idle;
            transform.rotation = Quaternion.Euler(0,180,0);
            _characterAnimation.EnableFinish();
            if(isPlayerCharacter)
                _cameraChanging.ChangeCamera(CameraType.Finish);
        }

        public void SetToFailFinish()
        {
            state = CharacterState.Idle;
            transform.rotation = Quaternion.Euler(0,180,0);
            _characterAnimation.EnableFinishFail();
            if(isPlayerCharacter)
                _cameraChanging.ChangeCamera(CameraType.Finish);
        }

        public float GetHighPlatformHeight() => HighPlatformHeight;
    }

    public enum CharacterState
    {
        Idle,
        Moving,
        Flying,
        Running,
        FlyingJetPack,
        BoatSwimming,
        Extinguishing,
        CarRiding
    }
}