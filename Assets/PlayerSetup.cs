using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public SportCar car;

    public GameObject camera;

    public string nickname;

    public TextMeshProUGUI nicknameText;
    public void isLocalPlayer()
    {
        car.enabled = true;
        camera.SetActive(true);
    }

[PunRPC]
    public void SetNickName(string _name)
    {
        nickname = _name;

        nicknameText.text = nickname;
    }
}
