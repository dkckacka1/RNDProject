using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Pattern.SingletonClass
{
    class Main : MonoBehaviour
    {

        private void Start()
        {
            var audio = SingletonManager.Instance.GetSingleton<AudioSingleton>();
            Debug.Log(audio.ToString());
        }
    }
}
