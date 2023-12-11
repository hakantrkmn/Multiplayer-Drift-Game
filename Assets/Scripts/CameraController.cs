using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera menuCam;
    public CinemachineVirtualCamera gameCam;


    private void OnEnable() {
        EventManager.SetGameCamera += SetGameCamera;
        EventManager.GameStarted += GameStarted;
    }

    private void OnDisable() {
         EventManager.SetGameCamera -= SetGameCamera;
        EventManager.GameStarted -= GameStarted;
    }
    private void GameStarted()
    {
        gameCam.Priority = 10;
        menuCam.Priority = 0;
    }

    private void SetGameCamera(Transform _player)
    {
        gameCam.Follow = _player;
        gameCam.LookAt = _player;

    }
}
