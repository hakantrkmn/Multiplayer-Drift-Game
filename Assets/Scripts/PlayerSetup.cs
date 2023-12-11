using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public SportCar car;


    public string nickname;

    public TextMeshProUGUI nicknameText;

    public bool isLocalPlayer;
   

    public void SetNickName(string _name)
    {
        nickname = _name;

        nicknameText.text = nickname;
    }
}
