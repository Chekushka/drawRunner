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
        private static readonly int JetPackAppear = Animator.StringToHash("JetPackAppear");
        private static readonly int JetPackDisappear = Animator.StringToHash("JetPackHide");

        #region Girl

        public void GirlIdleEnable() => girlAnimator.SetBool(Idle, true);
        public void GirlIdleDisable() => girlAnimator.SetBool(Idle, false);
        public void GirlStartRun() => girlAnimator.SetTrigger(StartRun);
        public void GirlStartRegularMoving() => girlAnimator.SetTrigger(StartMoving);
        public void GirlStartMovingAfterFly() => girlAnimator.SetTrigger(StartMovingAfterFly);
        public void GirlStartFly() => girlAnimator.SetTrigger(StartFly);
        public void GirlWallFall() => girlAnimator.SetTrigger(WallFall);
        public void GirlSetBeforeJetPackIdle() => girlAnimator.SetTrigger(SetBeforeJetPackIdle);
        public void EnableFinish() => girlAnimator.SetTrigger(SetFinish);
        public void EnableFinishFail() => girlAnimator.SetTrigger(SetFinishFail);
        public void GirlStartJetPackFly() => girlAnimator.SetTrigger(SetJetPackFly);
        
        
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
