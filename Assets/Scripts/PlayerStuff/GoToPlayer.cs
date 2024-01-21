using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPlayer : MonoBehaviour
{
    //made by chase

    public Transform player;

    public float lerpSpeed = 5f;
    public float Zoom = 1f;

    public float maxFOV = 120f;
    public float minFOV = 85f;

    public Camera playerCamera;

    void Update()
    {
        Vector3 playerLerp = new Vector3(player.transform.position.x, player.transform.position.y, -10f); 
        transform.position = Vector3.Lerp(transform.position, playerLerp, Time.deltaTime * lerpSpeed);

        float initialFOV = minFOV;

        float fovLerpSpeed = 2f;

        float normalizedMouseX = (Input.mousePosition.x / Screen.width - 0.5f) * 2f;
        float normalizedMouseY = (Input.mousePosition.y / Screen.height - 0.5f) * 2f;

        float targetFOV = Mathf.Lerp(initialFOV, maxFOV, Mathf.Max(Mathf.Abs(normalizedMouseX), Mathf.Abs(normalizedMouseY)));
        float currentFOV = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

        playerCamera.fieldOfView = currentFOV;

    }
}
