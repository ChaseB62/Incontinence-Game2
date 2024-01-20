using UnityEngine;
using Photon.Pun;

public class JumpSounds : MonoBehaviour
{

    public AudioSource jumpSource;
    public AudioClip jumpSoundEffect;

    [PunRPC]
    public void JumpSFX(){
        jumpSource.PlayOneShot(jumpSoundEffect);
    }
}
