using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.Singleton_Monobehaviour_
{
    class SingletonManager : MonoBehaviour
    {
        public AudioSingleton audio;

        private void Awake()
        {
            if (instance != null)
            {
                DestroyImmediate(this.gameObject);
            }

            instance = this;
            GetSingletonComponent();
            DontDestroyOnLoad(this.gameObject);
        }

        private void GetSingletonComponent()
        {
            audio = GetComponentInChildren<AudioSingleton>();
        }

        private static SingletonManager instance;
        public static SingletonManager Instance { get => instance; }

    }
}
