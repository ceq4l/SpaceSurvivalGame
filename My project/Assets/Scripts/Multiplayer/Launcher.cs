using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using System;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField maxPlayersInputField;

    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameBtn;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MainMenuManager.Instance.OpenMenu("start");
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();

        int maxPlayers = 0;

        if (Int32.TryParse(maxPlayersInputField.text, out maxPlayers))
        {
            roomOptions.MaxPlayers = maxPlayers;
        }
        else
        {
            roomOptions.MaxPlayers = 20;
        }

        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
        MainMenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        StartGame();
    }

    public void loadRoomMenu()
    {
        MainMenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(players[i]);
        }

        PhotonNetwork.AutomaticallySyncScene = true;

        startGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameBtn.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MainMenuManager.Instance.OpenMenu("error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MainMenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MainMenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MainMenuManager.Instance.OpenMenu("title");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().Setup(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().Setup(newPlayer);
    }
}