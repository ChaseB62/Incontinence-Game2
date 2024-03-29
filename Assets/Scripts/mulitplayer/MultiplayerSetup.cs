using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class MultiplayerSetup : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GunController gunController;
    public playerHealth playerHealthScript;
    public LookAtMouse lookAtMouse;
    public GameObject camera;


    public string nickname;

    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;


    public TextMeshProUGUI nicknameText;

    public bool IsTheGuy = false;

    public void IsLocalPlayer()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        IsTheGuy = true;
        playerMovement.enabled = true;
        playerHealthScript.enabled = true;
        gunController.enabled = true;
        lookAtMouse.enabled = true;
        camera.SetActive(true);
        SetObjectsActive(objectsToEnable, true);
        SetObjectsActive(objectsToDisable, false);
    }

    public void Update(){
        if(!IsTheGuy){
            playerMovement.enabled = false;
        }
    }

    void SetObjectsActive(GameObject[] objects, bool isActive)
    {
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(isActive);
            }
        }
    }

    [PunRPC]
    public void SetNickname(string _name)
    {
        nickname = _name;

        nicknameText.text = nickname;
    }
}
