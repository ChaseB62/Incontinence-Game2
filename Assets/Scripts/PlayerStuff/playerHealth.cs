using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class playerHealth : MonoBehaviour
{
    public Health healthScript;

    public float healthPlayer = 100f;

    public Slider healthSlider;

    public PlayerMovement playerMovement;

    public Rigidbody2D rb;
    
    public bool isDead = false;

    public float deathFlingForce = 1000f;
    public float deathRotateForce = 100f;

    private float maxFOV;
    private float minFOV;

    public particleHandler particleHandler;
    public GoToPlayer goToPlayer;

    private void Start() {
        maxFOV = goToPlayer.maxFOV;
        minFOV = goToPlayer.minFOV;
    }
    

    public void Update()
    {
        PhotonView photonView = PhotonView.Get(this);

        healthPlayer = healthScript.startHealth;
        healthSlider.value = healthPlayer;
        if(healthPlayer <= 0){
            Die();
        } else if(healthPlayer > 0){
            Revive();
        }
    }

    [PunRPC]
    public void Die()
    {
        playerMovement.enabled = false;
        rb.freezeRotation = false;
        if(isDead == false)
        {
            goToPlayer.minFOV = 40;
            goToPlayer.maxFOV = 85;
            goToPlayer.lerpSpeed = 10f;
            rb.AddForce(Random.insideUnitCircle * Random.Range(deathFlingForce, deathFlingForce + (deathFlingForce / 4)), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range((deathRotateForce * -1), deathRotateForce), ForceMode2D.Impulse);
            particleHandler.Die();
        }
        isDead = true;
    }
    [PunRPC]
    public void Revive(){
        playerMovement.enabled = true;
        isDead = false;
        goToPlayer.minFOV = minFOV;
        goToPlayer.maxFOV = maxFOV;
        goToPlayer.lerpSpeed = 10f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.freezeRotation = true;
    }
}
