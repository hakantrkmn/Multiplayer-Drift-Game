using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public Image healthBar;

    bool isLocalPlayer;




    void Start()
    {
               isLocalPlayer= GetComponentInParent<Player>().isLocalPlayer;

    }


[PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        healthBar.fillAmount = ((float)health)/100;

        if (health <=0)
        {
            if (isLocalPlayer)
            {
                EventManager.PlayerIsDead();
                
            }
            Destroy(gameObject);
        }
    }
}
