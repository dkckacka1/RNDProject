using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Character;

public class GameManager : MonoBehaviour
{
    // Singletone
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameManager is null");
                return null;
            }

            return instance;
        }
    }

    public UserInfo userInfo = new UserInfo();
    public PlayerStatus stutus;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
