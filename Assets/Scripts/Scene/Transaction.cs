using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transaction : MonoBehaviour
{
    private DBManager DB;
    private UserData user;
    private RoomData room;

    public Text playerAReadyText;
    public Text playerBReadyText;

    public Text notice;

    public GameObject inviteObject;
    
    void Start()
    {
        DB = DBManager.Instance;
        room = RoomData.Instance;
        user = UserData.Instance;

        user.GameState = GameState.Transaction;

        DB.SetReady();
    }

}
