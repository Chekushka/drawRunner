using System.Collections;
using Character;
using UnityEngine;

namespace Triggers
{
    public class ActionStopTrigger : MonoBehaviour
    {
        [SerializeField] private float delay;

        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("Character")) return;
            StartCoroutine(StopAction(other.GetComponent<CharacterMovement>()));
        }
    
        private IEnumerator StopAction(CharacterMovement character)
        {
            character.StartMovingAnimation();

            yield return new WaitForSeconds(delay);
            character.SetToMoving();
        }
    }
}
