using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("----- Audio Sources -----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource avtiveInventorySFXSource;
    [SerializeField] private AudioSource combatSFXSource;
    [SerializeField] private AudioSource pickUpSFXSource;

    [Header("----- Audio Clips -----")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip changeWeaponSound;
    [SerializeField] private AudioClip flashAttackSound;
    [SerializeField] private AudioClip arrowAttackSound;
    [SerializeField] private AudioClip arrowImpactSound;
    [SerializeField] private AudioClip laserAttackSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioClip gameOverSound;

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }

    public void PickupSFX()
    {
        pickUpSFXSource.clip = pickupSound;
        pickUpSFXSource.Play();
    }

    public void ChangeWeaponSFX()
    {
        avtiveInventorySFXSource.clip = changeWeaponSound;
        avtiveInventorySFXSource.Play();
    }

    public void FlashAttackSFX()
    {
        combatSFXSource.clip = flashAttackSound;
        combatSFXSource.Play();
    }

    public void ArrowAttackSFX()
    {
        combatSFXSource.clip = arrowAttackSound;
        combatSFXSource.Play();
    }

    public void ArrowImpactSFX()
    {
        combatSFXSource.clip = arrowImpactSound;
        combatSFXSource.Play();
    }

    public void LaserAttackSFX()
    {
        combatSFXSource.clip = laserAttackSound;
        combatSFXSource.Play();
    }

    public void PlayerHurtSFX()
    {
        combatSFXSource.PlayOneShot(playerHurtSound);
        // combatSFXSource.Play();
    }

    public void GameOverSFX()
    {
        musicSource.PlayOneShot(gameOverSound);
    }
}
