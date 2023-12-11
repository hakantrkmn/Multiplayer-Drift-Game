using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.Mathematics;
using Photon.Pun.UtilityScripts;
using System;
using Sirenix.OdinInspector;
using System.Linq;
using Unity.VisualScripting;
using System.Threading.Tasks;

public class RoomManager : MonoBehaviourPunCallbacks
{


    public GameObject player;



    string nickname = "unnamed";

    public string roomNameToJoin = "test";


    IEnumerator Start()
    {

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        Debug.Log("mastera bağlanıyo");

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("mastera bağlandı");
        PhotonNetwork.JoinLobby();
    }




    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.ChangeNickName += ChangeNickName;
        EventManager.JoinRoomButtonClicked += JoinRoomButtonClicked;
        EventManager.ChangeCurrentRoomName += ChangeCurrentRoomName;
        EventManager.PlayerIsDead += SpawnPlayer;
    }



    private void ChangeCurrentRoomName(string _name)
    {
        roomNameToJoin = _name;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.ChangeNickName -= ChangeNickName;
        EventManager.JoinRoomButtonClicked -= JoinRoomButtonClicked;
        EventManager.ChangeCurrentRoomName -= ChangeCurrentRoomName;
        EventManager.PlayerIsDead -= SpawnPlayer;
    }

    private void JoinRoomButtonClicked()
    {
        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);
        EventManager.GameStarted();

    }


    public void ChangeNickName(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, null, null);

    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("we are connected and in a room");
        EventManager.GameStarted();

        SpawnPlayer();

    }


    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        SetLocalOnlinePlayers();
        
    }

    public void SpawnPlayer()
    {

         GameObject _player = PhotonNetwork.Instantiate(player.name, Vector3.zero, quaternion.identity);
     //   _player.AddComponent<Player>();
        _player.GetComponent<Player>().isLocalPlayer=true;
        _player.GetComponent<Player>().SetUpCar();

        SetLocalOnlinePlayers();

        _player.GetComponent<PhotonView>().RPC("SetNickName", RpcTarget.AllBuffered, nickname);
        
        PhotonNetwork.LocalPlayer.NickName = nickname;

        EventManager.SetGameCamera(_player.transform);

        
    }


    public  void SetLocalOnlinePlayers()
    { 
        var players = FindObjectsOfType<Health>();

        foreach (var item in players)
        {
            if(!item.GetComponent<Player>().isLocalPlayer )
            {
                item.GetComponent<Player>().SetUpCar();
               // item.AddComponent<Player>();
            }
        }
    }
}
