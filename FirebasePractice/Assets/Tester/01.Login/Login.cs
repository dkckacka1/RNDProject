using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.Events;

public class Login : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;

    public Text resultText;

    FirebaseApp firebaseApp;
    FirebaseAuth auth;


    private void Awake()
    {
        CheckVaildFirebase();
    }

    Queue<UnityAction> messageQueue = new Queue<UnityAction>();    // 메시지 큐
    private void Update()
    {
        // 업데이트 마다 큐 체크
        if (messageQueue.Count > 0)
        {
            messageQueue.Dequeue().Invoke();
        }
    }

    public void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(task =>
            // 이메일, 패스워드를 통한 계정 생성
        {
            if (task.IsCanceled)
            {
                messageQueue.Enqueue(FaildSignIn);
            }
            else if (task.IsFaulted)
            {
                messageQueue.Enqueue(FaildSignIn);
            }
            else
                // 작업 성공 시 성공 메시지 출력
            {
                messageQueue.Enqueue(SuccessSignIn);
            }
        });
    }

    public void FaildSignIn()
    {
        resultText.text = "계정 생성 실패";
    }

    public void SuccessSignIn()
    {
        resultText.text = "계정 생성 성공";
    }

    public void CheckVaildFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                firebaseApp = FirebaseApp.DefaultInstance;
                // 파이어베이스 인증을 사용하기 위한 세팅
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("파이어베이스 SDK를 사용가능합니다.");
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
    }
}
