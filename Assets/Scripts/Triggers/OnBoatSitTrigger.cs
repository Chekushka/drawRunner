using Character;
using UnityEngine;

namespace Triggers
{
    public class OnBoatSitTrigger : MonoBehaviour
    {
        private const string ThighTag = "Thigh";
        private const int GirlLayer = 7;
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != GirlLayer) return;
            if(!other.gameObject.CompareTag(ThighTag)) return;
            var characterSwim = other.GetComponentInParent<CharacterSwim>();
            if (characterSwim.GetCurrentState() == SwimActionState.SittingDown)
                characterSwim.SetCurrentState(SwimActionState.Swimming);
        }
    }
}
