using Character;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Triggers
{
    public class DrowningTrigger : MonoBehaviour
    {
        [SerializeField] private MMFeedbacks failFeedback;
        private const string CharacterTag = "Character";
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(CharacterTag)) return;
            var characterSwim = other.GetComponent<CharacterSwim>();
            if (characterSwim.GetCurrentState() == SwimActionState.Falling && characterSwim.GetFailStatus())
            {
                characterSwim.SetDrowning();
                if(other.GetComponent<CharacterMovement>().isPlayerCharacter)
                    failFeedback.PlayFeedbacks();
            }
        }
    }
}
