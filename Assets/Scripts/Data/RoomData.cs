using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoomData : Singleton<RoomData>
{
    public string RoomId { get; set; }
    public string PlayerId { get; set; }

    private string _otherPlayerId { get; set; }
    public string OtherPlayerId
    {
        get
        {
            return _otherPlayerId;
        }
        set
        {
            _otherPlayerId = value;
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
            _result = value;
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
            _playerAReady = value;
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
            _playerBReady = value;
        }
    }
}
