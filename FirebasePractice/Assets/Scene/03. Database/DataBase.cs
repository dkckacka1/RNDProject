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

        // �����ͺ��̽� ��Ʈ�� �����մϴ�.
        databaseRootReference.Child("User").
            Child(Auth.CurrentUser.UserId).
                Child(keyInputField.text).
                    SetValueAsync(valueInputField.text).ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                        {
                            messageQueue.Enqueue(() => { resultText.text = "������ ���� ����"; });
                        }
                        else
                        {
                            messageQueue.Enqueue(() => { resultText.text = "������ ���� ����"; });
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
                        // �����͸� �޾ƿ��� ������ ���·� �޾ƿɴϴ�.
                        DataSnapshot dataSnapshot = task.Result;
                        messageQueue.Enqueue(() => 
                        {
                            string result = "";
                            // IEnumerable �ݺ��ڰ� �־ foreach������ ����
                            foreach (DataSnapshot data in dataSnapshot.Children)
                            {
                                result += $"{data.Key} : {data.Value}\n";
                            }
                            resultText.text = result;
                        });
                    }
                    else
                    {
                        messageQueue.Enqueue(() => { resultText.text = "������ �б� ����"; });
                    }
                });
    }
}
