using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public CanvasGroup joiningRoomPanel;
    public CanvasGroup lobbyPanel;
    public CanvasGroup leaderBoardPanel;


    private void Start()
    {
        Utility.OpenCloseCanvasGroup(joiningRoomPanel, false);
        Utility.OpenCloseCanvasGroup(lobbyPanel, true);
        Utility.OpenCloseCanvasGroup(leaderBoardPanel, false);

    }


    private void OnEnable()
    {
        EventManager.GameStarted += GameStarted;
        EventManager.OpenCreateRoomPanel += OpenCreateRoomPanel;
    }

    private void GameStarted()
    {
        Utility.OpenCloseCanvasGroup(joiningRoomPanel, false);
        Utility.OpenCloseCanvasGroup(lobbyPanel, false);
        Utility.OpenCloseCanvasGroup(leaderBoardPanel, false);
    }

    private void OnDisable()
    {
                EventManager.GameStarted -= GameStarted;
        EventManager.OpenCreateRoomPanel -= OpenCreateRoomPanel;

    }

    private void OpenCreateRoomPanel()
    {
        Utility.OpenCloseCanvasGroup(joiningRoomPanel, true);
        Utility.OpenCloseCanvasGroup(lobbyPanel, false);
        Utility.OpenCloseCanvasGroup(leaderBoardPanel, false);
    }
}
