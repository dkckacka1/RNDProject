using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.SingletonClass
{
    abstract class Singleton 
    {
        public abstract void Init();
    }

    class AudioSingleton : Singleton
    {
        public override void Init()
        {
            Debug.Log("오디오 초기 설정!");
        }

        public override string ToString()
        {
            string str = "오디오 싱글톤";
            return str;
        }

        public AudioSource audio;
    }
}
