using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] TMP_Text playerAmount;

    public RoomInfo info;

    public void Setup(RoomInfo _info)
    {
        info = _info;
        text.text = _info.Name;

        playerAmount.text = _info.PlayerCount + " / " + _info.MaxPlayers;
    }

    public void OnClick()
    {
        if (info.PlayerCount < info.MaxPlayers)
        {
            Launcher.instance.JoinRoom(info);
        }
    }
}
