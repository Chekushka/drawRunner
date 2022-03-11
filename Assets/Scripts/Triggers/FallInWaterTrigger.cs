using Character;
using UnityEngine;

namespace Triggers
{
    public class FallInWaterTrigger : MonoBehaviour
    {
        private const string CharacterTag = "Character";
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(CharacterTag)) return;
            var characterSwim = other.GetComponent<CharacterSwim>();
            if (characterSwim.GetCurrentState() == SwimActionState.WalkingToBoat && characterSwim.GetFailStatus())
                characterSwim.SetCurrentState(SwimActionState.Falling);
        }
    }
}
