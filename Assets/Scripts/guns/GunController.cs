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
    public bool isHoldingGun = false;
    private string lastPickedGunType = ""; // Store the type of the last picked up gun

    private bool isLocalPlayer;

    private Gun gun;

    public PhotonView photonView;

    public void Start(){
        if(multiplayerSetup.IsTheGuy){
            isLocalPlayer = true;
            Debug.Log("you ARE HIM");
        } else {
            Debug.Log("end your existence");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
                Debug.Log("THroweing in back");
            }
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
                //aiden wrote the majority of this but since hes stupid and dumb he had to make a new tag for each individual gun so i got rid of that and shunned him.
                //and then i proceded to fuck the game
                //My Life IS OVER!
                if (collider.CompareTag("Grab"))
                {
                    
                    Debug.Log("Picking up " + collider.name);

                    originalGunOnGround = collider.gameObject;

                    gun = originalGunOnGround.GetComponent<Gun>();
                    

                    if(multiplayerSetup.IsTheGuy){
                        Debug.Log("is player");
                        currentGun = originalGunOnGround;
                        
                        gun.enabled = true;


                        

                        
                    } else {
                        Debug.Log("is NOT player");
                    }
                    originalGunOnGround.transform.parent = playerHand;
                    originalGunOnGround.transform.localPosition = Vector3.zero;
                    originalGunOnGround.transform.localEulerAngles = new Vector3(0,0,0);
                    currentGun.transform.localScale = new Vector3(1f, 1f, 1f);

                    // Store the original Rigidbody2D component
                    originalRigidbody = collider.GetComponent<Rigidbody2D>();

                    Collider2D collider2 = originalGunOnGround.GetComponent<Collider2D>();

                    
                    photonView.RPC("ToggleRigidbodyAndCollider", RpcTarget.AllBuffered, false);

                    // Update the originalGunOnGround reference and lastPickedGunType
                    originalGunOnGround = currentGun;
                    lastPickedGunType = collider.tag;

                    isHoldingGun = true;
                    break;
                }
            }
        }


    [PunRPC]
    void DropGun()
    {
        if (currentGun != null)
        {
            
            Debug.Log("Dropping " + currentGun.name);

            originalGunOnGround.transform.parent = null;
            currentGun.transform.localScale = new Vector3(1f, 1f, 1f);

            gun = currentGun.GetComponent<Gun>();
            
            if(multiplayerSetup.IsTheGuy){
                
                gun.enabled = false;
            }

            // Re-enable the original Rigidbody2D component if it exists
            if (originalRigidbody != null)
            {
                originalRigidbody.simulated = true;
            }

            // Check if the instantiated gun has a Rigidbody2D and re-enable it
            Rigidbody2D gunRigidbody = currentGun.GetComponent<Rigidbody2D>();
            if (gunRigidbody != null)
            {
                Collider2D tempCollider = originalGunOnGround.GetComponent<Collider2D>();

                photonView.RPC("ToggleRigidbodyAndCollider", RpcTarget.AllBuffered, true);

                originalGunOnGround.transform.parent = null;

                gunRigidbody.AddForce(playerHand.transform.forward * chuckSpeed);

                Debug.Log("Toggle");
            }

            
            

            

            // Destroy the current gun if it's different from the original gun on the ground
            if (currentGun != originalGunOnGround)
            {
                Destroy(currentGun);
            }

            currentGun = null;

            isHoldingGun = false;
        } else {
            Debug.Log("FUCKING KILL yourself");
        }
    }

    [PunRPC]
    public void ToggleRigidbodyAndCollider(bool toggleBool){
        Debug.Log("toggling");
        gun.Toggle(toggleBool);
    }
}
//homer