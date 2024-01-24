using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunDisabler : MonoBehaviour
{
    public playerHealth healthScript;

    public MultiplayerSetup multiplayerSetup;

    private bool alreadyDropped = false;

    public PhotonView photonView;
    private void Update() {
        if(healthScript.isDead && !alreadyDropped){
            photonView.RPC("DropGun", RpcTarget.AllBuffered);
            alreadyDropped = true;
        } else {
            alreadyDropped = false;
        }
    }
}
