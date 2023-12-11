using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Unity.Mathematics;


public class Weapon : MonoBehaviour
{

    public int damage;

    public GunBullet bullet;

    public float fireRate;


    float nextFire;

    bool isLocalPlayer;


    void Start()
    {
        isLocalPlayer= GetComponentInParent<Player>().isLocalPlayer;
    }

    private void Update()
    {
        if (isLocalPlayer)
        {

            if (nextFire > 0)
            {
                nextFire -= Time.deltaTime;
            }
            if (Input.GetButton("Fire1") && nextFire <= 0)
            {


                nextFire = 1 / fireRate;

                Fire();

            }
        }

    }

    private void Fire()
    {

        GunBullet _bullet = PhotonNetwork.Instantiate(bullet.name, transform.position, quaternion.identity).GetComponent<GunBullet>();
        _bullet.direction = transform.forward;
        _bullet.damage = damage;

        /*
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
        */
    }
}
