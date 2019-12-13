using System;
using System.Collections;
using Data.Data_Types;
using DataBehaviors.Player;
using DataBehaviors.Player.States;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerSound : MonoBehaviour
    {
        public PlayerSoundEffects SoundEffects;
        public AudioSource Source;
        public float Fade = 4f;
        private PlayerComponent player;
        private PlayerStateMachine playerStateMachine;

        private void Awake()
        {
            player = GetComponent<PlayerComponent>();
            playerStateMachine = GetComponent<PlayerStateMachine>();
            player.PlayerInput.OnPrimaryKeyPressed += PlayerInputOnPrimaryKeyPressed;
            player.PlayerInput.OnPrimaryKeyReleased += PlayerInputOnPrimaryKeyReleased;
        }

        private void PlayerInputOnPrimaryKeyReleased()
        {
            StopSound();
        }

        private void StopSound()
        {
            StartCoroutine(FadeOut(Source, Fade));
        }

        private void PlayerInputOnPrimaryKeyPressed()
        {
            if(playerStateMachine.CurrentState != PlayerStates.BUILD) return;
            CancelInvoke();
            StopAllCoroutines();
            Source.volume = 1f;
            Source.PlayOneShot(SoundEffects.SummonFireStart);
            Source.clip = SoundEffects.Summoning;
            Source.loop = true;
            Source.Play();
            Invoke(nameof(StopSound), player.BuildData.BuildTime);
        }

        private IEnumerator FadeOut (AudioSource audioSource, float FadeTime) {
            float startVolume = audioSource.volume;
 
            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
                yield return null;
            }
 
            audioSource.Stop ();
            audioSource.volume = startVolume;
        }
    }
}