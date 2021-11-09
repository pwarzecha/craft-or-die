using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Menu : MonoBehaviourPunCallbacks
{
    /*private bool isConnecting = false;
    private const string GameVersion = "0.1";
    private const int MaxPlayersPerRoom = 20;
    */

    void Start()
    {
        string nickname = PlayerPrefs.GetString("USERNAME");
        ConnectToPhoton(nickname);
    }

    /*private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void SearchForGame()
    {
        isConnecting = true;
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else {
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }*/

    public void TestGame(){
        SceneController.LoadScene("Game");
    }
    private void ConnectToPhoton(string nickName) {
        Debug.Log($"Connected to Photon as {nickName}");
        PhotonNetwork.AuthValues = new AuthenticationValues(nickName);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void CreatePhotonRoom(string roomName)
    {
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom(roomName, ro, TypedLobby.Default);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the Photon Master Server");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Succesfully joined to a Photon Lobby");
        CreatePhotonRoom("TestLobby1");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Succesfully created  a Photon Room named {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Succesfully joined to a Photon Room named {PhotonNetwork.CurrentRoom.Name}");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("You have left a Photon Room");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("You failed to join a Photon Room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"Another player has joined the room {newPlayer.UserId}");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player has left the room {otherPlayer.UserId}");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log($"New Master Client is {newMasterClient.UserId}");
    }
    /*public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log($"Disconnected due to: {cause} ");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No clients have made any rooms");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = MaxPlayersPerRoom });
    }

    public override void OnJoinedRoom() 
    {
        Debug.Log("Client succesfully joined room");
        PhotonNetwork.LoadLevel("Game");
    }*/




}
