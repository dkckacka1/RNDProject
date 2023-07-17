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
            // 유저를 지금 접속한 파이어베이스 유저로 교체합니다.
            user = result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void LogoutBtn()
    {
        // 인증된 계정를 로그아웃합니다.
        auth.SignOut();
        resultText.text = "로그아웃 성공";
    }

    private void SuccessLogin()
    {
        resultText.text = "로그인 성공";
    }

    private void FaildLogin()
    {
        resultText.text = "로그인 실패";
    }

    // 현재 접속중인 계정이 변경되었을 때 호출할 이벤트
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
