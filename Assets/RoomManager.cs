using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Photon.Pun.UtilityScripts;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;


    public GameObject player;


    public GameObject roomCam;

    string nickname = "unnamed";

    public GameObject nameUI;

    public GameObject connectingUI;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    private void Awake()
    {
        instance = this;
    }

    public void ChangeNickName(string _name)
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
    void Start()
    {

    }


    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();

    }


    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);


    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("we are connected and in a room");

        roomCam.SetActive(false);
        SpawnPlayer();

    }


    public void SpawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, Vector3.zero, quaternion.identity);

        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;

        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;


    }
}
