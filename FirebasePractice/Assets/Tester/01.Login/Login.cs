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

    Queue<UnityAction> messageQueue = new Queue<UnityAction>();    // �޽��� ť
    private void Update()
    {
        // ������Ʈ ���� ť üũ
        if (messageQueue.Count > 0)
        {
            messageQueue.Dequeue().Invoke();
        }
    }

    public void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(task =>
            // �̸���, �н����带 ���� ���� ����
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
                // �۾� ���� �� ���� �޽��� ���
            {
                messageQueue.Enqueue(SuccessSignIn);
            }
        });
    }

    public void FaildSignIn()
    {
        resultText.text = "���� ���� ����";
    }

    public void SuccessSignIn()
    {
        resultText.text = "���� ���� ����";
    }

    public void CheckVaildFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                firebaseApp = FirebaseApp.DefaultInstance;
                // ���̾�̽� ������ ����ϱ� ���� ����
                auth = FirebaseAuth.DefaultInstance;
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
