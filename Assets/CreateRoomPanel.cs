using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomPanel : MonoBehaviour
{
    public void ChangeCurrentRoomName(string _name)
    {
        EventManager.ChangeCurrentRoomName(_name);
    }


    public void CreateRoomButtonClicked()
    {
        EventManager.OpenCreateRoomPanel();
    }
}
