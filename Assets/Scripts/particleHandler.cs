using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class particleHandler : MonoBehaviour
{
    public Transform player;

    public GameObject jumpParticle;

    public GameObject deathParticle;

    public void Jump(){
        Instantiate(jumpParticle, player.transform.position, player.transform.rotation);
    }

    public void Die(){
        GameObject bloodEffect = PhotonNetwork.Instantiate(deathParticle.name, player.transform.position, player.transform.rotation);
        bloodEffect.SetActive(true);
    }
}
