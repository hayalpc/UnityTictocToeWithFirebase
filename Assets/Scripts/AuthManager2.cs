using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager2 : MonoBehaviour
{
    public FirebaseAuth auth;
    public InputField emailForm;
    public InputField passwordForm;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void Signup()
    {
        var email = emailForm.text;
        var password = passwordForm.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Auth Cancelled!");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Auth Faulted!");
                return;
            }

            var newUser = task.Result;
            Debug.LogFormat("Yeni bir kullanıcı oluşturuldu: {0} {1}", newUser.DisplayName, newUser.UserId);
        });
    }

    public void SignupAnonmous()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("Signup Cancelled!");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.Log("Signup Faulted!");
                return;
            }
            var newUser = task.Result;
            Debug.LogFormat("Kullanıcı başarıyla oluşturuldu: {0} {1}", newUser.DisplayName, newUser.UserId);
        });
    }

    public void DoLogin()
    {
        var email = emailForm.text;
        var password = passwordForm.text;
        if (email.Length > 0 && password.Length > 0)
        {
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
             {
                 if (task.IsCanceled)
                 {
                     Debug.Log("Login Cancelled!");
                     return;
                 }
                 if (task.IsFaulted)
                 {
                     Debug.Log("Login Faulted!");
                     return;
                 }
                 var newUser = task.Result;
                 Debug.LogFormat("Başarıyla giriş yapıldı: {0} {1}", newUser.DisplayName, newUser.UserId);
             });
        }
        else
        {
            Debug.Log("Form alanlarını boş bırakmayınız!");
        }
    }

    public void LoginScene()
    {
        SceneManager.LoadScene("LoginScene");
    }
    public void RegisterScene()
    {
        SceneManager.LoadScene("RegisterScene");
    }

    public void ResetPassword()
    {
        var email = emailForm.text;
        if (email.Length > 0)
        {
            auth.SendPasswordResetEmailAsync(email).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.Log("ResetPassword Cancelled!");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.Log("ResetPassword Faulted!");
                    return;
                }
                Debug.Log("Şifre sıfırlama gönderilmiştir!");
            });
        }
        else
        {
            Debug.Log("Email alanını boş bırakmayınız!");
        }
    }

}
