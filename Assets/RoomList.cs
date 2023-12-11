using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class RoomList : MonoBehaviourPunCallbacks
{

    public GameObject roomUIItemPrefab;

    public Transform roomListParent;
    List<RoomInfo> cachedRoomList = new List<RoomInfo>();
    // Start is called before the first frame update



    public void ChangeRoomToCreateName(string _name)
    {
        EventManager.ChangeCurrentRoomName(_name);
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



}
