using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    //made by chase

    public Transform player;

    public float lerpSpeed = 5f;
    public float Zoom = 10f;

    void Update()
    {
        Vector3 playerLerp = new Vector3(player.transform.position.x, player.transform.position.y, (Zoom * -1)); 
        transform.position = Vector3.Lerp(transform.position, playerLerp, Time.deltaTime * lerpSpeed);
    }
}
