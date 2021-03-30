using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class DbManager2 : MonoBehaviour
{
    public DatabaseReference usersDatabase;

    void Start()
    {
        Initialization();
    }


    void Initialization()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Debug.Log("Db connection is success");

                usersDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");

                GetUserList();

                var userId = "-MWytmqNEBdrj5rD9HJN";
                usersDatabase.Child(userId).ValueChanged += GetUserDetails;
            }
            else
            {
                Debug.LogError(string.Format("Hata {0}", dependencyStatus));
            }
        });
    }

    void GetUserDetails(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
        }
        else
        {
            Debug.Log("GetUserDetails ValueChanged!");
            var username = args.Snapshot.Child("username").Value.ToString();
            Debug.Log(username);
        }
    }

    void GetUserList()
    {
        //.LimitToFirst(1)

        usersDatabase.OrderByChild("level").EqualTo(5).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("GetUserList Cancelled!");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.Log("GetUserList Faulted!");
                return;
            }
            else if (task.IsCompleted)
            {
                DataSnapshot dataSnapshot = task.Result;
                foreach (DataSnapshot userId in dataSnapshot.Children)
                {
                    var username = dataSnapshot.Child(userId.Key).Child("username").Value.ToString();
                    var level = int.Parse(dataSnapshot.Child(userId.Key).Child("level").Value.ToString());
                    Debug.Log($"{userId.Key} {username} {level}");
                }
            }

        });
    }

    void DeleteUser(string userId)
    {
        usersDatabase.Child(userId).RemoveValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("DeleteUser Cancelled!");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.Log("DeleteUser Faulted!");
                return;
            }
            else if (task.IsCompleted)
            {
                Debug.Log($"{userId} kullanıcısı silindi!");
            }
        });
    }

    void UpdateData(string userId, string username, int level, bool loginStatus)
    {
        Dictionary<string, object> children = new Dictionary<string, object>();
        children["username"] = username;
        children["level"] = level;
        children["loginStatus"] = loginStatus;

        usersDatabase.Child(userId).UpdateChildrenAsync(children);
    }

    void SaveData(string username, int level, bool loginStatus)
    {
        var user = new User(username, level, loginStatus);
        var json = JsonUtility.ToJson(user);

        var userId = usersDatabase.Push().Key;
        usersDatabase.Child(userId).SetRawJsonValueAsync(json);
    }
}
