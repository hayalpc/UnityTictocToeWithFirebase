
using UnityEngine;

public class BoardData : Singleton<BoardData>
{
    public string LastPlayed { get; set; }

    private string _s1;
    [SerializeField]
    public string S1
    {
        get { return _s1; }
        set
        {
            if (value != S1 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s1").GetComponent<GameController>().X, GameObject.Find("s1").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s1").GetComponent<GameController>().O, GameObject.Find("s1").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s1").GetComponent<GameController>().status = true;
            }
            _s1 = value;
        }
    }
    private string _s2;
    [SerializeField]
    public string S2
    {
        get { return _s2; }
        set
        {
            if (value != S2 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s2").GetComponent<GameController>().X, GameObject.Find("s2").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s2").GetComponent<GameController>().O, GameObject.Find("s2").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s2").GetComponent<GameController>().status = true;
            }
            _s2 = value;
        }
    }
    private string _s3;
    [SerializeField]
    public string S3
    {
        get { return _s3; }
        set
        {
            if (value != S3 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s3").GetComponent<GameController>().X, GameObject.Find("s3").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s3").GetComponent<GameController>().O, GameObject.Find("s3").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s3").GetComponent<GameController>().status = true;
            }
            _s3 = value;
        }
    }
    private string _s4;
    [SerializeField]
    public string S4
    {
        get { return _s4; }
        set
        {
            if (value != S4 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s4").GetComponent<GameController>().X, GameObject.Find("s4").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s4").GetComponent<GameController>().O, GameObject.Find("s4").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s4").GetComponent<GameController>().status = true;
            }
            _s4 = value;
        }
    }
    private string _s5;
    [SerializeField]
    public string S5
    {
        get { return _s5; }
        set
        {
            if (value != S5 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s5").GetComponent<GameController>().X, GameObject.Find("s5").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s5").GetComponent<GameController>().O, GameObject.Find("s5").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s5").GetComponent<GameController>().status = true;
            }
            _s5 = value;
        }
    }
    private string _s6;
    [SerializeField]
    public string S6
    {
        get { return _s6; }
        set
        {
            if (value != S6 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s6").GetComponent<GameController>().X, GameObject.Find("s6").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s6").GetComponent<GameController>().O, GameObject.Find("s6").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s6").GetComponent<GameController>().status = true;
            }
            _s6 = value;
        }
    }
    private string _s7;
    [SerializeField]
    public string S7
    {
        get { return _s7; }
        set
        {
            if (value != S7 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s7").GetComponent<GameController>().X, GameObject.Find("s7").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s7").GetComponent<GameController>().O, GameObject.Find("s7").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s7").GetComponent<GameController>().status = true;
            }
            _s7 = value;
        }
    }
    private string _s8;
    [SerializeField]
    public string S8
    {
        get { return _s8; }
        set
        {
            if (value != S8 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s8").GetComponent<GameController>().X, GameObject.Find("s8").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s8").GetComponent<GameController>().O, GameObject.Find("s8").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s8").GetComponent<GameController>().status = true;
            }
            _s8 = value;
        }
    }
    private string _s9;
    [SerializeField]
    public string S9
    {
        get { return _s9; }
        set
        {
            if (value != S9 && UserData.Instance.GameState == GameState.Gameplay)
            {
                if (value == "X")
                {
                    Instantiate(GameObject.Find("s9").GetComponent<GameController>().X, GameObject.Find("s9").transform);
                    RoomData.Instance.Turn = "PlayerB";
                    LastPlayed = "X";
                }
                if (value == "O")
                {
                    Instantiate(GameObject.Find("s9").GetComponent<GameController>().O, GameObject.Find("s9").transform);
                    RoomData.Instance.Turn = "PlayerA";
                    LastPlayed = "X";
                }
                GameObject.Find("s9").GetComponent<GameController>().status = true;
            }
            _s9 = value;
        }
    }

}
