using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Main.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    Debug.Log("AudioManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static AudioManager instance;

        public float musicVolume = 100f;
        public float soundVolume = 100f;

        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource soundSource;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void PlayMusic(string musicName)
        {

        }

        public void PlaySound(string soundName)
        {

        }

        public void SoundOneShot(string soundName)
        {
        }

        public void ChangeMusicVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            musicVolume = value;
            musicSource.volume = musicVolume / 100;
        }

        public void ChangeSoundVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            soundVolume = value;
            soundSource.volume = soundVolume / 100;
        }
    }
}