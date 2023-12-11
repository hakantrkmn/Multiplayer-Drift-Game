using System;
using UnityEngine;


public static class EventManager
{



    #region InputSystem
    public static Func<Vector2> GetInput;
    public static Func<Vector2> GetInputDelta;
    public static Action InputStarted;
    public static Action InputEnded;
    public static Func<bool> IsTouching;
    public static Func<bool> IsPointerOverUI;
    #endregion



    public static Action<string> JoinRoomByNameButtonClicked;

    public static Action JoinRoomButtonClicked;

    public static Action<string> ChangeCurrentRoomName;

    public static Action GameStarted;

    public static Action CreateRoomButtonClicked;
    public static Action OpenCreateRoomPanel;


    public static Action PlayerIsDead;
    public static Action<string> ChangeNickName;

    public static Action<Transform> SetGameCamera;



}