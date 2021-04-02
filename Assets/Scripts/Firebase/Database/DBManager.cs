using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        Initialization();
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

                if (auth.auth.CurrentUser != null)
                {
                    auth.AutoLogin(auth.auth.CurrentUser.UserId);
                }
                else
                {
                    SceneManager.LoadScene("LoginScene");
                }
            }
            else
            {
                Debug.LogError($"DbConnectionError {dependencyStatus}");
            }
        });
    }

    public void CreateUser(string username)
    {
        var general = new Dictionary<string, object>();
        general["Username"] = username;

        var progression = new Dictionary<string, object>();
        progression["Score"] = 0;

        usersDatabase.Child(user.UserId).Child("General").UpdateChildrenAsync(general);
        user.Username = username;

        usersDatabase.Child(user.UserId).Child("Progression").UpdateChildrenAsync(progression);
        user.Score = 0;

        Debug.Log("Kullanıcı başarıyla oluşturuldu, login sayfasına yönlendiriliyorsunuz");

        SceneManager.LoadScene("LoginScene");
    }

    public void GetUserInformation()
    {
        usersDatabase.Child(user.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                return;
            }

            var dataSnapshot = task.Result;
            var username = dataSnapshot.Child("General").Child("Username").Value.ToString();
            var score = int.Parse(dataSnapshot.Child("Progression").Child("Score").Value.ToString());

            user.Username = username;
            user.Score = score;

            Debug.Log("Kullanıcı login oldu ve bilgileri alındı. Lobiye yönlendirilecek");

            SceneManager.LoadScene("LobbyScene");
        });
    }

    public void CreateRoom()
    {
        var roomId = roomsDatabase.Push().Key;

        room.RoomId = roomId;

        var boards = new Dictionary<string, object>();
        boards["s1"] = "";
        boards["s2"] = "";
        boards["s3"] = "";
        boards["s4"] = "";
        boards["s5"] = "";
        boards["s6"] = "";
        boards["s7"] = "";
        boards["s8"] = "";
        boards["s9"] = "";

        var roomDetails = new Dictionary<string, object>();

        roomDetails["Name"] = $"{user.Username}'s Room";
        roomDetails["PlayerA"] = user.UserId;
        roomDetails["PlayerB"] = "none";
        roomDetails["Result"] = "none";
        roomDetails["PlayerAReady"] = true;
        roomDetails["PlayerBReady"] = false;
        roomDetails["Board"] = boards;

        roomsDatabase.Child(room.RoomId).UpdateChildrenAsync(roomDetails);

        room.PlayerId = "PlayerA";

        OpenListenRoom();
        OpenListenInvites();

        Debug.Log("Oda kuruldu, diğer oyuncular bekleniyor. Transaction ekranına yönlendiriliyor...");

        SceneManager.LoadScene("TransactionScene");
    }

    public void GetRoomList(Dropdown roomList)
    {
        roomsDatabase.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                return;
            }
            var dataSnapshot = task.Result;

            var menuList = new List<string>();
            int index = 0;

            foreach (var r in dataSnapshot.Children)
            {
                var _roomId = r.Key;
                var _hostId = dataSnapshot.Child(_roomId).Child("PlayerA").Value.ToString();
                var _name = dataSnapshot.Child(_roomId).Child("Name").Value.ToString();

                room.roomList.Add(new Room(_name, _roomId, _hostId));

                menuList.Add(_roomId);
                index++;
            }

            roomList.AddOptions(menuList);
        });
    }

    public void SendInvite(string _roomId)
    {
        var invite = new Dictionary<string, object>();
        invite[user.UserId] = user.UserId;

        invitesDatabase.Child(_roomId).UpdateChildrenAsync(invite);

        OpenListenAcceptedInvites();
    }

    public void AcceptInvite(string otherUserId)
    {
        Debug.Log("otherUserId " + otherUserId);
        Debug.Log("RoomId " + room.RoomId);
        Debug.Log("PlayerId " + room.PlayerId);

        if (room.PlayerId == "PlayerA" && room.RoomId != "")
        {
            var roomDetails = new Dictionary<string, object>();
            roomDetails["PlayerB"] = otherUserId;
            roomDetails["PlayerBReady"] = true;

            roomsDatabase.Child(room.RoomId).UpdateChildrenAsync(roomDetails);

            room.OtherUserId = otherUserId;

            var acceptedInviteDetails = new Dictionary<string, object>();
            acceptedInviteDetails["RoomId"] = room.RoomId;

            acceptedInvitesDatabase.Child(otherUserId).UpdateChildrenAsync(acceptedInviteDetails);

            GetOtherUserInformation();

            Debug.Log("Davet Kabul Edildi, Oyun sahnesine yönlendiriliyor");

        }
    }

    public void SetResult(bool b = false)
    {
        var result = new Dictionary<string, object>();

        if(b == false)
        {
            //oyundan ayrılma senaryosu
            if(room.PlayerId == "PlayerA")
            {
                result["Result"] = "PlayerB";
            }
            if(room.PlayerId == "PlayerB")
            {
                result["Result"] = "PlayerA";
            }
        }else
        {
            //normal kazanma senaryosu
            result["Result"] = room.PlayerId;
        }

        roomsDatabase.Child(room.RoomId).UpdateChildrenAsync(result);
    }

    public void SetReady()
    {
        var ready = new Dictionary<string, object>();
        if (room.PlayerId == "PlayerA")
        {
            ready["PlayerAReady"] = true;
        }
        else if (room.PlayerId == "PlayerB")
        {
            ready["PlayerBReady"] = true;
        }
        roomsDatabase.Child(room.RoomId).UpdateChildrenAsync(ready);
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
        if (room.PlayerId == "PlayerB")
        {
            action[p] = "O";
        }
        roomsDatabase.Child(room.RoomId).Child("Board").UpdateChildrenAsync(action);
    }

    public void GetOtherUserInformation()
    {
        Debug.Log("GetOtherUserInformation çalıştı");

        usersDatabase.Child(room.OtherUserId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            if (task.IsFaulted)
            {
                return;
            }
            Debug.Log("OtherUserInformation yükleniyor");
            try
            {
                var dataSnapshot = task.Result;
                var _username = dataSnapshot.Child("General").Child("Username").Value.ToString();
                var _score = dataSnapshot.Child("Progression").Child("Score").Value.ToString();

                Debug.Log($"username {_username} score {_score}");

                room.OtherUsername = _username;
                room.OtherScore = int.Parse(_score);
                Debug.Log("OtherUserInformation yüklendi");
            }
            catch (Exception exp)
            {
                Debug.LogError("OtherUserInformation Exception");
                Debug.LogError(exp.ToString());
            }

        });
    }

    public void RemoveAllInvites()
    {

    }

    public void RemoveAllAcceptedInvites()
    {

    }

    public void RemoveRoom()
    {
        roomsDatabase.Child(room.RoomId).RemoveValueAsync();
    }

    public void OpenListenRoom()
    {
        roomsDatabase.Child(room.RoomId).ValueChanged += ListenRoom;
    }

    public void ListenRoom(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        var snapshop = args.Snapshot;
        Debug.Log("Room değişiklik algılandı. #1");

        if (room.PlayerId == "PlayerA")
        {
            room.OtherUserId = snapshop.Child("PlayerB").Value.ToString();
        }
        if (room.PlayerId == "PlayerB")
        {
            room.OtherUserId = snapshop.Child("PlayerA").Value.ToString();
        }

        if (user.GameState != GameState.Gameplay && room != null)
        {
            room.PlayerAReady = (bool)snapshop.Child("PlayerAReady").GetValue(true);
            room.PlayerBReady = (bool)snapshop.Child("PlayerBReady").GetValue(true);
        }

        board.S1 = snapshop.Child("Board").Child("s1").Value.ToString();
        board.S2 = snapshop.Child("Board").Child("s2").Value.ToString();
        board.S3 = snapshop.Child("Board").Child("s3").Value.ToString();
        board.S4 = snapshop.Child("Board").Child("s4").Value.ToString();
        board.S5 = snapshop.Child("Board").Child("s5").Value.ToString();
        board.S6 = snapshop.Child("Board").Child("s6").Value.ToString();
        board.S7 = snapshop.Child("Board").Child("s7").Value.ToString();
        board.S8 = snapshop.Child("Board").Child("s8").Value.ToString();
        board.S9 = snapshop.Child("Board").Child("s9").Value.ToString();

        if (user.GameState == GameState.Gameplay)
        {
            room.Result = snapshop.Child("Result").Value.ToString();
            FindObjectOfType<Gameplay>().CheckTourConditions();
        }
    }

    public void CloseListenRoom()
    {
        roomsDatabase.Child(room.RoomId).ValueChanged -= ListenRoom;
    }

    public void OpenListenInvites()
    {
        invitesDatabase.Child(room.RoomId).ValueChanged += ListenInvites;

    }

    public void ListenInvites(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        var snapshop = args.Snapshot;
        Debug.Log("Invites değişiklik algılandı. #2");

        foreach (var invite in args.Snapshot.Children)
        {
            var inviteUserId = invite.Key;
            if (user.GameState == GameState.Transaction)
            {
                var inviteObject = Instantiate(FindObjectOfType<Transaction>().inviteObject, GameObject.Find("Canvas").transform);
                inviteObject.GetComponent<InviteManager>().otherUserId = inviteUserId;
            }
        }
    }

    public void CloseListenInvites()
    {
        invitesDatabase.Child(room.RoomId).ValueChanged -= ListenInvites;
    }

    public void OpenListenAcceptedInvites()
    {
        acceptedInvitesDatabase.Child(user.UserId).ValueChanged += ListenAcceptedInvites;
    }

    public void ListenAcceptedInvites(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        var snapshop = args.Snapshot;
        Debug.Log("AcceptedInvites değişiklik algılandı. #3");

        if (snapshop.HasChild("RoomId"))
        {
            var _roomId = snapshop.Child("RoomId").Value.ToString();

            room.RoomId = _roomId;
            room.PlayerId = "PlayerB";

            if (room.RoomId != "")
            {
                Debug.Log("Eşleşme sağlandı. Transaction sahnesine yönlendiriliyor");

                SceneManager.LoadScene("TransactionScene");
            }
        }
        else
        {
            Debug.LogError("ListenAcceptedInvites RoomId yok");

        }
    }

    public void CloseListenAcceptedInvites()
    {
        acceptedInvitesDatabase.Child(user.UserId).ValueChanged -= ListenAcceptedInvites;
    }

    public void FinishGame()
    {
        CloseListenAcceptedInvites();
        CloseListenInvites();
        CloseListenRoom();

        SceneManager.LoadScene("ResultScene");
    }
}
