using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("----- Audio Sources -----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource AvtiveInventorySFXSource;
    [SerializeField] private AudioSource CombatSFXSource;
    [SerializeField] private AudioSource PickUpSFXSource;

    [Header("----- Audio Clips -----")]
    [SerializeField] private AudioClip changeWeaponSound;
    [SerializeField] private AudioClip flashAttackSound;
    [SerializeField] private AudioClip arrowAttackSound;
    [SerializeField] private AudioClip arrowImpactSound;
    [SerializeField] private AudioClip laserAttackSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip PlayerHurtSound;

    public void PickupSFX()
    {
        PickUpSFXSource.clip = pickupSound;
        PickUpSFXSource.Play();
    }

    public void ChangeWeaponSFX()
    {
        AvtiveInventorySFXSource.clip = changeWeaponSound;
        AvtiveInventorySFXSource.Play();
    }

    public void FlashAttackSFX()
    {
        CombatSFXSource.clip = flashAttackSound;
        CombatSFXSource.Play();
    }

    public void ArrowAttackSFX()
    {
        CombatSFXSource.clip = arrowAttackSound;
        CombatSFXSource.Play();
    }

    public void ArrowImpactSFX()
    {
        CombatSFXSource.clip = arrowImpactSound;
        CombatSFXSource.Play();
    }

    public void LaserAttackSFX()
    {
        CombatSFXSource.clip = laserAttackSound;
        CombatSFXSource.Play();
    }

    public void PlayerHurtSFX()
    {
        CombatSFXSource.clip = PlayerHurtSound;
        CombatSFXSource.Play();
    }
}
