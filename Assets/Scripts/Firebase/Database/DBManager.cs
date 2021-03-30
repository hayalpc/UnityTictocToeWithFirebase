using Firebase;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    public AuthManager auth;

    public UserData user;
    public RoomData room;
    public BoardData board;

    public DatabaseReference usersDatabase;
    public DatabaseReference roomsDatabase;
    public DatabaseReference invitesDatabase;
    public DatabaseReference acceptedInvitesDatabase;

    void Start()
    {
        auth = AuthManager.Instance;

        user = UserData.Instance;
        room = RoomData.Instance;
        board = BoardData.Instance;

        //Initialization();
    }


    void Initialization()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Db connection is success");

                usersDatabase = FirebaseDatabase.DefaultInstance.GetReference("Users");
                roomsDatabase = FirebaseDatabase.DefaultInstance.GetReference("Rooms");
                invitesDatabase = FirebaseDatabase.DefaultInstance.GetReference("Invites");
                acceptedInvitesDatabase = FirebaseDatabase.DefaultInstance.GetReference("AcceptedInvites");
            }
            else
            {
                Debug.LogError($"DbConnectionError {dependencyStatus}");
            }
        });
    }

    public void CreateUser()
    {

    }

    public void GetUserInformation()
    {

    }

    public void CreateRoom()
    {

    }

    public void GetRoomList()
    {

    }

    public void SendInvite()
    {

    }

    public void AcceptInvite()
    {

    }

    public void SetResult()
    {

    }

    public void SetReady()
    {

    }

    public void EditScore()
    {

    }

    public void DoAction(string p)
    {
        Dictionary<string, object> action = new Dictionary<string, object>();
        if (room.PlayerId == "PlayerA")
        {
            action[p] = "X";
        }
        if (room.PlayerId == "PlayerA")
        {
            action[p] = "O";
        }
        roomsDatabase.Child(room.RoomId).Child("Board").UpdateChildrenAsync(action);
    }

    public void GetOtherUserInformation()
    {

    }

    public void RemoveAllInvites()
    {

    }

    public void RemoveAllAcceptedInvites()
    {

    }

    public void RemoveRoom()
    {

    }

    public void OpenListenRoom()
    {

    }

    public void ListenRoom()
    {

    }

    public void CloseListenRoom()
    {

    }

    public void OpenListenInvites()
    {

    }

    public void ListenInvites()
    {

    }

    public void CloseListenInvites()
    {

    }

    public void OpenListenAcceptedInvites()
    {

    }

    public void ListenAcceptedInvites()
    {

    }

    public void CloseListenAcceptedInvites()
    {

    }

    public void FinishGame()
    {

    }
}
