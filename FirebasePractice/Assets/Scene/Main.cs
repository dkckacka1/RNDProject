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
        Debug.Log("Start ȣ�� : " + this.gameObject.name);
        if (FirebaseManager.Instance != null)
        {
            ShowCurrentUser(Auth);
        }
    }

    public void ShowCurrentUser(FirebaseAuth auth)
    {
        if (auth == null || auth.CurrentUser == null)
        {
            currentUserText.text = "���� ���� ���� : ����";
            currentUserIDText.text = "���� �̸��� : -";
            currentUserDisplayName.text = "���� �г��� : -";
        }
        else
        {
            currentUserText.text = "���� ���� ���� : ����";
            currentUserIDText.text = $"���� �̸��� : {auth.CurrentUser.Email}";
            currentUserDisplayName.text = $"���� �г��� : {auth.CurrentUser.DisplayName}";
        }
    }
}
