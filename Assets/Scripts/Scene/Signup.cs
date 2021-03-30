using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signup : MonoBehaviour
{
    public InputField username;
    public InputField email;
    public InputField password;

    public Button signupBtn;

    private AuthManager auth;

    private UserData user;

    void Start()
    {
        auth = AuthManager.Instance;    
        user = UserData.Instance;

        user.GameState = GameState.Signup;

        signupBtn.onClick.AddListener(DoSignup);
    }

    void DoSignup()
    {
        Debug.Log("Signup süreci başlatıldı...");

        var _username = username.text;
        var _email= email.text;
        var _password = password.text;

        auth.Signup(_username, _email,_password);
    }

}
