using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;
using UnityEngine.Events;

public class DataBase : MonoBehaviour
{

    [SerializeField] InputField keyInputField;
    [SerializeField] InputField valueInputField;

    [SerializeField] Text resultText;

    public FirebaseAuth Auth => FirebaseManager.Instance.firebaseAuth;
    public DatabaseReference databaseRootReference => FirebaseManager.Instance.firebaseDatabase.RootReference;
    public Queue<UnityAction> messageQueue => FirebaseManager.Instance.messageQueue;

    public void WriteDataBtn()
    {
        if (keyInputField.text == string.Empty || valueInputField.text == string.Empty) return;

        // 데이터베이스 루트에 접속합니다.
        databaseRootReference.Child("User").
            Child(Auth.CurrentUser.UserId).
                Child(keyInputField.text).
                    SetValueAsync(valueInputField.text).ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                        {
                            messageQueue.Enqueue(() => { resultText.text = "데이터 쓰기 성공"; });
                        }
                        else
                        {
                            messageQueue.Enqueue(() => { resultText.text = "데이터 쓰기 실패"; });
                        }
                    });
    }
    public void ReadDataBtn()
    {
        if (keyInputField.text == string.Empty) return;

        databaseRootReference.Child("User").
            Child(Auth.CurrentUser.UserId).
                GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsCompleted)
                    {
                        // 데이터를 받아오면 스냅샷 형태로 받아옵니다.
                        DataSnapshot dataSnapshot = task.Result;
                        messageQueue.Enqueue(() => 
                        {
                            string result = "";
                            // IEnumerable 반복자가 있어서 foreach문으로 가능
                            foreach (DataSnapshot data in dataSnapshot.Children)
                            {
                                result += $"{data.Key} : {data.Value}\n";
                            }
                            resultText.text = result;
                        });
                    }
                    else
                    {
                        messageQueue.Enqueue(() => { resultText.text = "데이터 읽기 실패"; });
                    }
                });
    }
}
