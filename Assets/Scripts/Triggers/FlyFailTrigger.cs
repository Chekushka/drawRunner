using System;
using Character;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Triggers
{
    public class FlyFailTrigger : MonoBehaviour
    {
        [SerializeField] private MMFeedbacks failFeedback;
        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Character")) return;
            var characterFly = other.GetComponent<CharacterFly>();
            characterFly.SetToFall();
            if(other.GetComponent<CharacterMovement>().isPlayerCharacter)
                failFeedback.PlayFeedbacks();
            gameObject.SetActive(false);
        }
    }
}
