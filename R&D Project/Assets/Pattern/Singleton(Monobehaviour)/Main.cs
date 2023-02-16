using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Pattern.Singleton_Monobehaviour_
{
    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var audio = SingletonManager.Instance.audio;
            Debug.Log(audio.ToString());
        }
    }
}