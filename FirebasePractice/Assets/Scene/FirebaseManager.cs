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
    public Queue<UnityAction> messageQueue = new Queue<UnityAction>();    // 메시지 큐

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
        // 업데이트 마다 큐 체크
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
                // 파이어베이스 사용을 위한 게이트웨이 설정
                firebaseApp = FirebaseApp.DefaultInstance;
                // 파이어베이스 인증을 사용하기 위한 게이트웨이 설정
                firebaseAuth = FirebaseAuth.DefaultInstance;
                // 파이어베이스 데이터베이스를 사용하기 위한 게이트웨이 설정
                firebaseDatabase = FirebaseDatabase.DefaultInstance;
                // 파이어베이스 스토리지를 사용하기 위한 게이트웨이 설정
                firebaseStorage = FirebaseStorage.DefaultInstance;

                firebaseUser = (firebaseAuth.CurrentUser != null) ? firebaseAuth.CurrentUser : null;

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
