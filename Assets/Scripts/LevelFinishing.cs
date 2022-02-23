using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using MoreMountains.Feedbacks;
using UnityEngine;

public class LevelFinishing : MonoBehaviour
{
   [SerializeField] private Animator finishPlatformAnimator;
   [SerializeField] private GameObject levelCompleteScreen;
   [SerializeField] private float delay;
   [SerializeField] private List<ParticleSystem> confetti;
   [SerializeField] private MMFeedbacks failFeedback;

   private LevelProgressing _levelProgressing;

   private void Start()
   {
       _levelProgressing = FindObjectOfType<LevelProgressing>();
   }

   private void OnTriggerEnter(Collider other)
   {
       if (!other.gameObject.CompareTag("Character")) return;
       var characterMovement = other.GetComponent<CharacterMovement>();

       if (characterMovement.isPlayerCharacter)
       {
           if (_levelProgressing.levelHasWinner)
           {
               failFeedback.PlayFeedbacks();
               characterMovement.SetToFailFinish();
           }
           else
           {
               finishPlatformAnimator.enabled = true;
               foreach (var blast in confetti)
                   blast.Play();
               characterMovement.SetToFinish();
               StartCoroutine(EnableCompleteScreen());
               _levelProgressing.levelHasWinner = true;
           }
       }
       else
       {
           if(_levelProgressing.levelHasWinner)
               characterMovement.SetToFailFinish();
           else
           {
               finishPlatformAnimator.enabled = true;
               foreach (var blast in confetti)
                   blast.Play();
               characterMovement.SetToFinish();
               _levelProgressing.levelHasWinner = true;
           }
       }
   }

   private IEnumerator EnableCompleteScreen()
   {
       yield return new WaitForSeconds(delay);
       levelCompleteScreen.SetActive(true);
   }
}
