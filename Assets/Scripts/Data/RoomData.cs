using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RoomData : Singleton<RoomData>
{
    public string RoomId { get; set; }
    public string PlayerId { get; set; }

    private string _otherUserId { get; set; }
    public string OtherUserId
    {
        get
        {
            return _otherUserId;
        }
        set
        {
            _otherUserId = value;
        }
    }
    private string _otherUsername { get; set; }
    public string OtherUsername
    {
        get
        {
            return _otherUsername;
        }
        set
        {
            _otherUsername = value;
            if (UserData.Instance.GameState == GameState.Gameplay)
            {
                if (PlayerId == "PlayerA")
                {
                    FindObjectOfType<Gameplay>().playerBUsername.text = OtherUsername;
                }
                if (PlayerId == "PlayerB")
                {
                    FindObjectOfType<Gameplay>().playerAUsername.text = OtherUsername;
                }
            }
        }
    }
    private int _otherScore { get; set; }
    public int OtherScore
    {
        get
        {
            return _otherScore;
        }
        set
        {
            _otherScore = value;
            if (UserData.Instance.GameState == GameState.Gameplay)
            {
                if (PlayerId == "PlayerA")
                {
                    FindObjectOfType<Gameplay>().playerBScore.text = OtherScore.ToString();
                }
                if (PlayerId == "PlayerB")
                {
                    FindObjectOfType<Gameplay>().playerAScore.text = OtherScore.ToString();
                }
            }
        }
    }

    private string _turn = "PlayerA";
    public string Turn
    {
        get
        {
            return _turn;
        }
        set
        {
            _turn = value;
            if (value == "PlayerA" && UserData.Instance.GameState == GameState.Gameplay)
            {
                FindObjectOfType<Gameplay>().playerATurn.enabled = true;
                FindObjectOfType<Gameplay>().playerBTurn.enabled = false;
            }
            if (value == "PlayerB" && UserData.Instance.GameState == GameState.Gameplay)
            {
                FindObjectOfType<Gameplay>().playerATurn.enabled = false;
                FindObjectOfType<Gameplay>().playerBTurn.enabled = true;
            }
        }
    }

    private string _result;
    public string Result
    {
        get
        {
            return _result;
        }
        set
        {
            if (_result != value)
            {
                _result = value;
                if (value == "PlayerA" && UserData.Instance.GameState == GameState.Gameplay)
                {
                    if (PlayerId == "PlayerA")
                    {
                        Debug.Log("Kazandın!");
                    }
                    else
                    {
                        Debug.Log("Kaybettin!");
                    }
                    DBManager.Instance.FinishGame();
                }
                else if (value == "PlayerB" && UserData.Instance.GameState == GameState.Gameplay)
                {
                    if (PlayerId == "PlayerB")
                    {
                        Debug.Log("Kazandın!");
                    }
                    else
                    {
                        Debug.Log("Kaybettin!");
                    }
                    DBManager.Instance.FinishGame();
                }
            }
        }
    }

    public List<Room> roomList = new List<Room>();

    private bool _playerAReady;
    public bool PlayerAReady
    {
        get
        {
            return _playerAReady;
        }
        set
        {
            if (_playerAReady != value)
            {
                _playerAReady = value;
                if (value == true && UserData.Instance.GameState == GameState.Transaction)
                {
                    FindObjectOfType<Transaction>().playerAReadyText.text = "PlayerA Hazır!";
                    FindObjectOfType<Transaction>().playerAReadyText.color = new Color32(65, 203, 41, 255);
                }
            }
        }
    }

    private bool _playerBReady;
    public bool PlayerBReady
    {
        get
        {
            return _playerBReady;
        }
        set
        {
            if (_playerBReady != value)
            {
                _playerBReady = value;
                if (value == true && UserData.Instance.GameState == GameState.Transaction)
                {
                    FindObjectOfType<Transaction>().playerBReadyText.text = "PlayerB Hazır!";
                    FindObjectOfType<Transaction>().playerBReadyText.color = new Color32(65, 203, 41, 255);
                }
            }
        }
    }
}
