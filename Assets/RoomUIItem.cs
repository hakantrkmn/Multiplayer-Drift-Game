using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomUIItem : MonoBehaviour
{
   
   public TextMeshProUGUI roomNameText;
   public TextMeshProUGUI playerCountText;

   public void SetItem(string _roomName , string _playerCount)
   {
        roomNameText.text = _roomName;
        playerCountText.text = _playerCount;
   }

   public void OnButtonPressed()
   {
        EventManager.JoinRoomButtonClicked(roomNameText.text);
   }
}
