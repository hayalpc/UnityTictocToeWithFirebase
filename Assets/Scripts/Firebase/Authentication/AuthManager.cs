using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Signup(string username,string email,string password)
    {

    }

    public void Login(string email, string password)
    {

    }

    public void AutoLogin()
    {

    }

   
}
