using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
   
    public AudioClip cash;
    public AudioClip coin;
    public AudioClip coo;
    public AudioClip prec;
    public AudioClip pop;
    public AudioClip tap;
    public AudioClip universal02;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
