using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainLobby : MonoBehaviour
{
    public void ReturnLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
