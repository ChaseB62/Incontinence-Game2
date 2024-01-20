using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunDisabler : MonoBehaviour
{
    public playerHealth healthScript;

    public MultiplayerSetup multiplayerSetup;

    public PhotonView photonView;
    private void Update() {
        if(healthScript.isDead){
            photonView.RPC("DropGun", RpcTarget.AllBuffered);
        }
    }
}
