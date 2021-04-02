using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    private AuthManager auth;
    private DBManager DB;
    private UserData user;
    private RoomData room;

    public Dropdown gameListRoom;

    public Button joinRoomBtn;
    public Button createRoomBtn;

    public Text usernameText;
    public Text scoreText;

    void Start()
    {
        auth = AuthManager.Instance;
        DB = DBManager.Instance;
        user = UserData.Instance;
        room = RoomData.Instance;

        user.GameState = GameState.Lobby;

        usernameText.text = user.Username;
        scoreText.text = user.Score.ToString();

        joinRoomBtn.onClick.AddListener(JoinRoom);
        createRoomBtn.onClick.AddListener(CreateRoom);

        DB.GetRoomList(gameListRoom);
    }

    void JoinRoom()
    {
        Debug.Log("Odaya katýlma süreci baþatýldý..");

        var _roomId = gameListRoom.options[gameListRoom.value].text;

        DB.SendInvite(_roomId);
    }

    void CreateRoom()
    {
        Debug.Log("Oda oluþturma süreci baþatýldý..");

        DB.CreateRoom();
    }

    public void RefreshRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void Logout()
    {
        auth.Logout();

        room.RoomId = "";
        room.PlayerId = "";
        room.Turn = "PlayerA";
        room.OtherUserId = "";
        room.OtherUsername = "";
        room.OtherScore = 0;
        room.Result = "";
        room.roomList = new List<Room>();

        room.PlayerAReady = false;
        room.PlayerBReady = false;

        SceneManager.LoadScene("LoginScene");
    }

}
