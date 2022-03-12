using Character;
using UnityEngine;

namespace Triggers
{
    public class OnCarSitTrigger : MonoBehaviour
    {
        private const string ThighTag = "Thigh";
        private const int GirlLayer = 7;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != GirlLayer) return;
            if(!other.gameObject.CompareTag(ThighTag)) return;
            var characterRide = other.GetComponentInParent<CharacterRide>();
            if (characterRide.GetCurrentState() == RideActionState.SittingDown)
            {
                characterRide.SetCurrentState(RideActionState.Riding);
                characterRide.OffsetToDriverSeat();
            }
        }
    }
}
