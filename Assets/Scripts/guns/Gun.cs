using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    public KeyCode shootKey;
    public GameObject bulletObject;
    public Transform bulletSpawn;
    public float shootCooldown;

    public AudioSource audioSource;
    public AudioClip shootSound;

    private bool canShoot = true;
    private bool canBePickedUp;
    public PhotonView photonView;

    private void Update()
    {
        if (Input.GetKey(shootKey) && canShoot)
        {
            photonView.RPC("Shoot", RpcTarget.All);
        }
    }

    [PunRPC]
    private void Shoot()
    {
        Instantiate(bulletObject, bulletSpawn.position, bulletSpawn.rotation);
        audioSource.PlayOneShot(shootSound);
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    [PunRPC]
    public void Toggle(bool toggleBool)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.simulated = toggleBool;

        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = toggleBool;
    }

    [PunRPC]
    public void ClearParent()
    {
        Debug.Log("Cleared Parent");
        transform.parent = null;
    }

    [PunRPC]
    public void GoToZero(){
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localScale = Vector3.one;
    }
}
