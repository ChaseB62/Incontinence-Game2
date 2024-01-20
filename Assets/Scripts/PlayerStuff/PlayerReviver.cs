using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReviver : MonoBehaviour
{
    public playerHealth healthScript;

    public Health healthVariable;

    public float secondsUntilRevive = 5f;

    private Vector3 teleportPosition = new Vector3(0, 0, 0);

    private bool AlreadyDied = false;
    void Update()
    {
        if(healthScript.isDead && !AlreadyDied){
            StartCoroutine(Revive());
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
        AlreadyDied = false;
    }
}
