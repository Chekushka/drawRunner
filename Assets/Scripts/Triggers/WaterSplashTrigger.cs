using System;
using System.Collections;
using UnityEngine;

namespace Triggers
{
    public class WaterSplashTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleSystem waterSplash;
        [SerializeField] private Transform water;
        private const string CharacterTag = "Character";
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(CharacterTag)) return;
            StartCoroutine(ActivateSplash(other));
        }

        private IEnumerator ActivateSplash(Component other)
        {
            yield return new WaitForSeconds(0.5f);
            var characterTransformPos = other.transform.position;
            waterSplash.transform.position =
                new Vector3(characterTransformPos.x, water.position.y,characterTransformPos.z);
            waterSplash.Play();
        }
    }
}
