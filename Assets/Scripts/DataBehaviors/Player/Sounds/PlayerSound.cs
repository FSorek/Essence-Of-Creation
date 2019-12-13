using System;
using System.Collections;
using Data.Data_Types;
using Data.Player;
using DataBehaviors.Player;
using DataBehaviors.Player.States;
using Monobehaviors.BuildSpot;
using Monobehaviors.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerSound : MonoBehaviour
    {
        [SerializeField]private PlayerBuildData buildData;
        public PlayerSoundEffects SoundEffects;
        public AudioSource Source;
        public float Fade = 4f;
        private PlayerStateMachine playerStateMachine;

        private void Awake()
        {
            playerStateMachine = GetComponent<PlayerStateMachine>();
            playerStateMachine.OnStateEntered += PlayerStateMachineOnStateEntered;
            playerStateMachine.OnStateExit += PlayerStateMachineOnStateExit;
        }

        private void PlayerStateMachineOnStateExit(PlayerStates state)
        {
            if(state != PlayerStates.FORGING) return;
                StopSound();
        }

        private void PlayerStateMachineOnStateEntered(PlayerStates state)
        {
            if(state != PlayerStates.FORGING || buildData.CurrentEssence == null) return;
            ResetSound();
            Source.PlayOneShot(SoundEffects.SummonFireStart);
            Source.Play();
            Invoke(nameof(StopSound), buildData.BuildTime);
        }

        private void StopSound()
        {
            StartCoroutine(FadeOut(Source, Fade));
        }

        private void ResetSound()
        {
            StopAllCoroutines();
            CancelInvoke();
            Source.volume = 1f;
            Source.clip = SoundEffects.Summoning;
            Source.loop = true;
        }

        private IEnumerator FadeOut (AudioSource audioSource, float fadeTime) {
            float startVolume = audioSource.volume;
 
            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
 
                yield return null;
            }
 
            audioSource.Stop ();
            audioSource.volume = startVolume;
        }
    }
}