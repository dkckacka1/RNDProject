using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class Login : MonoBehaviour
{
    public InputField emailField;
    public InputField passwordField;

    public Text resultText;

    public FirebaseAuth auth => FirebaseManager.Instance.firebaseAuth;
    public FirebaseUser user
    {
        get => FirebaseManager.Instance.firebaseUser;
        set => FirebaseManager.Instance.firebaseUser = value;
    }

    private void Start()
    {
        auth.StateChanged += Auth_StateChanged;
    }

    private void OnDisable()
    {
        auth.StateChanged -= Auth_StateChanged;
    }

    public void LoginBtn()
    {
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                FirebaseManager.Instance.messageQueue.Enqueue(FaildLogin);
                Debug.Log("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                FirebaseManager.Instance.messageQueue.Enqueue(FaildLogin);
                Debug.Log("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

                FirebaseManager.Instance.messageQueue.Enqueue(SuccessLogin);
            Firebase.Auth.AuthResult result = task.Result;
            // ������ ���� ������ ���̾�̽� ������ ��ü�մϴ�.
            user = result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void LogoutBtn()
    {
        // ������ ������ �α׾ƿ��մϴ�.
        auth.SignOut();
        resultText.text = "�α׾ƿ� ����";
    }

    private void SuccessLogin()
    {
        resultText.text = "�α��� ����";
    }

    private void FaildLogin()
    {
        resultText.text = "�α��� ����";
    }

    // ���� �������� ������ ����Ǿ��� �� ȣ���� �̺�Ʈ
    private void Auth_StateChanged(object sender, System.EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
            }
        }
    }
}
