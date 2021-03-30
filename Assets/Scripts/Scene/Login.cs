using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField email;
    public InputField password;

    public Button loginBtn;

    private AuthManager auth;

    private UserData user;

    void Start()
    {
        auth = AuthManager.Instance;
        user = UserData.Instance;

        user.GameState = GameState.Login;

        loginBtn.onClick.AddListener(DoLogin);
    }

    void DoLogin()
    {
        Debug.Log("Login süreci başlatıldı...");

        var _email = email.text;
        var _password = password.text;

        auth.Login(_email, _password);
    }
}
