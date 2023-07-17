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
            // �̸���, �н����带 ���� ���� ����
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
                // �۾� ���� �� ���� �޽��� ���
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
        resultText.text = "���� ���� ����";
    }

    public void SuccessSignIn()
    {
        resultText.text = "���� ���� ����";
    }

}
