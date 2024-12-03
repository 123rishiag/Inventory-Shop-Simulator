using ServiceLocator.Event;
using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundService
    {
        // Services
        private EventService eventService;

        // Objects
        private SoundScriptableObject soundScriptableObject;
        private AudioSource audioEffects;
        private AudioSource backgroundMusic;

        public SoundService(EventService _eventService, SoundScriptableObject _soundData, AudioSource _soundEffect,
            AudioSource _backgroundMusic)
        {
            eventService = _eventService;
            soundScriptableObject = _soundData;
            audioEffects = _soundEffect;
            backgroundMusic = _backgroundMusic;
            PlayBackgroundMusic(SoundType.BackgroundMusic, true);

            // Adding Event Listeners
            eventService.OnPlaySoundEffectEvent.AddListener(PlaySoundEffect);
        }

        ~SoundService()
        {
            // Removing Event Listeners
            eventService.OnPlaySoundEffectEvent.RemoveListener(PlaySoundEffect);
        }

        private void PlaySoundEffect(SoundType _soundType)
        {
            AudioClip clip = GetSoundClip(_soundType);
            if (clip != null)
            {
                audioEffects.clip = clip;
                audioEffects.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlayBackgroundMusic(SoundType _soundType, bool _loopSound = true)
        {
            AudioClip clip = GetSoundClip(_soundType);
            if (clip != null)
            {
                backgroundMusic.loop = _loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType _soundType)
        {
            Sounds sound = Array.Find(soundScriptableObject.audioList, item => item.soundType == _soundType);
            if (sound.audio != null)
                return sound.audio;
            return null;
        }
    }
}