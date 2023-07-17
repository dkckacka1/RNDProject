using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.Events;

public class CreateUser : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;
    public InputField nickNameField;

    public Text resultText;

    public FirebaseAuth Auth => FirebaseManager.Instance.firebaseAuth;
    public Queue<UnityAction> messageQueue => FirebaseManager.Instance.messageQueue;



    public void CreateUserBtn()
    {
        Auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(task =>
            // 이메일, 패스워드를 통한 계정 생성
        {
            if (task.IsCanceled)
            {
                messageQueue.Enqueue(FaildSignIn);
            }
            else if (task.IsFaulted)
            {
                messageQueue.Enqueue(FaildSignIn);
                Debug.Log(task.Exception.ToString());
            }
            else
                // 작업 성공 시 성공 메시지 출력
            {
                var newProfile = new UserProfile();
                newProfile.DisplayName = nickNameField.text;
                task.Result.User.UpdateUserProfileAsync(newProfile);
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

}
