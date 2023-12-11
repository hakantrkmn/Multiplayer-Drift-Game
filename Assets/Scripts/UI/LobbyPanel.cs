using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPanel : MonoBehaviour
{
    public CanvasGroup joinRoomPanel;
    public CanvasGroup createRoomPanel;



    private void OnEnable()
    {
        EventManager.CreateRoomButtonClicked += OpenCreateRoomPanel;
    }

    private void OnDisable()
    {
        EventManager.CreateRoomButtonClicked -= OpenCreateRoomPanel;

    }
    private void OpenCreateRoomPanel()
    {
        Utility.OpenCloseCanvasGroup(joinRoomPanel,false);
        Utility.OpenCloseCanvasGroup(createRoomPanel,true);
    }
}
