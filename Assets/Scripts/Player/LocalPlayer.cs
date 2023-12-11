using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : Player
{
    public void SetUpCar()
    {
        car = GetComponent<SportCar>();
        playerUI = GetComponent<PlayerCarUI>();
        car.enabled = true;
    }
}
