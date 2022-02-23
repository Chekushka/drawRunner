using Character;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FlyFailTrigger : MonoBehaviour
{
    [SerializeField] private MMFeedbacks failFeedback;
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Character")) return;
        var characterFly = other.GetComponent<CharacterFly>();
        characterFly.SetToFall();
        failFeedback.PlayFeedbacks();
        gameObject.SetActive(false);
    }
}
