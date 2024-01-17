using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    [Space]
    public Transform SpawnPoint;
    [Space]
    public GameObject roomCam;
    [Space]
    public GameObject nameUI;
    public GameObject connectingUI;

    public string nickname = "unnamed loser";

    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
        
        nameUI.SetActive(false);
        connectingUI.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to server.");

        PhotonNetwork.JoinLobby();
    }    
        
    public override void OnJoinedLobby(){
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("Joined A Room");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        roomCam.SetActive(false);

        Debug.Log("In a Room.");

        SpawnPlayer();

        
    }

    public void SpawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, SpawnPoint.position, Quaternion.identity);
        _player.GetComponent<MultiplayerSetup>().IsLocalPlayer();

        _player.GetComponent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
    }
}

