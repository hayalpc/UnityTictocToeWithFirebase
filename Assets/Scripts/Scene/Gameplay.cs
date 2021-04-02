using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    UserData user;
    RoomData room;
    BoardData board;

    DBManager DB;

    public Text noticeText;

    public Text playerAUsername;
    public Text playerBUsername;

    public Text playerAScore;
    public Text playerBScore;


    public Image playerATurn;
    public Image playerBTurn;

    public GameObject X1;
    public GameObject O1;

    public GameObject g;

    void Start()
    {
        user = UserData.Instance;
        room = RoomData.Instance;
        board = BoardData.Instance;
        DB = DBManager.Instance;

        user.GameState = GameState.Gameplay;

        DB.GetOtherUserInformation();

        noticeText.text = "Siz: " + user.Username;

        if (room.PlayerId == "PlayerA")
        {
            playerAUsername.text = user.Username;
            playerAScore.text = user.Score.ToString();
        }
        if (room.PlayerId == "PlayerB")
        {
            playerBUsername.text = user.Username;
            playerBScore.text = user.Score.ToString();
        }
    }

    public void CheckTourConditions()
    {
        if (board.LastPlayed == "X")
        {
            g = X1;
        }
        else if (board.LastPlayed == "O")
        {
            g = O1;
        }

        //oyunu kazanma þartlarý
        if (board.S1.Length > 0 && board.S1 == board.S2 && board.S2 == board.S3)//1 2 3
        {
            Instantiate(g, GameObject.Find("s1").transform);
            Instantiate(g, GameObject.Find("s2").transform);
            Instantiate(g, GameObject.Find("s3").transform);
            if (room.PlayerId == ReturnPlayerId(board.S1))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S4.Length > 0 && board.S4 == board.S5 && board.S5 == board.S6)//4 5 6 
        {
            Instantiate(g, GameObject.Find("s4").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s6").transform);
            if (room.PlayerId == ReturnPlayerId(board.S4))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S7.Length > 0 && board.S7 == board.S8 && board.S8 == board.S9)//7 8 9 
        {
            Instantiate(g, GameObject.Find("s4").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s6").transform);
            if (room.PlayerId == ReturnPlayerId(board.S7))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S1.Length > 0 && board.S1 == board.S4 && board.S4 == board.S7)//1 4 7 
        {
            Instantiate(g, GameObject.Find("s1").transform);
            Instantiate(g, GameObject.Find("s4").transform);
            Instantiate(g, GameObject.Find("s7").transform);
            if (room.PlayerId == ReturnPlayerId(board.S1))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S2.Length > 0 && board.S2 == board.S5 && board.S5 == board.S8)//2 5 8 
        {
            Instantiate(g, GameObject.Find("s2").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s8").transform);
            if (room.PlayerId == ReturnPlayerId(board.S2))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S3.Length > 0 && board.S3 == board.S6 && board.S6 == board.S9)//3 6 9 
        {
            Instantiate(g, GameObject.Find("s3").transform);
            Instantiate(g, GameObject.Find("s6").transform);
            Instantiate(g, GameObject.Find("s9").transform);
            if (room.PlayerId == ReturnPlayerId(board.S3))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S1.Length > 0 && board.S1 == board.S5 && board.S5 == board.S9)//1 5 9 
        {
            Instantiate(g, GameObject.Find("s1").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s9").transform);
            if (room.PlayerId == ReturnPlayerId(board.S1))
            {
                DB.SetResult(true);
            }
        }
        else
        if (board.S3.Length > 0 && board.S3 == board.S5 && board.S5 == board.S7)//3 5 7 
        {
            Instantiate(g, GameObject.Find("s3").transform);
            Instantiate(g, GameObject.Find("s5").transform);
            Instantiate(g, GameObject.Find("s7").transform);
            if (room.PlayerId == ReturnPlayerId(board.S3))
            {
                DB.SetResult(true);
            }
        }

    }

    string ReturnPlayerId(string s)
    {
        return s == "X" ? "PlayerA" : "PlayerB";
    }
    public void ExitButton()
    {
        Debug.Log("Oyundan ayrýlma süreci baþatýldý!");

        DB.SetResult(false);
    }
}
