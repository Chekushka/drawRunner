using System.Collections.Generic;
using Character;
using MoreMountains.Feedbacks;
using RayFire;
using UnityEngine;

public class WallDemolition : MonoBehaviour
{
    [SerializeField] private MMFeedbacks failFeedback;
    [SerializeField] private List<ParticleSystem> failHitParticles;
    private RayfireRigid _rayfireRigid;
    private CameraShake _cameraShake;

    private void Start()
    {
        _rayfireRigid = GetComponentInChildren<RayfireRigid>();
        _cameraShake = FindObjectOfType<CameraShake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Character")) return;
        var characterRun = other.GetComponent<CharacterRun>();
        if (characterRun.isAbleToDestroyWall)
        {
            _cameraShake.Shake(4f, 0.7f);
            _rayfireRigid.Demolish();
            characterRun.PlayHitParticles();
        }
        else
        {
            foreach (var particle in failHitParticles)
                particle.Play();
            _cameraShake.Shake(2f, 0.5f);
            characterRun.WallFailFall();
            failFeedback.PlayFeedbacks();
            _rayfireRigid.Demolish();
        }
    }
}
