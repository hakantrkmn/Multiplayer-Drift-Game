using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Pun.UtilityScripts;


public class Weapon : MonoBehaviour
{
    
    public int damage;


    public float fireRate;


    float nextFire;

    private void Update() {
        if (nextFire > 0 )
            {
                nextFire -= Time.deltaTime;
            }
        if (Input.GetButton("Fire1") && nextFire <=0)
        {

            
            nextFire = 1 / fireRate;

            Fire();

        }
    }

    private void Fire()
    {
        Ray ray = new Ray(transform.position , transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin,ray.direction,out hit,100))
        {
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                PhotonNetwork.LocalPlayer.AddScore(damage);
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage",RpcTarget.All,damage);
            }
        }

    }
}
