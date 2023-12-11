using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SportCar car;

    public string nickname;


    public PlayerCarUI playerUI;
    public Health health;

    public Weapon weapon;

    public bool isLocalPlayer;


    public void SetUpCar()
    {
        if (isLocalPlayer)
        {
            car.enabled = true;

        }
    }

    [PunRPC]
    public void SetNickName(string _name)
    {
        nickname = _name;

        playerUI.nicknameText.text = nickname;
    }
}
