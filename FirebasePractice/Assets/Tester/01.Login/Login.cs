using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class Login : MonoBehaviour
{
    public Text loginResultText;

    public string email;
    public string password;

    FirebaseApp firebaseApp;
    FirebaseAuth auth;
    FirebaseUser user;

    private void Awake()
    {
        CheckVaildFirebase();
    }

    public void CreateUser()
    {
        System.Threading.Tasks.Task tt = auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => 
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });

    }

    public void CheckVaildFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                firebaseApp = FirebaseApp.DefaultInstance;
                auth = FirebaseAuth.GetAuth(firebaseApp);
                Debug.Log("���̾�̽� SDK�� ��밡���մϴ�.");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
}
