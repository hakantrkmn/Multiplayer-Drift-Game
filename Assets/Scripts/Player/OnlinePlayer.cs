using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePlayer : Player
{
   public void SetUpCar()
    {
        playerUI = GetComponent<PlayerCarUI>();
    }
}
