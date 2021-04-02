using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AuthManager : Singleton<AuthManager>
{

    public FirebaseAuth auth;
    public DBManager DB;
    public UserData user;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DB = DBManager.Instance;
        user = UserData.Instance;
    }

    public void Signup(string username, string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                return;
            }
            var newUser = task.Result;
            DB.user.UserId = newUser.UserId;
            DB.CreateUser(username);
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                return;
            }
            var newUser = task.Result;
            DB.user.UserId = newUser.UserId;
            DB.GetUserInformation();
        });
    }

    public void Logout()
    {
        auth.SignOut();
    }

    public void AutoLogin(string userId)
    {
        Debug.Log("Autologin süreci başlatıldı");

        var newUser = auth.CurrentUser;

        user.UserId = userId;
        DB.GetUserInformation();

        SceneManager.LoadScene("LobbyScene");
    }

}
