using Character;
using UnityEngine;

namespace Triggers
{
    public class CarSitDownTrigger : MonoBehaviour
    {
        private const string CharacterTag = "Character";
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(CharacterTag)) return;
            var characterRide = other.GetComponent<CharacterRide>();
            if (characterRide.GetCurrentState() == RideActionState.WalkingToCar)
            {
                characterRide.SetLastHeightPos();
                characterRide.SetCurrentState(RideActionState.SittingDown);
                characterRide.SetSitting();
            }
        }
    }
}
