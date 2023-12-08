using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class RoomList : MonoBehaviourPunCallbacks
{

    public GameObject roomUIItemPrefab;

    public CanvasGroup roomListCanvas;
    public Transform roomListParent;
    List<RoomInfo> cachedRoomList = new List<RoomInfo>();
    // Start is called before the first frame update
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


  public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError("Bağlantı Kesildi! Sebep: " + cause);
    }


    public void ChangeRoomToCreateName(string _name)
    {
        EventManager.ChangeJoinRoomName(_name);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("mastera bağlandı");
        PhotonNetwork.JoinLobby();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (cachedRoomList.Count <= 0)
        {
            cachedRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cachedRoomList = newList;
                    }
                }
            }
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform rom in roomListParent)
        {
            Destroy(rom.gameObject);
        }

        foreach (var room in cachedRoomList)
        {
            var rm = Instantiate(roomUIItemPrefab, roomListParent).GetComponent<RoomUIItem>();
            rm.SetItem(room.Name, room.PlayerCount + "/16");
        }

    }

    private void OnEnable()
    {
        EventManager.JoinRoomButtonClicked += JoinRoomByName;
    }

    private void OnDisable()
    {
        EventManager.JoinRoomButtonClicked -= JoinRoomByName;

    }

    public void JoinRoomByName(string _name)
    {
        roomListCanvas.alpha = 0;
        roomListCanvas.blocksRaycasts = false;
        roomListCanvas.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.NetworkClientState);
    }
}
