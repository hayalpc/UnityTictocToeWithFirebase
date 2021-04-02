using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InviteManager : MonoBehaviour
{
    DBManager DB;

    public Text inviteMessage;

    public Button acceptInvite;
    public Button rejectInvite;

    public string otherUserId;

    void Start()
    {
        DB = DBManager.Instance;

        acceptInvite.onClick.AddListener(AcceptInvite);
        rejectInvite.onClick.AddListener(RejectInvite);
    }

    void AcceptInvite()
    {
        DB.AcceptInvite(otherUserId);
    }

    void RejectInvite()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
    }

}
