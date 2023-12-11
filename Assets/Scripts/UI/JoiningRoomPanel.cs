using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoiningRoomPanel : MonoBehaviour
{

    public CanvasGroup setNickNamePanel;
    public CanvasGroup connectingScreen;


    private void OnEnable()
    {
        EventManager.OpenCreateRoomPanel += OpenCreateRoomPanel;
        EventManager.JoinRoomButtonClicked += OpenConnectingScreen;
    }

    private void OnDisable()
    {
        EventManager.OpenCreateRoomPanel -= OpenCreateRoomPanel;
        EventManager.JoinRoomButtonClicked -= OpenConnectingScreen;

    }

    public void ChangeNickName(string _nickname)
    {
        EventManager.ChangeNickName(_nickname);
    }
    private void OpenConnectingScreen()
    {
        Utility.OpenCloseCanvasGroup(setNickNamePanel, false);
        Utility.OpenCloseCanvasGroup(connectingScreen, true);
    }

    private void OpenCreateRoomPanel()
    {
        Utility.OpenCloseCanvasGroup(setNickNamePanel, true);
        Utility.OpenCloseCanvasGroup(connectingScreen, false);
    }


    public void JoinRoomButtonClicked()
    {
        EventManager.JoinRoomButtonClicked();
    }
}
