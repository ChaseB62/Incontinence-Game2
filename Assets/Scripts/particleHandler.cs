using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleHandler : MonoBehaviour
{
    public Transform player;

    public GameObject jumpParticle;

    public GameObject deathParticle;

    public void Jump(){
        Instantiate(jumpParticle, player.transform.position, player.transform.rotation);
    }

    public void Die(){
        Instantiate(deathParticle, player.transform.position, player.transform.rotation);
    }
}
