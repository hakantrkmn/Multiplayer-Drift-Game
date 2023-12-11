using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public Vector3 direction;
    public int damage;

    public float timer;
    void Update()
    {
        transform.position += direction * Time.deltaTime * 30;

        timer+=Time.deltaTime;

        if (timer>2)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.GetComponent<Health>())
        {
            if (!other.transform.gameObject.GetComponent<Player>().isLocalPlayer)
            {
                Debug.Log("hakannn");
            PhotonNetwork.LocalPlayer.AddScore(damage);
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
            
        }
    }
}
