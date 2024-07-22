using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace CardGame.Managers
{
    public class SoundManager
    {
        
        private Dictionary<string, SoundEffect> soundEffects;
        private Dictionary<string, SoundEffectInstance> soundEffectInstances;
        private float masterVolume;
        private float musicVolume;
        private float sfxVolume;

        public SoundManager()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            soundEffectInstances = new Dictionary<string, SoundEffectInstance>();
            masterVolume = 1.0f;
            musicVolume = 1.0f;
            sfxVolume = 1.0f;
        }

        public void AddSoundEffect(string soundName, SoundEffect soundEffect)
        {
            soundEffects[soundName] = soundEffect;
        }

        public void PlaySound(string soundName, bool isLooped = false, float volume=1f)
        {
            if (soundEffects.ContainsKey(soundName))
            {
                SoundEffectInstance instance = soundEffects[soundName].CreateInstance();
                instance.IsLooped = isLooped;
                instance.Volume = sfxVolume * masterVolume* volume;
                instance.Play();
                soundEffectInstances[soundName] = instance;
            }
        }

        public void StopSound(string soundName)
        {
            if (soundEffectInstances.ContainsKey(soundName))
            {
                soundEffectInstances[soundName].Stop();
                soundEffectInstances.Remove(soundName);
            }
        }

        public void SetMasterVolume(float volume)
        {
            masterVolume = MathHelper.Clamp(volume, 0f, 1f);
            UpdateVolumes();
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = MathHelper.Clamp(volume, 0f, 1f);
            MediaPlayer.Volume = musicVolume * masterVolume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = MathHelper.Clamp(volume, 0f, 1f);
            UpdateVolumes();
        }

        private void UpdateVolumes()
        {
            foreach (var instance in soundEffectInstances.Values)
            {
                instance.Volume = sfxVolume * masterVolume;
            }
            MediaPlayer.Volume = musicVolume * masterVolume;
        }

        public void PlayMusic(Song song, bool isRepeating = true)
        {
            MediaPlayer.IsRepeating = isRepeating;
            MediaPlayer.Volume = musicVolume * masterVolume;
            MediaPlayer.Play(song);
        }

        public void StopMusic()
        {
            MediaPlayer.Stop();
        }
    }

}
