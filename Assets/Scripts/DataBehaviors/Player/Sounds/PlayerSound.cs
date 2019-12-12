using System.Collections;
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
        private void Awake()
        {
            AttunedPlayerState.OnElementBuildingStarted += AttunedPlayerStateOnElementBuildingStarted;
            AttunedPlayerState.OnElementBuildingInterrupted += AttunedPlayerStateOnElementBuildingStopped;
            AttunedPlayerState.OnElementBuildingFinished += AttunedPlayerStateOnElementBuildingStopped;
        }

        private void AttunedPlayerStateOnElementBuildingStopped(PlayerComponent player)
        {
            StartCoroutine(FadeOut(Source, Fade));
        }

        private void AttunedPlayerStateOnElementBuildingStarted(PlayerComponent player, BuildSpotComponent spot)
        {
            StopAllCoroutines();
            Source.volume = 1f;
            Source.PlayOneShot(SoundEffects.SummonFireStart);
            Source.clip = SoundEffects.Summoning;
            Source.loop = true;
            Source.Play();
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