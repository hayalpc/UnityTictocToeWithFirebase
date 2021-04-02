using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    UserData user;
    RoomData room;
    BoardData board;

    DBManager DB;

    public Text resultText;

    void Start()
    {
        user = UserData.Instance;
        room = RoomData.Instance;
        board = BoardData.Instance;

        DB = DBManager.Instance;

        user.GameState = GameState.Result;

        PrintResult();
        CloseSession();
    }

    void PrintResult()
    {
        if (room.Result == "PlayerA")
        {
            if (room.PlayerId == "PlayerA")
            {
                resultText.text = "KAZANDIN";
                DB.EditScore();
            }
            if (room.PlayerId == "PlayerB")
            {
                resultText.text = "KAYBETTINIZ!";
            }
        }
        else if (room.Result == "PlayerB")
        {
            if (room.PlayerId == "PlayerA")
            {
                resultText.text = "KAYBETTINIZ!";
            }
            if (room.PlayerId == "PlayerB")
            {
                resultText.text = "KAZANDIN";
                DB.EditScore();
            }
        }
    }

    public void CloseSession()
    {
        DB.CloseListenRoom();
        if(room.PlayerId == "PlayerA")
        {
            DB.CloseListenInvites();
            DB.RemoveAllInvites();
        }
        if(room.PlayerId == "PlayerB")
        {
            DB.CloseListenAcceptedInvites();
            DB.RemoveAllAcceptedInvites();
        }

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

        board.LastPlayed = "";
        board.S1 = "";
        board.S2 = "";
        board.S3 = "";
        board.S4 = "";
        board.S5 = "";
        board.S6 = "";
        board.S7 = "";
        board.S8 = "";
        board.S9 = "";

        DB.RemoveRoom();

    }

    public void GoLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
