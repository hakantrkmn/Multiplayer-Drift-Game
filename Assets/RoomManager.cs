using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Photon.Pun.UtilityScripts;
using System;
using Sirenix.OdinInspector;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;


    public GameObject player;


    public GameObject roomCam;

    string nickname = "unnamed";

    public string roomNameToJoin = "test";
    public GameObject nameUI;

    public CanvasGroup roomManagerCanvas;

    public GameObject connectingUI;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>

    private void Awake()
    {
        instance = this;
    }


    private void OnEnable()
    {
        EventManager.ChangeJoinRoomName += ChangeJoinRoomName;
        EventManager.JoinRoomButtonClicked += JoinRoomByName;
    }

    private void ChangeJoinRoomName(string _name)
    {
        roomNameToJoin = _name;
    }

    private void OnDisable()
    {
                EventManager.ChangeJoinRoomName -= ChangeJoinRoomName;
        EventManager.JoinRoomButtonClicked -= JoinRoomByName;

    }

    public void JoinRoomByName(string _name)
    {
            roomNameToJoin = _name;
            roomManagerCanvas.alpha=1;
            roomManagerCanvas.blocksRaycasts=true;
            roomManagerCanvas.interactable=true;
    }
    public void ChangeNickName(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

        nameUI.SetActive(false);
        connectingUI.SetActive(true);
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
