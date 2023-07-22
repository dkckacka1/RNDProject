using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FirebaseManager : MonoBehaviour
{

    public FirebaseApp firebaseApp;
    public FirebaseAuth firebaseAuth;
    public FirebaseUser firebaseUser;
    public FirebaseDatabase firebaseDatabase;
    public FirebaseStorage firebaseStorage;
    public Queue<UnityAction> messageQueue = new Queue<UnityAction>();    // �޽��� ť

    public static FirebaseManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        CheckVaildFirebase();
    }

    private void Update()
    {
        // ������Ʈ ���� ť üũ
        if (messageQueue.Count > 0)
        {
            messageQueue.Dequeue().Invoke();
        }
    }

    public void CheckVaildFirebase()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // ���̾�̽� ����� ���� ����Ʈ���� ����
                firebaseApp = FirebaseApp.DefaultInstance;
                // ���̾�̽� ������ ����ϱ� ���� ����Ʈ���� ����
                firebaseAuth = FirebaseAuth.DefaultInstance;
                // ���̾�̽� �����ͺ��̽��� ����ϱ� ���� ����Ʈ���� ����
                firebaseDatabase = FirebaseDatabase.DefaultInstance;
                // ���̾�̽� ���丮���� ����ϱ� ���� ����Ʈ���� ����
                firebaseStorage = FirebaseStorage.DefaultInstance;

                firebaseUser = (firebaseAuth.CurrentUser != null) ? firebaseAuth.CurrentUser : null;

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
