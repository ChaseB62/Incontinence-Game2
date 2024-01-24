using UnityEngine;
using Photon.Pun;

public class GunController : MonoBehaviour
{
    public MultiplayerSetup multiplayerSetup;
    public Transform playerHand;
    public LayerMask gunLayer;
    private GameObject currentGun;
    private GameObject originalGunOnGround;
    private Rigidbody2D originalRigidbody; // Store the original Rigidbody2D
    public float pickupRadius = 2f;

    public float chuckSpeed = 10f;
    private bool isHoldingGun = false;
    private string lastPickedGunType = ""; // Store the type of the last picked up gun

    private bool isLocalPlayer;

    private Gun gun;

    public PhotonView photonView;

    public void Start()
    {
        if (multiplayerSetup.IsTheGuy)
        {
            isLocalPlayer = true;
            Debug.Log("You are the guy");
        }
        else
        {
            Debug.Log("End your existence");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isLocalPlayer)
        {
            photonView = PhotonView.Get(this);

            if (!isHoldingGun)
            {
                photonView.RPC("PickUpGun", RpcTarget.AllBuffered);
                Debug.Log("Picking UP");
            }
            else
            {
                photonView.RPC("DropGun", RpcTarget.AllBuffered);
                Debug.Log("Throwing in back");
            }
        }

        if(isHoldingGun){
            photonView.RPC("SyncPosition", RpcTarget.All);
        }
    }

    [PunRPC]
    void PickUpGun()
    {
        if (isHoldingGun)
        {
            Debug.Log("Cannot pick up another gun while already holding one.");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerHand.position, pickupRadius, gunLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Grab"))
            {
                Debug.Log("Picking up " + collider.name);

                originalGunOnGround = collider.gameObject;
                gun = originalGunOnGround.GetComponent<Gun>();

                if (isLocalPlayer)
                {
                    Debug.Log("Is local player");
                    gun.enabled = true;
                }
                else
                {
                    Debug.Log("Is not local player");
                }

                originalGunOnGround.transform.parent = playerHand;
                originalGunOnGround.transform.localPosition = Vector3.zero;
                originalGunOnGround.transform.localEulerAngles = Vector3.zero;
                originalGunOnGround.transform.localScale = Vector3.one;

                originalRigidbody = collider.GetComponent<Rigidbody2D>();
                photonView.RPC("ToggleRigidbodyAndCollider", RpcTarget.AllBuffered, false);
                photonView.RPC("AddParent", RpcTarget.AllBuffered);

                isHoldingGun = true;
                break;
            }
        }
    }

    [PunRPC]
    void DropGun()
    {
        if (originalGunOnGround != null)
        {
            Debug.Log("Dropping " + originalGunOnGround.name);

            originalGunOnGround.transform.parent = null;
            originalGunOnGround.transform.localScale = Vector3.one;

            gun = originalGunOnGround.GetComponent<Gun>();
            gun.enabled = false;

            if (originalRigidbody != null)
            {
                originalRigidbody.simulated = true;
            }

            Rigidbody2D gunRigidbody = originalGunOnGround.GetComponent<Rigidbody2D>();
            if (gunRigidbody != null)
            {
                photonView.RPC("ToggleRigidbodyAndCollider", RpcTarget.AllBuffered, true);
                photonView.RPC("ClearParent", RpcTarget.AllBuffered);

                gunRigidbody.AddForce(playerHand.transform.right * chuckSpeed, ForceMode2D.Impulse);

                Debug.Log("Toggle");
            }

            isHoldingGun = false;

            gun = null;
            originalRigidbody = null;
        }
        else
        {
            Debug.Log("Original gun on ground is null. Cannot drop.");
        }
    }

    [PunRPC]
    public void ToggleRigidbodyAndCollider(bool toggleBool)
    {
        Debug.Log("Toggling");
        gun.Toggle(toggleBool);
    }

    [PunRPC]
    public void ClearParent()
    {
        if (gun != null)
        {
            gun.ClearParent();
            Debug.Log("Clearing Parent...");
        }
        else
        {
            Debug.LogError("Gun component is null. Cannot clear parent.");
        }
    }


    [PunRPC]
    public void AddParent()
    {
        if (originalGunOnGround != null)
        {
            originalGunOnGround.transform.parent = playerHand;

            if (originalGunOnGround.transform.parent == playerHand)
            {
                Debug.Log("Parent added");
            }
            else
            {
                Debug.LogError("Parent failed to add :(");
            }
        }
        else
        {
            Debug.LogError("Original gun on ground is null. Cannot add parent.");
        }
    }

    [PunRPC]
    public void SyncPosition(){
        gun.GoToZero();
    }
}
