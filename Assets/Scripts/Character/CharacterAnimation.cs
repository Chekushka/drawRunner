using UnityEngine;

namespace Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        [Header("Animators")]
        [SerializeField] private Animator girlAnimator;
        [SerializeField] private Animator wingsAnimator;
        [SerializeField] private Animator helmetAnimator;

        private static readonly int StartFly = Animator.StringToHash("StartFly");
        private static readonly int StartWingsFly = Animator.StringToHash("StartWingsFly");
        private static readonly int StartWingsFailFly = Animator.StringToHash("StartWingsFailFly");
        private static readonly int StartRun = Animator.StringToHash("StartRun");
        private static readonly int Idle = Animator.StringToHash("Idle");
        private static readonly int StartMoving = Animator.StringToHash("StartMoving");
        private static readonly int StartMovingAfterFly = Animator.StringToHash("StartMovingAfterFly");
        private static readonly int WallFall = Animator.StringToHash("WallFall");
        private static readonly int HelmetAppear = Animator.StringToHash("HelmetAppear");
        private static readonly int HelmetDisappear = Animator.StringToHash("HelmetDisappear");
        private static readonly int SetFinish = Animator.StringToHash("SetFinish");
        private static readonly int SetFinishFail = Animator.StringToHash("SetFinishFail");
        private static readonly int SetBeforeJetPackIdle = Animator.StringToHash("SetBeforeJetPackIdle");
        private static readonly int SetJetPackFly = Animator.StringToHash("SetJetPackFly");
        private static readonly int SetBalloonFly = Animator.StringToHash("SetBalloonFly");
        private static readonly int SetToSitting = Animator.StringToHash("SetToSitting");
        private static readonly int SetDrowning = Animator.StringToHash("SetDrowning");
        private static readonly int SetExtinguishing = Animator.StringToHash("SetExtinguishing");
        private static readonly int SetSkateBoardRide = Animator.StringToHash("SetSkateBoardRide");

        #region Girl

        public void GirlIdleEnable() => girlAnimator.SetBool(Idle, true);
        public void GirlIdleDisable() => girlAnimator.SetBool(Idle, false);
        public void GirlStartRun() => girlAnimator.SetTrigger(StartRun);
        public void GirlStartRegularMoving() => girlAnimator.SetTrigger(StartMoving);
        public void GirlStartMovingAfterFly() => girlAnimator.SetTrigger(StartMovingAfterFly);
        public void GirlStartFly() => girlAnimator.SetTrigger(StartFly);
        public void GirlWallFall() => girlAnimator.SetTrigger(WallFall);
        public void EnableFinish() => girlAnimator.SetTrigger(SetFinish);
        public void EnableFinishFail() => girlAnimator.SetTrigger(SetFinishFail);
        public void GirlStartJetPackFly() => girlAnimator.SetTrigger(SetJetPackFly);
        public void GirlSetBalloonFly() => girlAnimator.SetTrigger(SetBalloonFly);
        public void GirlSetToSitting() => girlAnimator.SetTrigger(SetToSitting);
        public void GirlSetToDrowning() => girlAnimator.SetTrigger(SetDrowning);
        public void GirlSetExtinguishing() => girlAnimator.SetTrigger(SetExtinguishing);
        public void GirlSetSkateBoardRide() => girlAnimator.SetTrigger(SetSkateBoardRide);
    
        #endregion

        #region Wings

        public void WingsStartFly() => wingsAnimator.SetTrigger(StartWingsFly);
        public void WingsStartFailFly() => wingsAnimator.SetTrigger(StartWingsFailFly);

        #endregion

        #region Helmet

        public void HelmetShow() => helmetAnimator.SetTrigger(HelmetAppear);
        public void HelmetHide() => helmetAnimator.SetTrigger(HelmetDisappear);
        
        #endregion

        public void DisableGirlAnimator() => girlAnimator.enabled = false;
    }
}
