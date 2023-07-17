using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text currentUserText;
    public Text currentUserIDText;
    public Text currentUserDisplayName;

    public FirebaseAuth Auth => FirebaseManager.Instance.firebaseAuth;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Start()
    {
        Debug.Log("Start 호출 : " + this.gameObject.name);
        if (FirebaseManager.Instance != null)
        {
            ShowCurrentUser(Auth);
        }
    }

    public void ShowCurrentUser(FirebaseAuth auth)
    {
        if (auth == null || auth.CurrentUser == null)
        {
            currentUserText.text = "현재 접속 유저 : 없음";
            currentUserIDText.text = "현재 이메일 : -";
            currentUserDisplayName.text = "현재 닉네임 : -";
        }
        else
        {
            currentUserText.text = "현재 접속 유저 : 있음";
            currentUserIDText.text = $"현재 이메일 : {auth.CurrentUser.Email}";
            currentUserDisplayName.text = $"현재 닉네임 : {auth.CurrentUser.DisplayName}";
        }
    }
}
