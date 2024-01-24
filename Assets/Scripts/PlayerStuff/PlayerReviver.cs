using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerReviver : MonoBehaviour
{
    public playerHealth healthScript;

    public Health healthVariable;

    public float secondsUntilRevive = 5f;

    private Vector3 teleportPosition = new Vector3(0, 0, 0);

    public MultiplayerSetup multiplayerSetup;

    private bool AlreadyDied = false;
    void Update()
    {
        if(healthScript.isDead && !AlreadyDied && multiplayerSetup.IsTheGuy){
            StartCoroutine(Revive());
        }

        if(healthScript.isDead == false){
            AlreadyDied = false;
        }
    }

    IEnumerator Revive()
    {
        AlreadyDied = true;
        Debug.Log("Reviving...");
        yield return new WaitForSeconds(secondsUntilRevive);

        transform.position = teleportPosition;

        healthVariable.startHealth = 100f;

        Debug.Log("Revived.");
    }
}
