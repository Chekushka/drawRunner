using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovement), typeof(CharacterAnimation))]
    public class CharacterExtinguishing : MonoBehaviour
    {
        [SerializeField] private float movingSpeed;
        [SerializeField] private MMFeedbacks extinguisherFeedback;
        [SerializeField] private MMFeedbacks firesFeedback;
        [SerializeField] private MMFeedbacks tntFeedback;
        [SerializeField] private Renderer girlRenderer;
        [SerializeField] private Renderer girlHairRenderer;
        [SerializeField] private ParticleSystem explosion;
        [SerializeField] private ParticleSystem tntSmallExplosion;
        [SerializeField] private GameObject tnt;
        [SerializeField] private MMFeedbacks failFeedback;
        [SerializeField] private GameObject girlObject;
        [SerializeField] private Color burnedBodyColor;
        [SerializeField] private ParticleSystem burnSmoke;
        private CharacterMovement _characterMovement;
        private CharacterAnimation _characterAnimation;
        
        private void Start()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterAnimation = GetComponent<CharacterAnimation>();
        }

        public void Extinguish() => transform.position += Vector3.forward * movingSpeed * Time.deltaTime;

        public void StartExtinguishing()
        {
            extinguisherFeedback.PlayFeedbacks();
            StartCoroutine(WaitForAction(false));
        }
        
        public void StartFailExtinguishing()
        {
            tntFeedback.PlayFeedbacks();
            StartCoroutine(WaitForAction(true));
        }

        private IEnumerator WaitForAction(bool isFail)
        {
            yield return new WaitForSeconds(0.5f);
            _characterAnimation.GirlIdleDisable();
            _characterAnimation.GirlSetExtinguishing();
            _characterMovement.state = CharacterState.Extinguishing;
            if (!isFail)
                firesFeedback.PlayFeedbacks();
            else
                StartCoroutine(PlayFail());
        }

        private void EnableRagDoll()
        {
            _characterAnimation.DisableGirlAnimator();
            var bodies = girlObject.GetComponentsInChildren<Rigidbody>();
            foreach (var rb in bodies)
                rb.isKinematic = false;
            GetComponent<Collider>().enabled = false;
        }

        private IEnumerator PlayFail()
        {
            yield return new WaitForSeconds(2f);
            explosion.Play();
            tntSmallExplosion.Play();
            tnt.SetActive(false);
            _characterMovement.state = CharacterState.Idle;
            girlRenderer.material.color = burnedBodyColor;
            var burnedHairMaterials = girlHairRenderer.materials;
            for (var i = 0; i < burnedHairMaterials.Length; i++)
                burnedHairMaterials[i].color = burnedBodyColor;
            girlHairRenderer.materials = burnedHairMaterials;
            _characterAnimation.GirlWallFall();
            //EnableRagDoll();
            failFeedback.PlayFeedbacks();
        }
    }
}
