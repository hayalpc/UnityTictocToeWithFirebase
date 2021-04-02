using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if(room.PlayerId == "PlayerB")
        {
            DB.OpenListenRoom();
            DB.CloseListenAcceptedInvites();

            playerAReadyText.enabled = false;
            playerBReadyText.enabled = false;
        }
        StartCoroutine(CheckReadyStatus());

    }

    IEnumerator CheckReadyStatus()
    {
        yield return new WaitUntil(() => room.PlayerAReady == true && room.PlayerBReady == true);

        notice.text = "Oyun yükleniyor... Lütfen bekleyiniz";

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("GameplayScene");
    }

}
