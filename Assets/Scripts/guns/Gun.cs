using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public KeyCode shootKey;
    public GameObject bulletObject;
    public Transform bulletSpawn;
    public float shootCooldown;

    private AudioSource audioSource;
    public AudioClip shootSound;

    private bool canShoot = true;

    private bool canBePickedUp;
    public PhotonView photonView;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKey(shootKey) && canShoot)
        {
            Debug.Log("got key");
            photonView.RPC("Shoot", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Shoot()
    {
        Debug.Log("shot");

        Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);

        audioSource.PlayOneShot(shootSound);

        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        Debug.Log("Start cooldown");
        canShoot = false;

        // Adding a log here to check if the coroutine is reached
        Debug.Log("Coroutine started");

        yield return new WaitForSeconds(shootCooldown);
        
        // Adding a log here to check if WaitForSeconds is working
        Debug.Log("Coroutine finished waiting");

        Debug.Log("end cooldown");
        canShoot = true;
    }

    public void Toggle(bool toggleBool){
        Debug.Log("Eh");
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.simulated = toggleBool;

        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = toggleBool;
    }


}
